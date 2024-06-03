using ServiceLocatorSystem;
using UnityEngine;
using UnityEngine.Events;
using Utils.Extra;
using Logger = Utils.Extra.Logger;

namespace EventBusSystem
{
    public class EventListenerComponent : MonoBehaviour
    {
        [SerializeField] private SignalEnum eventName;
        [SerializeField] private UnityEvent<EventModel> onEventRaised;

        protected EventBus EventBus;
        protected bool IsEventBusInitialized;

        protected virtual void Awake()
        {
            IsEventBusInitialized = ServiceLocator.TryGet(out EventBus);
            if (!IsEventBusInitialized)
            {
                Logger.Log(LoggerChannel.EventBus, Priority.Error,
                    $"{name} can't find EventBus. EventBehaviour will be offline");
                return;
            }

            EventBus.AddListener(eventName, this, Listen);
        }

        public void Listen(EventModel model)
        {
            onEventRaised.Invoke(model);
        }

        protected virtual void OnDestroy()
        {
            EventBus.RemoveListener(eventName, this, Listen);
        }
    }
}