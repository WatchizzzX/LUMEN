using UnityEngine;

namespace Player
{
    /// <summary>
    /// Animator for player
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>
        /// Time to smooth transitions between walking and sprinting animations
        /// </summary>
        [Tooltip("Time to smooth transitions between walking and sprinting animations")]
        [SerializeField, Range(0f, 0.5f)]
        private float smoothTime;

        #endregion

        #region Private Variables

        /// <summary>
        /// Cached Animator
        /// </summary>
        private Animator _animator;

        /// <summary>
        /// Cached IMovementController realization
        /// </summary>
        private IMovementController _movementController;

        /// <summary>
        /// Cached current walking speed
        /// </summary>
        private float _walkingSpeed;

        /// <summary>
        /// Cached sprinting state
        /// </summary>
        private bool _isSprinting;

        /// <summary>
        /// Cached jumping state
        /// </summary>
        private bool _isJumping;

        /// <summary>
        /// Smoothed walking speed
        /// </summary>
        private float _smoothedWalkingSpeed;

        /// <summary>
        /// Velocity of change smoothedWalkingSpeed
        /// </summary>
        private float _smoothVelocity;

        /// <summary>
        /// Hash of forward parameter
        /// </summary>
        private static readonly int Forward = Animator.StringToHash("Forward");

        /// <summary>
        /// Hash of jumping parameter
        /// </summary>
        private static readonly int IsJumping = Animator.StringToHash("IsJumping");

        /// <summary>
        /// Hash of falling parameter
        /// </summary>
        private static readonly int IsFalling = Animator.StringToHash("IsFalling");

        #endregion

        #region MonoBehaviour

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
            _isJumping = state.IsJumping;
            _isSprinting = state.IsSprinting;

            SmoothValues();

            _animator.SetFloat(Forward, _smoothedWalkingSpeed);

            _animator.SetBool(IsFalling, state.IsFalling);
            _animator.SetBool(IsJumping, _isJumping);

            if (_isJumping && state.IsFalling)
            {
                _isJumping = false;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Smooth walking speed, based on smoothTime
        /// </summary>
        private void SmoothValues()
        {
            if (_smoothedWalkingSpeed < 0.001f)
            {
                _smoothedWalkingSpeed =
                    Mathf.SmoothDamp(_smoothedWalkingSpeed, _isSprinting ? _walkingSpeed * 2 : _walkingSpeed,
                        ref _smoothVelocity, 0f);
            }
            else
            {
                _smoothedWalkingSpeed =
                    Mathf.SmoothDamp(_smoothedWalkingSpeed, _isSprinting ? _walkingSpeed * 2 : _walkingSpeed,
                        ref _smoothVelocity, smoothTime);
            }
        }

        #endregion
    }
}