using System;
using Baracuda.Monitoring;
using Unity.TinyCharacterController.Check;
using Unity.TinyCharacterController.Control;
using Unity.TinyCharacterController.Effect;
using UnityEngine;

[SelectionBase]
public class PlayerControllerTcc : MonoBehaviour
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
    private WallCheck _wallCheck;
    private ExtraForce _extraForce;

    [Monitor] private Vector3 WallNormal => _wallCheck.Normal;
    [Monitor] private Vector3 InvertWallNormal => _wallCheck.Normal * -1;
    [Monitor] private Vector3 MovementForceOnWallAxis => Vector3.Project(_moveControl.Velocity, InvertWallNormal);

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _moveControl = GetComponent<MoveControl>();
        _jumpControl = GetComponent<JumpControl>();
        _gravity = GetComponent<Gravity>();
        _groundCheck = GetComponent<GroundCheck>();
        _wallCheck = GetComponent<WallCheck>();
        _extraForce = GetComponent<ExtraForce>();
        Monitor.StartMonitoring(this);
    }

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

    private void WallRunHandler()
    {
        //_gravity.GravityScale = 0;
        _gravity.SetVelocity(Vector3.zero);

        _moveControl.Velocity -= MovementForceOnWallAxis;
        _moveControl.Velocity += InvertWallNormal;

        //_extraForce.SetVelocity();
        _moveControl.MovePriority = 1;
        _moveControl.TurnPriority = 1;
    }
    
    private void WallJumpHandler()
    {
        _jumpControl.JumpDirection = WallNormal + Vector3.up;
        
        _jumpControl.MovePriority = 0;
        _jumpControl.TurnPriority = 5;
    }

    private void JumpHandler()
    {
        _jumpControl.JumpDirection = Vector3.up;

        //Allow to move in air
        _jumpControl.MovePriority = 0;
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
        if (_wallCheck.IsContact)
            WallJumpHandler();
        else
            JumpHandler();
        
        _jumpControl.Jump();
    }

    public void OnStartedJump()
    {
       UpdateAnimatorJumpState();
    }

    public void OnWallStuck()
    {
        //WallRunHandler();
    }

    public void OnWallLeft()
    {
        /*_gravity.GravityScale = 2;

        _moveControl.MovePriority = 1;
        _moveControl.TurnPriority = 1;*/
    }
}