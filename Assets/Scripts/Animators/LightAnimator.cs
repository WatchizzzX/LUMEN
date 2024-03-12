using DG.Tweening;
using UnityEngine;
using Animators.Interfaces;

namespace Animators
{
    public class LightAnimator : MonoBehaviour , IAnimator
    {
        #region Serialized Fields

        [SerializeField] private float targetIntensity;
        [SerializeField] private float targetRange;
        [SerializeField] private Color targetColor;
        [SerializeField] private Light targetLightSource;

        /// <summary>
        /// Duration of transition
        /// </summary>
        [Tooltip("Duration of transition")] [SerializeField]
        private float transitionDuration = 0.5f;

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
            DOTween.To(() => targetLightSource.range, x => targetLightSource.range = x, _isEnabled ? targetRange : 0,
                duration);
            targetLightSource.DOIntensity(_isEnabled ? targetIntensity : 0, duration);
            targetLightSource.DOColor(_isEnabled ? targetColor : Color.black, duration);
        }

        #endregion
    }
}