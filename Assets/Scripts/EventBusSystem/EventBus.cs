using System;
using System.Collections;
using System.Reflection;
using EventBusSystem.Interfaces;
using ServiceLocatorSystem;
using UnityEngine;
using Utils;
using Logger = Utils.Logger;

namespace EventBusSystem
{
    public class EventBus : MonoBehaviour, IService
    {
        private EventsDictionary _eventActions;
        private ObjectsDictionary _objectEvents;

        public void Awake()
        {
            _eventActions = new EventsDictionary();
            _objectEvents = new ObjectsDictionary();
        }

        public void AddListener(SignalEnum eventName, object obj, Action<EventModel> listener)
        {
            Logger.Log(LoggerChannel.EventBus, Priority.Info, $"AddListener: {obj} is subscribed to {eventName}");

            RegisterModel(eventName, obj, listener.Method);
        }

        public void Register(object obj)
        {
            var methods = obj.GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var method in methods)
            {
                var attributes = Attribute.GetCustomAttributes(method, typeof(ListenToAttribute));
                foreach (ListenToAttribute attribute in attributes)
                {
                    if (attribute == null)
                        continue;

                    if (_objectEvents.IsRegistered(attribute.Event, obj, method))
                    {
                        Logger.Log(LoggerChannel.EventBus, Priority.Info,
                            $"{obj}.{method.Name} is already bound to {attribute.Event}");

                        continue;
                    }

                    RegisterModel(attribute.Event, obj, method, attribute.Priority);

                    Logger.Log(LoggerChannel.EventBus, Priority.Info,
                        $"{obj}.{method.Name} is bound to {attribute.Event}");
                }
            }
        }

        private void RegisterModel(SignalEnum eventName, object targetObject, MethodInfo method,
            int priority = 0)
        {
            _eventActions.Add(eventName, targetObject, method, priority);

            _objectEvents.Add(eventName, targetObject, method);
        }

        public void RaiseEvent(SignalEnum eventName, object sender = null,
            float delay = 0)
        {
            RaiseEvent(new EventModel(eventName, null, sender, delay));
        }

        public void RaiseEvent(ISignal payload, object sender = null, float delay = 0)
        {
            RaiseEvent(new EventModel(SignalDictionary.TypeToEnum[payload.GetType()], payload, sender, delay));
        }

        public void RaiseEvent(EventModel eventData)
        {
            if (eventData.Delay > 0)
                StartCoroutine(RaiseEventDelayed(eventData));
            else
                RaiseImmediately(eventData);
        }

        private void RaiseImmediately(EventModel eventData)
        {
            Logger.Log(LoggerChannel.EventBus, Priority.Info, $"Event Raised: {eventData.EventName}");

            foreach (var item in _eventActions.GetAllValues(eventData.EventName))
            {
                if (item.TargetObject == null)
                    // What about remove???
                    continue;

                item.Call(eventData);

                if (eventData.IsHandled)
                    break;
            }
        }

        private IEnumerator RaiseEventDelayed(EventModel eventData)
        {
            yield return new WaitForSeconds(eventData.Delay);

            RaiseImmediately(eventData);
        }

        public void RemoveListener(object obj)
        {
            if (_objectEvents.ContainsKey(obj) == false)
                return;

            foreach (var item in _objectEvents[obj])
            {
                _eventActions.Remove(item.Key, obj);
            }
        }

        public void RemoveListener(SignalEnum eventName, object obj, Action<EventModel> listener)
        {
            RemoveListener(eventName, obj, listener.Method);
        }

        public void RemoveListener(SignalEnum eventName, object obj, Action listener)
        {
            RemoveListener(eventName, obj, listener.Method);
        }

        private void RemoveListener(SignalEnum eventName, object obj, MethodInfo listener)
        {
            if (_objectEvents.Contains(eventName, obj) == false)
                return;

            var listeners = _objectEvents.GetMethods(eventName, obj);

            for (var j = 0; j < listeners.Count; j++)
            {
                if (listeners[j] != listener) continue;
                listeners.RemoveAt(j);
                break;
            }

            _eventActions.Remove(eventName, obj, listener);
        }

        private void OnDestroy()
        {
            _eventActions.Clear();
            _objectEvents.Clear();
        }
    }
}