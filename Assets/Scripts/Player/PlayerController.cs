using Baracuda.Monitoring;
using Unity.TinyCharacterController;
using Unity.TinyCharacterController.Brain;
using Unity.TinyCharacterController.Check;
using Unity.TinyCharacterController.Control;
using Unity.TinyCharacterController.Effect;
using UnityEngine;
using Utils.Extensions;
using Utils.Gameplay;

namespace Player
{
    [SelectionBase]
    public class PlayerController : MonoBehaviour
    {
        [Header("Move Settings")] [SerializeField, Min(1f)]
        private float walkSpeed;

        [SerializeField, Min(1f)] private float runSpeed;

        private Vector2 _inputMove;
        private bool _inputSprint;

        private Animator _animator;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int IsGround = Animator.StringToHash("IsGround");
        private static readonly int IsMove = Animator.StringToHash("IsMove");
        private static readonly int ContactWall = Animator.StringToHash("IsContactWall");

        private RigidbodyBrain _brain;
        private CharacterSettings _settings;
        private MoveControl _moveControl;
        private JumpControl _jumpControl;
        private Gravity _gravity;
        private GroundCheck _groundCheck;
        private LayerCheck _wallCheck;
        private LayerCheck _sliderCheck;
        private HeadContactCheck _headContactCheck;
        private ExtraForce _endSlidingForce;

        private bool _isOnWall;
        private bool _wallOnRightSide;
        private GameObject _cashedLastWallGameObject;
        private WallJumpSurface _connectedWallJumpSurface;

        private bool _isOnSlider;
        private SlidingSurface _connectedSliderSurface;

        [Monitor] private bool IsOnWall => _isOnWall;

        [Monitor] [MShowIf(nameof(IsOnWall))] private Vector3 WallNormal => _wallCheck.Normal;

        [Monitor]
        [MShowIf(nameof(IsOnWall))]
        private Vector3 WallJumpDirection
        {
            get
            {
                if (!_connectedWallJumpSurface) return Vector3.zero;

                var localVelocity = transform.up * _connectedWallJumpSurface.AdditiveUpVelocity
                                    + transform.forward * _connectedWallJumpSurface.AdditiveForwardVelocity
                                    + (_wallOnRightSide ? -transform.right * _connectedWallJumpSurface.AdditiveSideVelocity:
                                    transform.right * _connectedWallJumpSurface.AdditiveSideVelocity);

                return _wallCheck.Normal + localVelocity;
            }
        }

        [Monitor] private bool IsOnSlider => _isOnSlider;

        [Monitor] private bool IsGrounded => _groundCheck.IsOnGround;
        [Monitor] private Vector3 ForwardDirection => transform.forward;
        [Monitor] private float CurrentSpeed => _moveControl.CurrentSpeed;
        [Monitor] private Gravity.State GravityState => _gravity.CurrentState;
        [Monitor] private int AerialJumpCount => _jumpControl.AerialJumpCount;

        [MonitorProperty]
        [MShowIf(nameof(IsOnWall))]
        private string GetSideOfWallDebug
        {
            get
            {
                var wallNormalXZ = _wallCheck.Normal.ToXZVector2();
                var perpendicular = Vector2.Perpendicular(wallNormalXZ).ToXZVector3();

                var angle = Vector3.SignedAngle(transform.forward, perpendicular, Vector3.up);

                return angle > 90 ? "Wall on right side" : "Wall on left side";
            }
        }

        private void Awake()
        {
            _brain = GetComponent<RigidbodyBrain>();
            _settings = GetComponent<CharacterSettings>();
            _animator = GetComponent<Animator>();
            _moveControl = GetComponent<MoveControl>();
            _jumpControl = GetComponent<JumpControl>();
            _gravity = GetComponent<Gravity>();
            _groundCheck = GetComponent<GroundCheck>();
            _headContactCheck = GetComponent<HeadContactCheck>();
            _endSlidingForce = GetComponent<ExtraForce>();

            var layerChecks = GetComponents<LayerCheck>();
            _wallCheck = layerChecks[0];
            _sliderCheck = layerChecks[1];

#if DEBUG || UNITY_EDITOR
            this.StartMonitoring();
#endif
        }

#if DEBUG || UNITY_EDITOR
        void OnScriptHotReload()
        {
            this.StopMonitoring();
            this.StartMonitoring();
        }
#endif

        private void OnDestroy()
        {
#if DEBUG || UNITY_EDITOR
            this.StopMonitoring();
#endif
        }

        private void Update()
        {
            UpdateAnimatorGroundMovementState();
            UpdateAnimatorWallMovementState();

            if (_groundCheck.IsOnGround)
            {
                _cashedLastWallGameObject = null;

                RemoveEndSlidingForce();
            }
        }

