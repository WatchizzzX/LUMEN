using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Utils.Extensions;

namespace Player
{
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
    public class PlayerController : MonoBehaviour, IMovementController
    {
        [SerializeField, Range(0f, 100f)] private float walkSpeed = 1.2f, runSpeed = 4.2f;

        [SerializeField, Range(0f, 100f)] private float maxAcceleration = 10f, maxAirAcceleration = 1f;

        [SerializeField, Range(0f, 1)] private float smoothRotationTime = 0.5f;

        [SerializeField, Range(0f, 10f)] private float jumpHeight = 2f;

        [SerializeField, Range(0, 5)] private int maxAirJumps = 0;

        [SerializeField, Range(0f, 0.5f)] private float coyoteTime;
        [SerializeField, Range(0f, 1f)] private float jumpCooldown;

        [SerializeField, Range(0, 90)] private float maxGroundAngle = 25f, maxStairsAngle = 50f;

        [SerializeField, Range(0f, 100f)] private float maxSnapSpeed = 100f;

        [SerializeField, Min(0f)] private float probeDistance = 1f;

        [SerializeField] private LayerMask probeMask = -1, stairsMask = -1;

        [SerializeField] private TextMeshProUGUI debugText;

        private Rigidbody _body;

        private Vector3 _velocity, _desiredVelocity;
        private bool _desiredJump;

        private float _calculatedAngle;
        private float _currentAngleVelocity;

        private Vector3 _contactNormal, _steepNormal;
        private int _groundContactCount, _steepContactCount;

        private Vector2 _cachedDirection;
        private bool _cachedSprinting;

        private int _jumpPhase;
        private int _stepsSinceLastGrounded, _stepsSinceLastJump;

        private Transform _cameraPosition;

        private float _minGroundDotProduct, _minStairsDotProduct;

        private float _internalCoyoteTimer;
        private float _internalJumpCooldownTimer;

        public bool OnGround => _groundContactCount > 0;
        public bool OnSteep => _steepContactCount > 0;
        public float DesiredSpeed => _cachedSprinting ? runSpeed : walkSpeed;
        public Vector3 HorizontalVelocity => new(_body.velocity.x, 0f, _body.velocity.z);

        public MovementState GetMovementState()
        {
            return new MovementState(
                (OnSteep && !OnGround) || (!OnGround && _body.velocity.y < 0),
                HorizontalVelocity.magnitude / DesiredSpeed);
        }

        private void Awake()
        {
            _body = GetComponent<Rigidbody>();
            _cameraPosition = Camera.main.transform;
            OnValidate();
        }

        private void OnValidate()
        {
            _minGroundDotProduct = Mathf.Cos(maxGroundAngle * Mathf.Deg2Rad);
            _minStairsDotProduct = Mathf.Cos(maxStairsAngle * Mathf.Deg2Rad);
        }

        private void Update()
        {
            CalculateMovementVector();
            RotateVelocityAccordingToCamera();
            RotateToVelocity();
            UpdateTimers();
            DebugText();
        }

        private void FixedUpdate()
        {
            UpdateState();
            AdjustVelocity();

            if (_desiredJump)
            {
                _desiredJump = false;
                Jump();
            }

            CompensateStairsSliding();
            _body.velocity = _velocity;
            ClearState();
        }

        private void DebugText()
        {
            debugText.text = $"CoyoteTime:{_internalCoyoteTimer:f2}. OnGround:{OnGround}. OnSteep:{OnSteep}";
        }

        private void UpdateTimers()
        {
            if (_internalCoyoteTimer > 0f)
                _internalCoyoteTimer -= Time.deltaTime;
            if (_internalJumpCooldownTimer > 0f)
                _internalJumpCooldownTimer -= Time.deltaTime;
        }

        private void RotateToVelocity()
        {
            var horizontalVelocity = HorizontalVelocity;
            if (!(horizontalVelocity.magnitude > 0.1f)) return;

            var targetAngle = Mathf.Atan2(horizontalVelocity.x, horizontalVelocity.z) * Mathf.Rad2Deg;

            _calculatedAngle = Mathf.SmoothDampAngle(_calculatedAngle, targetAngle, ref _currentAngleVelocity,
                smoothRotationTime);
            transform.rotation = Quaternion.Euler(0, _calculatedAngle, 0);
        }

        private void ClearState()
        {
            _groundContactCount = _steepContactCount = 0;
            _contactNormal = _steepNormal = Vector3.zero;
        }

        private void UpdateState()
        {
            _stepsSinceLastGrounded += 1;
            _stepsSinceLastJump += 1;
            _velocity = _body.velocity;
            if (OnGround || SnapToGround() || CheckSteepContacts())
            {
                if (_stepsSinceLastGrounded > 1)
                    _internalJumpCooldownTimer = jumpCooldown;
                
                _stepsSinceLastGrounded = 0;
                if (_stepsSinceLastJump > 1)
                {
                    _jumpPhase = 0;
                }

                if (_groundContactCount > 1)
                {
                    _contactNormal.Normalize();
                }
            }
            else
            {
                if (_stepsSinceLastGrounded == 1)
                {
                    if (_stepsSinceLastJump > 2)
                        _internalCoyoteTimer = coyoteTime;
                }
                if (_stepsSinceLastJump < _stepsSinceLastGrounded)
                    _internalCoyoteTimer = 0f;

                _contactNormal = Vector3.up;
            }
        }

