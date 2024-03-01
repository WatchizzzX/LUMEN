using EventBusSystem;
using EventBusSystem.Signals.GameSignals;
using ServiceLocatorSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Triggers
{
    public class RespawnPlayerTrigger : MonoBehaviour
    {
        [SerializeField] private LayerMask layersToReact;
        [SerializeField] private UnityEvent<bool> onRespawnPlayer;

        private EventBus _eventBus;

        private void Awake()
        {
            _eventBus = ServiceLocator.Get<EventBus>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (layersToReact != (layersToReact | (1 << other.gameObject.layer))) return;
            
            onRespawnPlayer.Invoke(true);
            _eventBus.Invoke(new OnRespawnPlayerSignal());
        }
    }
}