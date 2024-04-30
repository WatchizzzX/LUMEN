using ServiceLocatorSystem;
using UnityEngine;
using UnityEngine.Events;
using Utils;
using Logger = Utils.Logger;

namespace EventBusSystem
{
    public class EventListenerComponent : MonoBehaviour
    {
        [SerializeField] private SignalEnum eventName;
        [SerializeField] private UnityEvent<EventModel> onEventRaised;

<<<<<<< Updated upstream
        private EventBus _eventBus;
=======
        protected EventBus EventBus;
>>>>>>> Stashed changes
        protected bool IsEventBusInitialized;

        protected virtual void Awake()
        {
<<<<<<< Updated upstream
            IsEventBusInitialized = ServiceLocator.TryGet(out _eventBus);
=======
            IsEventBusInitialized = ServiceLocator.TryGet(out EventBus);
>>>>>>> Stashed changes
            if (!IsEventBusInitialized)
            {
                Logger.Log(LoggerChannel.EventBus, Priority.Error,
                    $"{name} can't find EventBus. EventBehaviour will be offline");
                return;
            }

<<<<<<< Updated upstream
            _eventBus.AddListener(eventName, this, Listen);
=======
            EventBus.AddListener(eventName, this, Listen);
>>>>>>> Stashed changes
        }

        public void Listen(EventModel model)
        {
            onEventRaised.Invoke(model);
        }

        protected virtual void OnDestroy()
        {
<<<<<<< Updated upstream
            _eventBus.RemoveListener(eventName, this, Listen);
=======
            EventBus.RemoveListener(eventName, this, Listen);
>>>>>>> Stashed changes
        }
    }
}