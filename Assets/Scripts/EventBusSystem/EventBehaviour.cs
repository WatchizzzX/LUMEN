using EventBusSystem.Interfaces;
using UnityEngine;
using Utils.Extra;
using Logger = Utils.Extra.Logger;

namespace EventBusSystem
{
    public abstract class EventBehaviour : MonoBehaviour
    {
        protected bool IsEventBusInitialized;
        protected EventBus _eventBus;

        protected virtual void Awake()
        {
            IsEventBusInitialized = ServiceLocatorSystem.ServiceLocator.TryGet(out _eventBus);

            if (IsEventBusInitialized)
            {
                _eventBus.Register(this);
            }
            else
                Logger.Log(LoggerChannel.EventBus, Priority.Error,
                    $"{name} can't find EventBus. Work with EventBus will be stopped");
        }

        protected virtual void OnDestroy()
        {
            if (IsEventBusInitialized)
                _eventBus.RemoveListener(this);
        }

        protected virtual void RaiseEvent(SignalEnum eventName)
        {
            if (CheckEventBus())
                _eventBus.RaiseEvent(eventName, this);
        }
        
        protected virtual void RaiseEvent(SignalEnum eventName, float delay)
        {
            if (CheckEventBus())
                _eventBus.RaiseEvent(eventName, this, delay);
        }

        protected virtual void RaiseEvent(ISignal payload)
        {
            if (CheckEventBus())
                
                _eventBus.RaiseEvent(payload, this);
        }

        protected virtual void RaiseEvent(ISignal payload, float delay)
        {
            if (CheckEventBus())
                _eventBus.RaiseEvent(payload, this, delay);
        }
        
        protected bool CheckEventBus()
        {
            if (IsEventBusInitialized)
                return true;
            Logger.Log(LoggerChannel.EventBus, Priority.Warning,
                $"{name} try to raise event, but EventBus on this object doesn't initialized. Please check initialize on {name}");
            return false;
        }
    }
}