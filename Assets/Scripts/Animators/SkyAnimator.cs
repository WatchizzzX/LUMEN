using DG.Tweening;
using EventBusSystem;
using EventBusSystem.Signals.GameSignals;
using ServiceLocatorSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Animators
{
    public class SkyAnimator : MonoBehaviour
    {
        [SerializeField] private float offHorizontalLine = 0.8f;
        [SerializeField] private float onHorizontalLine;

        private Material _skyMaterial;
        private EventBus _eventBus;
        
        private static readonly int HorizontalLineContribution = Shader.PropertyToID("_HorizonLineContribution");

        private void Awake()
        {
            _skyMaterial = RenderSettings.skybox;
            _skyMaterial.SetFloat(HorizontalLineContribution, offHorizontalLine);
            _eventBus = ServiceLocator.Get<EventBus>();
            SubscribeToEventBus();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEventBus();
        }

        private void OnStartExitCutscene(OnStartExitCutsceneSignal signal)
        {
            _skyMaterial.DOFloat(onHorizontalLine, HorizontalLineContribution, signal.CutsceneDuration);
        }

        private void SubscribeToEventBus()
        {
            _eventBus.Subscribe<OnStartExitCutsceneSignal>(OnStartExitCutscene);
        }

        private void UnsubscribeFromEventBus()
        {
            _eventBus.Unsubscribe<OnStartExitCutsceneSignal>(OnStartExitCutscene);
        }
    }
}