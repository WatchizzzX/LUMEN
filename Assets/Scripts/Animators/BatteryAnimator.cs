using Animators.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace Animators
{
    /// <summary>
    /// Animator for battery
    /// </summary>
    public class BatteryAnimator : MonoBehaviour, IAnimator
    {
        #region Serialized Fields

        /// <summary>
        /// Color when battery is disabled
        /// </summary>
        [Tooltip("Color when battery is disabled")] [SerializeField]
        private Color offColor = Color.black;

        /// <summary>
        /// Color when battery is enabled
        /// </summary>
        [Tooltip("Color when battery is enabled")] [SerializeField]
        private Color onColor = Color.yellow;

        /// <summary>
        /// Duration of transition
        /// </summary>
        [Tooltip("Duration of transition")] [SerializeField]
        private float transitionDuration = 0.5f;

        /// <summary>
        /// VFX container to enable when is on
        /// </summary>
        [Tooltip("VFX container to enable when is on")] [SerializeField]
        private GameObject vfxContainer;

        #endregion

        #region Private Variables

        private bool _isEnabled;

        private MeshRenderer _meshRenderer;

        private Material _materialFirst;
        private Material _materialSecond;
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

        #endregion

        #region MonoBehaviour

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _materialFirst = _meshRenderer.materials[2];
            _materialSecond = _meshRenderer.materials[3];
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
            _materialFirst.DOColor(_isEnabled ? onColor : offColor, EmissionColor, duration);
            _materialSecond.DOColor(_isEnabled ? onColor : offColor, EmissionColor, duration);
            vfxContainer.SetActive(_isEnabled);
        }

        #endregion
    }
}