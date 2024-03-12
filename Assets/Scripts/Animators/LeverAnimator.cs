using Animators.Interfaces;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Animators
{
    /// <summary>
    /// Animator for lever
    /// </summary>
    public class LeverAnimator : MonoBehaviour, IAnimator
    {
        #region Serialized Fields

        /// <summary>
        /// Transform of lever handle
        /// </summary>
        [Tooltip("Transform of lever handle")] [SerializeField]
        private Transform leverTransform;

        /// <summary>
        /// Angle of handle when is off
        /// </summary>
        [Tooltip("Angle of handle when is off")] [SerializeField]
        private float offAngle;

        /// <summary>
        /// Angle of handle when is on
        /// </summary>
        [Tooltip("Angle of handle when is on")] [SerializeField]
        private float onAngle;

        /// <summary>
        /// Duration of transition
        /// </summary>
        [Tooltip("Duration of transition")] [SerializeField]
        private float transitionDuration;

        #endregion

        #region Private Variables

        private bool _isEnabled;

        #endregion

        #region Methods

        public void Animate()
        {
            _isEnabled = !_isEnabled;
            StartAnimation(transitionDuration);
        }

        public void Animate(bool value)
        {
            if (_isEnabled == value) return;
            _isEnabled = value;
            StartAnimation(transitionDuration);
        }

        public void Animate(bool value, float duration)
        {
            if (_isEnabled == value) return;
            _isEnabled = value;
            StartAnimation(duration);
        }

        private void StartAnimation(float duration)
        {
            leverTransform.DOLocalRotate(_isEnabled ? new Vector3(onAngle, 0f, 0f) : new Vector3(offAngle, 0f, 0f),
                duration);
        }

        #endregion
    }
}