        /// <summary>
        /// Updates the animator's ground movement state based on the current player's movement and ground check.
        /// </summary>
        /// <remarks>
        /// This method sets the animator's Speed parameter to the current speed of the player's movement control.
        /// It also sets the IsGround parameter to true if the player is on the ground and not falling, and false otherwise.
        /// Finally, it sets the IsMove parameter to true if the player is moving and false otherwise.
        /// </remarks>
        private void UpdateAnimatorGroundMovementState()
        {
            _animator.SetFloat(Speed, _moveControl.CurrentSpeed);

            _animator.SetBool(IsGround, _groundCheck.IsOnGround && _gravity.FallSpeed <= 0);

            _animator.SetBool(IsMove, _moveControl.IsMove);
        }

        /// <summary>
        /// Updates the animator's wall movement state based on the current player's movement and ground check.
        /// </summary>
        private void UpdateAnimatorWallMovementState()
        {
            if (_isOnWall)
            {
                _animator.CrossFade(_wallOnRightSide ? "WallHang_R" : "WallHang_L", 0.09f);
            }
        }

        /// <summary>
        /// Updates the animator's jump state based on the current player's contact with the ground and jump count.
        /// </summary>
        /// <remarks>
        /// This method checks if the player is in contact with a surface and not on the ground. If so, it returns early and does not update the animator.
        /// If the player is on the ground or in contact with a surface, it checks the jump count.
        /// If the jump count is 1, it plays the "DoubleJump" animation.
        /// Otherwise, it plays the "JumpStart" animation.
        /// </remarks>
        private void UpdateAnimatorJumpState()
        {
            if (_isOnWall)
            {
                return;
            }

            switch (_jumpControl.AerialJumpCount)
            {
                case 1:
                    _animator.Play("DoubleJump");
                    break;
                default:
                    _animator.Play("JumpStart");
                    break;
            }
        }

        private void RemoveEndSlidingForce()
        {
            _connectedSliderSurface = null;
            _isOnSlider = false;
            _endSlidingForce.ResetVelocity();
        }

        /// <summary>
        /// Handles the wall jump behavior of the player.
        /// </summary>
        private void WallJumpHandler()
        {
            if (_connectedWallJumpSurface.JumpHeight != 0)
                _jumpControl.JumpHeight = _connectedWallJumpSurface.JumpHeight;

            _jumpControl.JumpDirection = WallJumpDirection;

            _jumpControl.MovePriority = 5;
        }

        /// <summary>
        /// Handles the basic jump behavior of the player.
        /// </summary>
        private void StandardJumpHandler()
        {
            _jumpControl.JumpDirection = Vector3.up;

            _jumpControl.JumpHeight = 2f;

            _jumpControl.MovePriority = 0;
        }

        /// <summary>
        /// Handles the behavior of the player when they are hanging on a wall.
        /// </summary>
        private void OnWallHang()
        {
            _animator.SetBool(ContactWall, true);

            _moveControl.MovePriority = 0;
            _moveControl.TurnPriority = 0;
            _jumpControl.TurnPriority = 0;

            var newRotation = Quaternion.LookRotation(_wallCheck.Normal, Vector3.up) * Quaternion.Euler(0f, 90f, 0f);

            _brain.Warp(_wallOnRightSide
                ? newRotation
                : newRotation * Quaternion.Euler(0f, 180f, 0f));

            _gravity.GravityScale = 0;
            _gravity.SetVelocity(Vector3.zero);

            _isOnWall = true;
        }

        /// <summary>
        /// Resets the player's state after ending the wall hang behavior.
        /// </summary>
        private void OnEndWallHang()
        {
            _animator.SetBool(ContactWall, false);

            _moveControl.MovePriority = 1;
            _moveControl.TurnPriority = 1;
            _jumpControl.TurnPriority = 2;

            _gravity.GravityScale = 2;
            _gravity.SetVelocity(Vector3.zero);

            _isOnWall = false;
        }

        private void OnEndSliding()
        {
            _moveControl.MovePriority = 1;
            _moveControl.TurnPriority = 1;

            if (!_connectedSliderSurface) return;

            var targetVelocity = Vector3.ProjectOnPlane(Physics.gravity, _sliderCheck.Normal) *
                                 _connectedSliderSurface.SpeedMultiplier;
            targetVelocity.y += _connectedSliderSurface.JumpMultiplier;
            _endSlidingForce.SetVelocity(targetVelocity);

            if (targetVelocity.y > 0)
            {
                _animator.CrossFade("JumpStart", 0.05f);
            }
        }

        /// <summary>
        /// Private method which called on change move direction
        /// </summary>
        private void OnChangeMove()
        {
            _moveControl.Move(_inputMove);
        }

