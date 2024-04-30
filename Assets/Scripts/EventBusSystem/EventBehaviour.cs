using EventBusSystem.Interfaces;
using UnityEngine;
using Utils;
using Logger = Utils.Logger;

namespace EventBusSystem
{
    public abstract class EventBehaviour : MonoBehaviour
    {
<<<<<<< Updated upstream
        private bool _isEventBusInitialized;
        private EventBus _eventBus;

        protected virtual void Awake()
        {
            _isEventBusInitialized = ServiceLocatorSystem.ServiceLocator.TryGet(out _eventBus);

            if (_isEventBusInitialized)
=======
        protected bool IsEventBusInitialized;
        protected EventBus _eventBus;

        protected virtual void Awake()
        {
            IsEventBusInitialized = ServiceLocatorSystem.ServiceLocator.TryGet(out _eventBus);

            if (IsEventBusInitialized)
>>>>>>> Stashed changes
            {
                _eventBus.Register(this);
            }
            else
                Logger.Log(LoggerChannel.EventBus, Priority.Error,
                    $"{name} can't find EventBus. Work with EventBus will be stopped");
        }

        protected virtual void OnDestroy()
        {
<<<<<<< Updated upstream
            if (_isEventBusInitialized)
=======
            if (IsEventBusInitialized)
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
        protected virtual void RaiseEvent(ISignal payload)
=======
        protected virtual void RaiseEvent(Signal payload)
>>>>>>> Stashed changes
        {
            if (CheckEventBus())
                
                _eventBus.RaiseEvent(payload, this);
        }

<<<<<<< Updated upstream
        protected virtual void RaiseEvent(ISignal payload, float delay)
=======
        protected virtual void RaiseEvent(Signal payload, float delay)
>>>>>>> Stashed changes
        {
            if (CheckEventBus())
                _eventBus.RaiseEvent(payload, this, delay);
        }
        
        protected bool CheckEventBus()
        {
<<<<<<< Updated upstream
            if (_isEventBusInitialized)
=======
            if (IsEventBusInitialized)
>>>>>>> Stashed changes
                return true;
            Logger.Log(LoggerChannel.EventBus, Priority.Warning,
                $"{name} try to raise event, but EventBus on this object doesn't initialized. Please check initialize on {name}");
            return false;
        }
    }
}