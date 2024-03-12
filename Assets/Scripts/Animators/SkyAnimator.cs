using Animators.Interfaces;
using DG.Tweening;
using EventBusSystem;
using EventBusSystem.Signals.GameSignals;
using ServiceLocatorSystem;
using UnityEngine;

namespace Animators
{
    public class SkyAnimator : MonoBehaviour, IAnimator
    {
        #region SerializedFields

        [SerializeField] private float offSunDiscMultiplier;
        [SerializeField] private float onSunDiscMultiplier;
        [SerializeField] private float offHorizontalLineContribution;
        [SerializeField] private float onHorizontalLineContribution;
        [SerializeField] private float transitionDuration;

        #endregion

        #region Private Variables

        private bool _isEnabled;

        private Material _skyMaterial;
        private EventBus _eventBus;

        private float _cachedHorizontalLineContribution;
        private float _cachedSunDiskMultiplier;

        private static readonly int HorizontalLineContribution = Shader.PropertyToID("_HorizonLineContribution");
        private static readonly int SunDiscMultiplier = Shader.PropertyToID("_SunDiscMultiplier");

        #endregion

        #region MonoBehaviour

        private void Awake()
        {
            _skyMaterial = RenderSettings.skybox;

            _cachedHorizontalLineContribution = _skyMaterial.GetFloat(HorizontalLineContribution);
            _cachedSunDiskMultiplier = _skyMaterial.GetFloat(SunDiscMultiplier);

            _skyMaterial.SetFloat(HorizontalLineContribution, offHorizontalLineContribution);
            _skyMaterial.SetFloat(SunDiscMultiplier, offSunDiscMultiplier);

            _eventBus = ServiceLocator.Get<EventBus>();
            SubscribeToEventBus();
        }

        private void OnDestroy()
        {
            _skyMaterial.SetFloat(HorizontalLineContribution, _cachedHorizontalLineContribution);
            _skyMaterial.SetFloat(SunDiscMultiplier, _cachedSunDiskMultiplier);
            UnsubscribeFromEventBus();
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
            _skyMaterial.DOFloat(onHorizontalLineContribution, HorizontalLineContribution, duration);
            _skyMaterial.DOFloat(onSunDiscMultiplier, SunDiscMultiplier, duration);
        }

        private void OnStartExitCutscene(OnStartExitCutsceneSignal signal)
        {
            StartAnimation(signal.CutsceneDuration);
        }

        private void SubscribeToEventBus()
        {
            _eventBus.Subscribe<OnStartExitCutsceneSignal>(OnStartExitCutscene);
        }

        private void UnsubscribeFromEventBus()
        {
            _eventBus.Unsubscribe<OnStartExitCutsceneSignal>(OnStartExitCutscene);
        }

        #endregion
    }
}