        /// <summary>
        /// Private method which called on change sprint
        /// </summary>
        private void OnChangeSprint()
        {
            _moveControl.MoveSpeed = _inputSprint ? runSpeed : walkSpeed;
        }

        /// <summary>
        /// Change move direction
        /// </summary>
        /// <param name="moveVector">New move direction</param>
        public void Move(Vector2 moveVector)
        {
            _inputMove = moveVector;
            OnChangeMove();
        }

        /// <summary>
        /// Change sprint state
        /// </summary>
        /// <param name="isSprint">New sprint state</param>
        public void Sprint(bool isSprint)
        {
            _inputSprint = isSprint;
            OnChangeSprint();
        }

        /// <summary>
        /// Performs a jump action for the player.
        /// </summary>
        /// <remarks>
        /// This method is called when the player wants to jump. It first checks if the player is currently hanging on a wall.
        /// If so, it resets the jump and performs a wall jump. If the player is not hanging on a wall, it simply performs a regular jump.
        /// </remarks>
        public void Jump()
        {
            if (_wallCheck.IsContact && !_groundCheck.IsOnGround)
            {
                OnEndWallHang();
                _jumpControl.ResetJump();
                _jumpControl.Jump(false);
            }
            else
            {
                _jumpControl.Jump();
            }
        }

        /// <summary>
        /// Called when the player starts a jump.
        /// </summary>
        /// <remarks>
        /// This method checks if the player is overhead and returns early if true.
        /// If the player is in contact with a surface and not on the ground, it calls the WallJumpHandler method.
        /// Otherwise, it calls the StandardJumpHandler method.
        /// Finally, it updates the animator's jump state.
        /// </remarks>
        public void OnStartedJump()
        {
            if (_headContactCheck.IsObjectOverhead) return;

            if (_wallCheck.IsContact && !_groundCheck.IsOnGround)
                WallJumpHandler();
            else
                StandardJumpHandler();

            UpdateAnimatorJumpState();
        }

        /// <summary>
        /// Called when the player contacts a wall.
        /// </summary>
        /// <remarks>
        /// This method checks if the player is on the ground and returns early if true.
        /// It calculates the angle between the player's forward direction and the perpendicular wall axis.
        /// The angle is used to determine if the wall is on the right side of the player.
        /// Finally, it calls the OnWallHang method to handle the wall hang behavior.
        /// </remarks>
        public void OnContactWall()
        {
            if (_groundCheck.IsOnGround) return;
            if (_cashedLastWallGameObject == _wallCheck.ContactedGameObject) return;

            _cashedLastWallGameObject = _wallCheck.ContactedGameObject;
            _connectedWallJumpSurface = _cashedLastWallGameObject.GetComponent<WallJumpSurface>();

            var wallNormalXZ = _wallCheck.Normal.ToXZVector2();
            var perpendicularWallAxis = Vector2.Perpendicular(wallNormalXZ).ToXZVector3();

            var angle = Vector3.SignedAngle(transform.forward, perpendicularWallAxis, Vector3.up);

            _wallOnRightSide = angle > 90;

            OnWallHang();
        }

        /// <summary>
        /// Called when the player stay contact a wall.
        /// </summary>
        public void OnStayContactWall()
        {
            if (!_groundCheck.IsOnGround) return;
            OnEndWallHang();
        }

        /// <summary>
        /// Resets the player's state after ending the wall hang behavior.
        /// </summary>
        public void OnLeftWall()
        {
            if (!_isOnWall) return;
            OnEndWallHang();
        }

        /// <summary>
        /// Called when the player contacts a sliding surface.
        /// </summary>
        public void OnContactSlider()
        {
            if (_groundCheck.IsOnGround) return;

            _connectedSliderSurface = _sliderCheck.ContactedGameObject.GetComponent<SlidingSurface>();

            _moveControl.MovePriority = 0;
            _moveControl.TurnPriority = 0;

            _isOnSlider = true;
        }

        /// <summary>
        /// Called when the player stay contact a sliding surface.
        /// </summary>
        public void OnStayContactSlider()
        {
            if (!_groundCheck.IsOnGround)
            {
                if (_connectedSliderSurface == null)
                {
                    _connectedSliderSurface = _sliderCheck.ContactedGameObject.GetComponent<SlidingSurface>();
                }

                _gravity.SetVelocity(Vector3.ProjectOnPlane(Physics.gravity, _sliderCheck.Normal) *
                                     _connectedSliderSurface.SpeedMultiplier);
            }
            else
            {
                OnEndSliding();
            }
        }

        /// <summary>
        /// Resets the player's state after ending the sliding behavior.
        /// </summary>
        public void OnLeftSlider()
        {
            if (!_isOnSlider) return;
            OnEndSliding();
        }
    }
}