        private bool SnapToGround()
        {
            if (_stepsSinceLastGrounded > 1 || _stepsSinceLastJump <= 2)
            {
                return false;
            }

            var speed = _velocity.magnitude;
            if (speed > maxSnapSpeed)
            {
                return false;
            }

            if (!Physics.Raycast(_body.position, Vector3.down, out RaycastHit hit, probeDistance, probeMask))
            {
                return false;
            }

            if (hit.normal.y < GetMinDot(hit.collider.gameObject.layer))
            {
                return false;
            }

            _groundContactCount = 1;
            _contactNormal = hit.normal;
            var dot = Vector3.Dot(_velocity, hit.normal);
            if (dot > 0f)
            {
                _velocity = (_velocity - hit.normal * dot).normalized * speed;
            }

            return true;
        }

        private bool CheckSteepContacts()
        {
            if (_steepContactCount <= 1) return false;

            _steepNormal.Normalize();
            if (!(_steepNormal.y >= _minGroundDotProduct)) return false;

            _steepContactCount = 0;
            _groundContactCount = 1;
            _contactNormal = _steepNormal;
            return true;
        }

        private void AdjustVelocity()
        {
            var xAxis = ProjectOnContactPlane(Vector3.right).normalized;
            var zAxis = ProjectOnContactPlane(Vector3.forward).normalized;

            var currentX = Vector3.Dot(_velocity, xAxis);
            var currentZ = Vector3.Dot(_velocity, zAxis);

            var acceleration = OnGround ? maxAcceleration : maxAirAcceleration;
            var maxSpeedChange = acceleration * Time.deltaTime;

            var newX = Mathf.MoveTowards(currentX, _desiredVelocity.x, maxSpeedChange);
            var newZ = Mathf.MoveTowards(currentZ, _desiredVelocity.z, maxSpeedChange);

            _velocity += xAxis * (newX - currentX) + zAxis * (newZ - currentZ);
        }

        private void Jump()
        {
            if (_internalJumpCooldownTimer > 0f) return;
            
            Vector3 jumpDirection;
            if (OnGround)
            {
                jumpDirection = _contactNormal;
            }
            else if (OnSteep)
            {
                jumpDirection = _steepNormal;
                _jumpPhase = 0;
            }
            else if (maxAirJumps > 0 && _jumpPhase <= maxAirJumps)
            {
                if (_jumpPhase == 0)
                {
                    _jumpPhase = 1;
                }

                jumpDirection = _contactNormal;
            }
            else if (_internalCoyoteTimer > 0)
            {
                jumpDirection = Vector3.up;
            }
            else
            {
                return;
            }

            _stepsSinceLastJump = 0;
            _jumpPhase += 1;
            var jumpSpeed = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
            jumpDirection = (jumpDirection + Vector3.up).normalized;
            var alignedSpeed = Vector3.Dot(_velocity, jumpDirection);
            if (alignedSpeed > 0f)
            {
                jumpSpeed = Mathf.Max(jumpSpeed - alignedSpeed, 0f);
            }

            _velocity += jumpDirection * jumpSpeed;
        }

        private void OnCollisionEnter(Collision collision)
        {
            EvaluateCollision(collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            EvaluateCollision(collision);
        }

        private void EvaluateCollision(Collision collision)
        {
            var minDot = GetMinDot(collision.gameObject.layer);
            for (var i = 0; i < collision.contactCount; i++)
            {
                var normal = collision.GetContact(i).normal;
                if (normal.y >= minDot)
                {
                    _groundContactCount += 1;
                    _contactNormal += normal;
                }
                else if (normal.y > -0.01f)
                {
                    _steepContactCount += 1;
                    _steepNormal += normal;
                }
            }
        }

        private Vector3 ProjectOnContactPlane(Vector3 vector)
        {
            return vector - _contactNormal * Vector3.Dot(vector, _contactNormal);
        }

        private float GetMinDot(int layer)
        {
            return (stairsMask & (1 << layer)) == 0 ? _minGroundDotProduct : _minStairsDotProduct;
        }

        private void CalculateMovementVector()
        {
            var convertedDirection = _cachedDirection.ToXZVector3();
            convertedDirection = ProjectOnContactPlane(convertedDirection);
            convertedDirection *= DesiredSpeed;

            _desiredVelocity = new Vector3(convertedDirection.x, 0, convertedDirection.z);
        }

        private void CompensateStairsSliding()
        {
            _velocity -= ProjectOnContactPlane(Physics.gravity) * Time.fixedDeltaTime;
        }

        private void RotateVelocityAccordingToCamera()
        {
            var cameraRotation = Quaternion.AngleAxis(_cameraPosition.eulerAngles.y, Vector3.up);
            _desiredVelocity = cameraRotation * _desiredVelocity;
        }

        public void SetMoveDirection(Vector2 input)
        {
            _cachedDirection = input;
        }

        public void SetSprint(bool sprint)
        {
            _cachedSprinting = sprint;
        }

        public void CallToJump()
        {
            _desiredJump = true;
        }
    }
}