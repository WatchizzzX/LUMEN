using DG.Tweening;
using EventBusSystem;
using EventBusSystem.Signals.GameSignals;
using ServiceLocatorSystem;
using UnityEngine;

namespace Animators
{
    public class FogAnimator : MonoBehaviour
    {
        [SerializeField] private Color offColor = Color.black;
        [SerializeField] private Color onColor = Color.green;

        private Material _material;
        private EventBus _eventBus;

        private void Awake()
        {
            var meshRenderer = GetComponent<MeshRenderer>();
            _material = meshRenderer.material;
            _material.color = offColor;
            _eventBus = ServiceLocator.Get<EventBus>();
            SubscribeToEventBus();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEventBus();
        }

        private void OnStartExitCutscene(OnStartExitCutsceneSignal signal)
        {
            _material.DOColor(onColor, signal.CutsceneDuration);
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