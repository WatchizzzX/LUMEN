using TMPro;
using UnityEngine;
using Utils;
using Utils.Extensions;
using Logger = Utils.Logger;

namespace Player
{
    /// <summary>
    /// Player movement controller
    /// </summary>
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
    public class PlayerController : MonoBehaviour, IMovementController
    {
        #region Serialized Fileds

        /// <summary>
        /// Walk speed
        /// </summary>
        [Header("Movement settings")] [Tooltip("Walk speed (m/s)")] [SerializeField, Range(0f, 100f)]
        private float walkSpeed = 1.2f;

        /// <summary>
        /// Run speed
        /// </summary>
        [Tooltip("Run speed (m/s)")] [SerializeField, Range(0f, 100f)]
        private float runSpeed = 4.2f;

        /// <summary>
        /// Acceleration force when player on ground
        /// </summary>
        [Tooltip("Acceleration force on ground")] [SerializeField, Range(0f, 100f)]
        private float maxAcceleration = 10f;

        /// <summary>
        /// Acceleration force when player in air
        /// </summary>
        [Tooltip("Acceleration force in air")] [SerializeField, Range(0f, 100f)]
        private float maxAirAcceleration = 1f;

        /// <summary>
        /// The time for which the player turns in desired direction
        /// </summary>
        [Space(2f), Header("Rotation settings")]
        [Tooltip("The time for which the player turns")]
        [SerializeField, Range(0f, 1)]
        private float smoothRotationTime = 0.5f;

        /// <summary>
        /// Jump height
        /// </summary>
        [Space(2f), Header("Jumping settings")] [Tooltip("Jump height in metres")] [SerializeField, Range(0f, 10f)]
        private float jumpHeight = 2f;

        /// <summary>
        /// Max count of air jumps
        /// </summary>
        [Tooltip("Max jumps count in air")] [SerializeField, Range(0, 5)]
        private int maxAirJumps = 0;

        /// <summary>
        /// Time for coyote jump
        /// </summary>
        [Tooltip("Time for coyote jump")] [SerializeField, Range(0f, 0.5f)]
        private float coyoteTime;

        /// <summary>
        /// Cooldown for next jump
        /// </summary>
        [Tooltip("Cooldown for next jump")] [SerializeField, Range(0f, 1f)]
        private float jumpCooldown;

        /// <summary>
        /// The maximum angle at which the ground remains the ground"
        /// </summary>
        [Space(2f), Header("Ground detection settings")]
        [Tooltip("The maximum angle at which the ground remains the ground")]
        [SerializeField, Range(0, 90)]
        private float maxGroundAngle = 25f;

        /// <summary>
        /// The maximum angle at which the stairs remains the stairs
        /// </summary>
        [Tooltip("The maximum angle at which the stairs remains the stairs")] [SerializeField, Range(0, 90)]
        private float maxStairsAngle = 50f;

        /// <summary>
        /// Layer for ground
        /// </summary>
        [Tooltip("Layer for ground")] [SerializeField]
        private LayerMask probeMask = -1;

        /// <summary>
        /// Layer for stairs
        /// </summary>
        [Tooltip("Layer for stairs")] [SerializeField]
        private LayerMask stairsMask = -1;

        /// <summary>
        /// The maximum speed at which the player will be pressed to the ground
        /// </summary>
        [Space(2f), Header("Snap settings")]
        [Tooltip("The maximum speed at which the player will be pressed to the ground")]
        [SerializeField, Range(0f, 100f)]
        private float maxSnapSpeed = 100f;

        /// <summary>
        /// The maximum distance to the ground at which the player will still be attracted to the ground
        /// </summary>
        [Tooltip("The maximum distance to the ground at which the player will still be attracted to the ground")]
        [SerializeField, Min(0f)]
        private float probeDistance = 1f;

        [Tooltip("Debug text")] [SerializeField]
        private TextMeshProUGUI debugText;

        #endregion

        #region Private Variables

        /// <summary>
        /// Cached Rigidbody
        /// </summary>
        private Rigidbody _body;

