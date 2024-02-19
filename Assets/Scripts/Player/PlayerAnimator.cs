using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField, Range(0f, 0.5f)] private float smoothTime;

        private Animator _animator;
        private IMovementController _movementController;

        private float _walkingSpeed;
        private bool _isSprinting;
        private bool _isJumping;

        private float _smoothedWalkingSpeed;
        private float _smoothVelocity;

        private static readonly int Forward = Animator.StringToHash("Forward");
        private static readonly int IsJumping = Animator.StringToHash("IsJumping");
        private static readonly int IsFalling = Animator.StringToHash("IsFalling");

        private void Awake()
        {
            _animator = GetComponent<Animator>();

            if (!TryGetComponent(out _movementController))
            {
                //TODO: Add logging
                Debug.Log("Can't find component that realize IMovementController");
            }
        }

        private void Update()
        {
            if (_movementController == null) return;

            var state = _movementController.GetMovementState();

            _walkingSpeed = state.RelativeWalkSpeed;

            SmoothValues();

            _animator.SetFloat(Forward, _smoothedWalkingSpeed);

            _animator.SetBool(IsFalling, state.IsFalling);
            _animator.SetBool(IsJumping, _isJumping);

            if (_isJumping && state.IsFalling)
            {
                _isJumping = false;
            }
        }

        private void SmoothValues()
        {
            _smoothedWalkingSpeed =
                Mathf.SmoothDamp(_smoothedWalkingSpeed, _isSprinting ? _walkingSpeed * 2 : _walkingSpeed,
                    ref _smoothVelocity, smoothTime);
        }

        public void SetSprint(bool sprint)
        {
            _isSprinting = sprint;
        }

        public void CallToJump()
        {
            _isJumping = true;
        }
    }
}