using EventBusSystem;
using EventBusSystem.Signals.GameSignals;
using EventBusSystem.Signals.SceneSignals;
using Managers;
using NaughtyAttributes;
using ServiceLocatorSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Triggers
{
    public class FinishLevelTrigger : MonoBehaviour
    {
        [SerializeField, Scene] private int sceneToSwitch;
        [SerializeField] private LayerMask layersToReact;
        [SerializeField] private UnityEvent<bool, float> onTrigger;
        [SerializeField] private ExitCamera exitCamera;
        [SerializeField] private bool enableCustomDuration;

        [SerializeField, ShowIf(nameof(enableCustomDuration))]
        private float customTransitionDuration;

        private float _transitionDuration;
        private EventBus _eventBus;

        private bool _isTriggered;

        private void Awake()
        {
            _eventBus = ServiceLocator.Get<EventBus>();
            var gameManager = ServiceLocator.Get<GameManager>();
            _transitionDuration = gameManager.Settings.ExitCutsceneDuration;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isTriggered) return;
            if (layersToReact != (layersToReact | (1 << other.gameObject.layer))) return;

            _isTriggered = true;
            onTrigger.Invoke(true, enableCustomDuration ? customTransitionDuration : _transitionDuration);
            SwitchScene(enableCustomDuration ? customTransitionDuration : _transitionDuration);
        }

        private void SwitchScene(float duration)
        {
            _eventBus.Invoke(new OnExitCutsceneSignal(sceneToSwitch, duration, exitCamera));
        }
    }
}