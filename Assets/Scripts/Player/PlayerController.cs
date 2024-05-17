using System;
using Baracuda.Monitoring;
using DavidFDev.DevConsole;
using EventBusSystem;
using TMPro;
using UnityEditor;
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
        /// Acceleration force which will be applied when player on slope
        /// </summary>
        [Tooltip("Acceleration force to slope")] [SerializeField, Range(0f, 1f)]
        private float accelerationForceToSlope;

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

        [SerializeField] private bool enableJumpingFromWalls;

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
        /// Layer for sliding ground
        /// </summary>
        [Tooltip("Layer for sliding ground")] [SerializeField]
        private LayerMask slidingMask = -1;

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

        #endregion

        #region Private Variables

        /// <summary>
        /// Cached Rigidbody
        /// </summary>
        private Rigidbody _body;

        /// <summary>
        /// Cached connected rigidbody
        /// </summary>
        private Rigidbody _connectedRigidbody;

        /// <summary>
        /// Cached previous connected rigidbody
        /// </summary>
        private Rigidbody _previousConnectedRigidbody;

        /// <summary>
        /// Calculated velocity
        /// </summary>
        [Monitor] private Vector3 _velocity;

        /// <summary>
        /// Relative velocity in local space
        /// </summary>
        private Vector3 _relativeVelocity;

        /// <summary>
        /// Cached velocity of connected Rigidbody
        /// </summary>
        private Vector3 _connectedVelocity;

        /// <summary>
        /// World position of connected Rigidbody
        /// </summary>
        private Vector3 _connectedWorldPosition;

        /// <summary>
        /// Local position of connected Rigidbody
        /// </summary>
        private Vector3 _connectedLocalPosition;

        /// <summary>
        /// Desired velocity according to input
        /// </summary>
        [Monitor] private Vector3 _desiredVelocity;

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
        [Monitor] private int _groundContactCount;

        /// <summary>
        /// Count of contacts with steep
        /// </summary>
        [Monitor] private int _steepContactCount;

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
        /// Calculated internal value to check sliding
        /// </summary>
        private float _minSlidingDotProduct;

        /// <summary>
        /// Internal timer for coyote jump
        /// </summary>
        [Monitor] private float _internalCoyoteTimer;

        /// <summary>
        /// Internal timer for jump
        /// </summary>
        private float _internalJumpCooldownTimer;

        /// <summary>
        /// Direction in which player slide on slope
        /// </summary>
        private Vector3 _slidingDirection;

        /// <summary>
        /// Cached gravity force
        /// </summary>
        private Vector3 _gravityForce;

        private Vector3 _lastGroundPoint;

        [Monitor] private bool _isRaycastGrounded;
        
        [Monitor] private bool _isRaycastSliding;

        #endregion

        #region Public Fields

        [Monitor] public bool OnGround => _groundContactCount > 0;
        [Monitor] public bool OnSteep => _steepContactCount > 0;
        [Monitor] public float DesiredSpeed => _cachedSprinting ? runSpeed : walkSpeed;
        [Monitor] public Vector3 HorizontalVelocity => new(_relativeVelocity.x, 0f, _relativeVelocity.z);

        #endregion

        #region MonoBehaviour

        private void Awake()
        {
            _body = GetComponent<Rigidbody>();
            _cameraPosition = Camera.main?.transform;

            Monitor.StartMonitoring(this);

            if (_cameraPosition == null)
                Logger.Log(LoggerChannel.Player, Priority.Warning,
                    "Controller can't find MainCamera. Rotating input will be not work");
            OnValidate();

#if UNITY_EDITOR || DEBUG
            RegisterCommands();
            ChangeMonitoring(true);
#endif
        }

        private void OnDestroy()
        {
#if UNITY_EDITOR || DEBUG
            UnregisterCommands();
            ChangeMonitoring(false);
#endif
        }

        private void OnValidate()
        {
            _minGroundDotProduct = Mathf.Cos(maxGroundAngle * Mathf.Deg2Rad);
            _minStairsDotProduct = Mathf.Cos(maxStairsAngle * Mathf.Deg2Rad);
            _minSlidingDotProduct = Mathf.Cos(1f * Mathf.Deg2Rad);
            _gravityForce = Physics.gravity;
        }

        private void Update()
        {
            CalculateMovementVector();
            RotateVelocityAccordingToCamera();
            RotateToVelocity();
            UpdateTimers();
        }

        private void FixedUpdate()
        {
            UpdateState();

            RaycastCheckGround();

            IncreaseSlideOnSlope();

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
            var isFalling = (OnSteep && !OnGround && Vector3.Dot(transform.up, _steepNormal) > 0f) ||
                            (!OnGround && velocity.y < 0);
            var relativeSpeed = HorizontalVelocity.magnitude / DesiredSpeed;
            var isJumping = _jumpPhase > 0 && velocity.y > 0.1f;
            return new MovementState(isFalling, relativeSpeed, isJumping, _cachedSprinting);
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
            _contactNormal = _steepNormal = _connectedVelocity = Vector3.zero;
            _previousConnectedRigidbody = _connectedRigidbody;
            _connectedRigidbody = null;
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

            if (!_connectedRigidbody) return;
            if (_connectedRigidbody.isKinematic || _connectedRigidbody.mass >= _body.mass)
            {
                UpdateConnectionState();
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

            if (!Physics.Raycast(_body.position, Vector3.down, out var hit, probeDistance, probeMask))
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

            if (hit.rigidbody)
                _connectedRigidbody = hit.rigidbody;
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
            if (_steepNormal.y >= _minGroundDotProduct)
            {
                _steepContactCount = 0;
                _groundContactCount = 1;
                _contactNormal = _steepNormal;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Adjust velocity with according to input
        /// </summary>
        private void AdjustVelocity()
        {
            var xAxis = ProjectOnContactPlane(Vector3.right).normalized;
            var zAxis = ProjectOnContactPlane(Vector3.forward).normalized;

            _relativeVelocity = _velocity - _connectedVelocity;

            var currentX = Vector3.Dot(_relativeVelocity, xAxis);
            var currentZ = Vector3.Dot(_relativeVelocity, zAxis);

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
            var isCoyoteJump = false;
            if (OnGround)
            {
                jumpDirection = _contactNormal;
            }
            else if (OnSteep)
            {
                if (!enableJumpingFromWalls) return;
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
                isCoyoteJump = true;
            }
            else
            {
                return;
            }

            _stepsSinceLastJump = 0;
            _jumpPhase += 1;

            var heightDifference = _lastGroundPoint.y - transform.position.y;
            var jumpSpeed = Mathf.Sqrt(-2f * _gravityForce.y *
                                       (isCoyoteJump ? jumpHeight + heightDifference * 3.5f : jumpHeight));
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
                    if (_isRaycastSliding)
                    {
                        _groundContactCount = 0;
                        _contactNormal = Vector3.up;
                        return;
                    }
                    _groundContactCount += 1;
                    _contactNormal += normal;
                    _lastGroundPoint = collision.GetContact(i).point;
                }
                else if (normal.y > -0.01f)
                {
                    _steepContactCount += 1;
                    _steepNormal += normal;
                }
            }

            if (collision.rigidbody == null) return;
            _connectedRigidbody = collision.rigidbody;
        }

        private void RaycastCheckGround()
        {
            var probeRays = new Ray[]
            {
                new(_body.position, Vector3.down),
                new(_body.position.AddX(0.25f), Vector3.down),
                new(_body.position.AddX(-0.25f), Vector3.down),
                new(_body.position.AddZ(0.25f), Vector3.down),
                new(_body.position.AddZ(-0.25f), Vector3.down)
            };

            _isRaycastGrounded = RaycastHelpers.CheckAllRays(probeRays, probeDistance, probeMask);
            _isRaycastSliding = RaycastHelpers.CheckAnyRays(probeRays, probeDistance, slidingMask);
        }

        /// <summary>
        /// Update connection state
        /// </summary>
        private void UpdateConnectionState()
        {
            if (_connectedRigidbody == _previousConnectedRigidbody)
            {
                var connectionMovement = _connectedRigidbody.transform.TransformPoint(_connectedLocalPosition) -
                                         _connectedWorldPosition;
                _connectedVelocity = connectionMovement / Time.deltaTime;
            }

            _connectedWorldPosition = _body.position;
            _connectedLocalPosition = _connectedRigidbody.transform.InverseTransformPoint(_connectedWorldPosition);
        }

        /// <summary>
        /// Project vector on ground contact point
        /// </summary>
        /// <param name="vector">Input vector</param>
        /// <returns>Projected vector</returns>
        private Vector3 ProjectOnContactPlane(Vector3 vector)
        {
            return vector - _contactNormal.normalized * Vector3.Dot(vector, _contactNormal.normalized);
        }

        /// <summary>
        /// Get minimal dot product according to physic layer
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        private float GetMinDot(int layer)
        {
            if ((probeMask.value & (1 << layer)) != 0)
            {
                return _minGroundDotProduct;
            }

            if ((stairsMask.value & (1 << layer)) != 0)
            {
                return _minStairsDotProduct;
            }

            if ((slidingMask.value & (1 << layer)) != 0)
            {
                return _minSlidingDotProduct;
            }

            return _minGroundDotProduct;
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

        private void IncreaseSlideOnSlope()
        {
            if (_isRaycastSliding || !_isRaycastGrounded)
            {
                if (!(Vector3.Dot(transform.up, _steepNormal) > 0f)) return;
                _groundContactCount = 0;
                _slidingDirection = _gravityForce - _steepNormal * Vector3.Dot(_gravityForce, _steepNormal);
                _desiredVelocity = Vector3.zero;

                _velocity += _slidingDirection.normalized * accelerationForceToSlope;
            }
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

        private void ResetVelocity()
        {
            _desiredVelocity = Vector3.zero;
            _velocity = Vector3.zero;
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

        [ListenTo(SignalEnum.OnExitCutscene)]
        private void OnExitCutscene(EventModel eventModel)
        {
            ResetVelocity();
        }

        #endregion

        #region Dev-commands

#if UNITY_EDITOR || DEBUG

        private void RegisterCommands()
        {
            DevConsole.AddCommand(Command.Create(
                name: "walljump",
                aliases: "",
                helpText: "Switch wall jump ability",
                callback: () =>
                {
                    enableJumpingFromWalls = !enableJumpingFromWalls;
                    DevConsole.Log($"Walljump is {enableJumpingFromWalls}");
                }));
        }

        private void UnregisterCommands()
        {
            DevConsole.RemoveCommand("walljump");
        }

        private void ChangeMonitoring(bool enable)
        {
            if (enable)
                Monitor.StartMonitoring(this);
            else
                Monitor.StopMonitoring(this);
        }

#endif

        #endregion
    }
}