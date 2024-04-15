using EventBusSystem;
using EventBusSystem.Signals.GameSignals;
using PickupSystem;
using ServiceLocatorSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Triggers
{
    public class RespawnPlayerTrigger : MonoBehaviour
    {
        [SerializeField] private LayerMask layersToReact;
        [SerializeField] private LayerMask layersToCallEvent;
        [SerializeField] private UnityEvent<bool> onRespawnPlayer;
        [SerializeField] private UnityEvent onTriggered;
        [SerializeField] private float startTransitionDelay = 1f;

        private EventBus _eventBus;

        private void Awake()
        {
            _eventBus = ServiceLocator.Get<EventBus>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (layersToCallEvent == (layersToCallEvent | (1 << other.gameObject.layer)))
            {
                if (other.gameObject.TryGetComponent(out PickupObject pickupObject))
                {
                    pickupObject.Respawn();
                    onTriggered.Invoke();
                }
            }

            if (layersToReact != (layersToReact | (1 << other.gameObject.layer))) return;

            onRespawnPlayer.Invoke(true);
            _eventBus.Invoke(new OnRespawnPlayerSignal(startTransitionDelay));
        }
    }
}