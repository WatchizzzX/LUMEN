using System;
using Baracuda.Monitoring;
using Unity.TinyCharacterController.Check;
using Unity.TinyCharacterController.Control;
using Unity.TinyCharacterController.Effect;
using UnityEngine;

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

        private MoveControl _moveControl;
        private JumpControl _jumpControl;
        private Gravity _gravity;
        private GroundCheck _groundCheck;
        private LayerCheck _layerCheck;
        private ExtraForce _extraForce;

        [Monitor] private bool IsContactWall => _layerCheck.IsContact;
        [Monitor] private bool IsGrounded => _groundCheck.IsOnGround;
        [Monitor] private Gravity.State GravityState => _gravity.CurrentState;
        [Monitor] private int AerialJumpCount => _jumpControl.AerialJumpCount;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _moveControl = GetComponent<MoveControl>();
            _jumpControl = GetComponent<JumpControl>();
            _gravity = GetComponent<Gravity>();
            _groundCheck = GetComponent<GroundCheck>();
            _layerCheck = GetComponent<LayerCheck>();
            _extraForce = GetComponent<ExtraForce>();

#if DEBUG || UNITY_EDITOR
            Monitor.StartMonitoring(this);
#endif
        }

#if DEBUG || UNITY_EDITOR
        void OnScriptHotReload()
        {
            Monitor.StopMonitoring(this);
            Monitor.StartMonitoring(this);
        }
#endif

        private void Update()
        {
            UpdateAnimatorGroundMovementState();
        }

        private void UpdateAnimatorGroundMovementState()
        {
            _animator.SetFloat(Speed, _moveControl.CurrentSpeed);

            _animator.SetBool(IsGround, _groundCheck.IsOnGround && _gravity.FallSpeed <= 0);

            _animator.SetBool(IsMove, _moveControl.IsMove);
        }

        private void UpdateAnimatorJumpState()
        {
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

        private void WallJumpHandler()
        {
            _jumpControl.JumpDirection = _layerCheck.Normal + Vector3.up;

            _jumpControl.MovePriority = 0;
            _jumpControl.JumpHeight = 4;

            _jumpControl.TurnPriority = 0;

            _gravity.ResetVelocity();
        }

        private void JumpHandler()
        {
            _jumpControl.JumpDirection = Vector3.up;
            _jumpControl.JumpHeight = 2;

            _jumpControl.MovePriority = 0;
            _jumpControl.TurnPriority = 0;
        }

        private void OnChangeMove()
        {
            _moveControl.Move(_inputMove);
        }

        private void OnChangeSprint()
        {
            _moveControl.MoveSpeed = _inputSprint ? runSpeed : walkSpeed;
        }

        public void Move(Vector2 moveVector)
        {
            _inputMove = moveVector;
            OnChangeMove();
        }

        public void Sprint(bool isSprint)
        {
            _inputSprint = isSprint;
            OnChangeSprint();
        }

        public void Jump()
        {
            if (_layerCheck.IsContact)
                WallJumpHandler();
            else
                JumpHandler();

            _jumpControl.Jump();
        }

        public void OnStartedJump()
        {
            UpdateAnimatorJumpState();
        }

        public void OnLeftWall()
        {
            _jumpControl.MovePriority = 0;
            _jumpControl.TurnPriority = 0;

            _moveControl.MovePriority = 1;
            _moveControl.TurnPriority = 1;
        }

        public void OnMovingInWall()
        {
            _moveControl.MovePriority = 0;
            _moveControl.TurnPriority = 0;
        }

        public void OnContactWall()
        {
            _jumpControl.ResetJump();
        }
    }
}