        /// <summary>
        /// Calculated velocity
        /// </summary>
        private Vector3 _velocity;

        /// <summary>
        /// Desired velocity according to input
        /// </summary>
        private Vector3 _desiredVelocity;

        /// <summary>
        /// Cached jump state
        /// </summary>
        private bool _desiredJump;

        /// <summary>
        /// Calculated angle for rotation
        /// </summary>
        private float _calculatedAngle;

        /// <summary>
        /// Velocity of change calculatedAngle
        /// </summary>
        private float _currentAngleVelocity;

        /// <summary>
        /// Normal of contact point
        /// </summary>
        private Vector3 _contactNormal;

        /// <summary>
        /// Normal of contact, when player on steep
        /// </summary>
        private Vector3 _steepNormal;

        /// <summary>
        /// Count of contacts with ground
        /// </summary>
        private int _groundContactCount;

        /// <summary>
        /// Count of contacts with steep
        /// </summary>
        private int _steepContactCount;

        /// <summary>
        /// Cached input move
        /// </summary>
        private Vector2 _cachedDirection;

        /// <summary>
        /// Cached internal state of sprinting
        /// </summary>
        private bool _cachedSprinting;

        /// <summary>
        /// Cached internal state of sprinting in air
        /// </summary>
        private bool _cachedInAirSprinting;

        /// <summary>
        /// Internal state of jump
        /// </summary>
        private int _jumpPhase;

        /// <summary>
        /// Counter steps from last ground
        /// </summary>
        private int _stepsSinceLastGrounded;

        /// <summary>
        /// Counter steps from last jump
        /// </summary>
        private int _stepsSinceLastJump;

        /// <summary>
        /// Cached camera Transform
        /// </summary>
        private Transform _cameraPosition;

        /// <summary>
        /// Calculated internal value to check ground
        /// </summary>
        private float _minGroundDotProduct;

        /// <summary>
        /// Calculated internal value to check stairs
        /// </summary>
        private float _minStairsDotProduct;

        /// <summary>
        /// Internal timer for coyote jump
        /// </summary>
        private float _internalCoyoteTimer;

        /// <summary>
        /// Internal timer for jump
        /// </summary>
        private float _internalJumpCooldownTimer;

        #endregion

        #region Public Fields

        public bool OnGround => _groundContactCount > 0;
        public bool OnSteep => _steepContactCount > 0;
        public float DesiredSpeed => _cachedSprinting ? runSpeed : walkSpeed;
        public Vector3 HorizontalVelocity => new(_body.velocity.x, 0f, _body.velocity.z);

        #endregion

        #region MonoBehaviour

