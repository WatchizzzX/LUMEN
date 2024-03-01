using EventBusSystem;
using EventBusSystem.Signals.GameSignals;
using EventBusSystem.Signals.SceneSignals;
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
        [SerializeField] private float transitionDuration = 1f;
        [SerializeField] private UnityEvent<bool> onTrigger;

        private EventBus _eventBus;

        private bool _isTriggered;

        private void Awake()
        {
            _eventBus = ServiceLocator.Get<EventBus>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_isTriggered) return;
            if (layersToReact != (layersToReact | (1 << other.gameObject.layer))) return;
            
            _isTriggered = true;
            onTrigger.Invoke(true);
            SwitchScene();
        }

        private void SwitchScene()
        {
            _eventBus.Invoke(new OnStartExitCutsceneSignal(sceneToSwitch, transitionDuration));
        }
    }
}