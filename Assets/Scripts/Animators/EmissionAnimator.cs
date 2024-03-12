using Animators.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace Animators
{
    public class EmissionAnimator: MonoBehaviour, IAnimator
    {
        #region Serialized Fields

        [SerializeField] private Color offColor = Color.black;
        [SerializeField] private Color onColor = Color.green;
        [SerializeField] private float transitionDuration = 1.5f;
        [SerializeField] private Ease easing;

        #endregion

        #region Private Variables

        private bool _isEnabled;
        
        private Material _material;
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
        
        #endregion

        #region MonoBehaviour
        
        private void Awake()
        {
            var meshRenderer = GetComponent<MeshRenderer>();
            _material = meshRenderer.material;
            _material.SetColor(EmissionColor,  offColor);
        }

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
            _material.DOColor(_isEnabled ? onColor : offColor, EmissionColor, duration).SetEase(easing);
        }

        #endregion
    }
}