        private void Awake()
        {
            _body = GetComponent<Rigidbody>();
            _cameraPosition = Camera.main.transform;

            if (_cameraPosition == null)
                Logger.Log(LoggerChannel.Player, Priority.Warning,
                    "Controller can't find MainCamera. Rotating input will be not work");
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

        private void OnCollisionEnter(Collision collision)
        {
            EvaluateCollision(collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            EvaluateCollision(collision);
        }

        #endregion

        #region Methods

        public MovementState GetMovementState()
        {
            var velocity = _body.velocity;
            var isFalling = (OnSteep && !OnGround) || (!OnGround && velocity.y < 0);
            var relativeSpeed = HorizontalVelocity.magnitude / DesiredSpeed;
            var isJumping = _desiredJump && _internalJumpCooldownTimer <= 0f || (!OnGround && velocity.y >= 0);
            return new MovementState(isFalling, relativeSpeed, isJumping, _cachedSprinting);
        }

        /// <summary>
        /// Draw debug info
        /// </summary>
        private void DebugText()
        {
            debugText.text = $"CoyoteTime:{_internalCoyoteTimer:f2}. OnGround:{OnGround}. OnSteep:{OnSteep}";
        }

        /// <summary>
        /// Update internal timers. Call on every Update
        /// </summary>
        private void UpdateTimers()
        {
            if (_internalCoyoteTimer > 0f)
                _internalCoyoteTimer -= Time.deltaTime;
            if (_internalJumpCooldownTimer > 0f)
                _internalJumpCooldownTimer -= Time.deltaTime;
        }

        /// <summary>
        /// Rotate controller in velocity direction
        /// </summary>
        private void RotateToVelocity()
        {
            var horizontalVelocity = HorizontalVelocity;
            if (!(horizontalVelocity.magnitude > 0.1f)) return;

            var targetAngle = Mathf.Atan2(horizontalVelocity.x, horizontalVelocity.z) * Mathf.Rad2Deg;

            _calculatedAngle = Mathf.SmoothDampAngle(_calculatedAngle, targetAngle, ref _currentAngleVelocity,
                smoothRotationTime);
            transform.rotation = Quaternion.Euler(0, _calculatedAngle, 0);
        }

        /// <summary>
        /// Clear internal state in end of FixedUpdate
        /// </summary>
        private void ClearState()
        {
            _groundContactCount = _steepContactCount = 0;
            _contactNormal = _steepNormal = Vector3.zero;
        }

        /// <summary>
        /// Update internal state in start of FixedUpdate
        /// </summary>
        private void UpdateState()
        {
            _stepsSinceLastGrounded += 1;
            _stepsSinceLastJump += 1;
            _velocity = _body.velocity;
            if (OnGround || SnapToGround() || CheckSteepContacts())
            {
                if (_stepsSinceLastGrounded > 1)
                    _internalJumpCooldownTimer = jumpCooldown;

                if (_cachedInAirSprinting)
                {
                    _cachedSprinting = true;
                    _cachedInAirSprinting = false;
                }

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

        /// <summary>
        /// Try to snap controller to ground
        /// </summary>
        /// <returns>Snapping is successful</returns>
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

        /// <summary>
        /// Check steep contact
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Adjust velocity with according to input
        /// </summary>
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

        /// <summary>
        /// Jump handler
        /// </summary>
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

        /// <summary>
        /// Evaluate internal state, according on every contact point
        /// </summary>
        /// <param name="collision">Detected collision</param>
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

        /// <summary>
        /// Project vector on ground contact point
        /// </summary>
        /// <param name="vector">Input vector</param>
        /// <returns>Projected vector</returns>
        private Vector3 ProjectOnContactPlane(Vector3 vector)
        {
            return vector - _contactNormal * Vector3.Dot(vector, _contactNormal);
        }

        /// <summary>
        /// Get minimal dot product according to physic layer
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        private float GetMinDot(int layer)
        {
            return (stairsMask & (1 << layer)) == 0 ? _minGroundDotProduct : _minStairsDotProduct;
        }

        /// <summary>
        /// Calculate desired movement vector
        /// </summary>
        private void CalculateMovementVector()
        {
            var convertedDirection = _cachedDirection.ToXZVector3();
            convertedDirection = ProjectOnContactPlane(convertedDirection);
            convertedDirection *= DesiredSpeed;

            _desiredVelocity = new Vector3(convertedDirection.x, 0, convertedDirection.z);
        }

        /// <summary>
        /// Compensate sliding on steep
        /// </summary>
        private void CompensateStairsSliding()
        {
            _velocity -= ProjectOnContactPlane(Physics.gravity) * Time.fixedDeltaTime;
        }

        /// <summary>
        /// Rotate input, according to camera
        /// </summary>
        private void RotateVelocityAccordingToCamera()
        {
            var cameraRotation = Quaternion.AngleAxis(_cameraPosition.eulerAngles.y, Vector3.up);
            _desiredVelocity = cameraRotation * _desiredVelocity;
        }

        #endregion

        #region Callbacks

        public void SetMoveDirection(Vector2 input)
        {
            _cachedDirection = input;
        }

        public void SetSprint(bool sprint)
        {
            if (OnGround || _cachedSprinting)
                _cachedSprinting = sprint;
            else
            {
                _cachedInAirSprinting = true;
            }
        }

        public void CallToJump()
        {
            _desiredJump = true;
        }

        #endregion
    }
}