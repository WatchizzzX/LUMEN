<<<<<<< Updated upstream
<<<<<<< Updated upstream
using System;
using System.Collections.Generic;
using System.Linq;
using EventBusSystem.Interfaces;
using EventBusSystem.Signals;
using ServiceLocatorSystem;
using Utils;
using Logger = Utils.Logger;

namespace EventBusSystem
{
    public class EventBus : IService
    {
        private Dictionary<string, List<CallbackWithPriority>> _signalCallbacks = new();

        public void Subscribe<T>(Action<T> callback, int priority = 0) where T : ISignal
        {
            var key = typeof(T).Name;
            if (_signalCallbacks.ContainsKey(key))
            {
                _signalCallbacks[key].Add(new CallbackWithPriority(priority, callback));
            }
            else
            {
                _signalCallbacks.Add(key, new List<CallbackWithPriority> { new(priority, callback) });
            }

            _signalCallbacks[key] = _signalCallbacks[key].OrderByDescending(x => x.Priority).ToList();
            var methodInfo = callback.Method;

            Logger.Log(LoggerChannel.EventBus, Priority.Info,
                $"{methodInfo.DeclaringType?.Name} register {methodInfo.Name} handler with {priority} priority for {key}");
        }

        public void Invoke<T>(T signal) where T : ISignal
        {
            var key = typeof(T).Name;
            Logger.Log(LoggerChannel.EventBus, Priority.Info, $"Invoked {key}");
            if (!_signalCallbacks.TryGetValue(key, out var signalCallback)) return;

            foreach (var obj in signalCallback.OrderBy(obj => obj.Priority))
            {
                var callback = obj.Callback as Action<T>;
                callback?.Invoke(signal);
            }
        }

        public void Unsubscribe<T>(Action<T> callback) where T : ISignal
        {
            var key = typeof(T).Name;
            var methodInfo = callback.Method;
            if (_signalCallbacks.ContainsKey(key))
            {
                var callbackToDelete = _signalCallbacks[key].FirstOrDefault(x => x.Callback.Equals(callback));
                if (callbackToDelete != null)
                {
                    _signalCallbacks[key].Remove(callbackToDelete);
                }

                Logger.Log(LoggerChannel.EventBus, Priority.Info,
                    $"{methodInfo.DeclaringType?.Name} unregister {methodInfo.Name} handler for {key}");
            }
            else
            {
                Logger.Log(LoggerChannel.EventBus, Priority.Error,
                    $"Can't unsubscribe {methodInfo.Name} of {methodInfo.DeclaringType?.Name} from {key} signal, because this signal doesn't register in EventBus");
            }
        }
    }
=======
=======
>>>>>>> Stashed changes
﻿using System;
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
<<<<<<< Updated upstream
=======
            FindAll();
        }

        private void FindAll()
        {
            var allMonoBehavior = FindObjectsOfType<MonoBehaviour>(true);
            foreach (var item in allMonoBehavior)
            {
                //Register(item);
            }
>>>>>>> Stashed changes
        }

        public void AddListener(SignalEnum eventName, object obj, Action<EventModel> listener)
        {
            Logger.Log(LoggerChannel.EventBus, Priority.Info, $"AddListener: {obj} is subscribed to {eventName}");

            RegisterModel(eventName, obj, listener.Method);
        }

        public void Register(object obj)
        {
<<<<<<< Updated upstream
            var methods = obj.GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
=======
            var methods = obj.GetType().GetMethods();
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
        public void RaiseEvent(ISignal payload, object sender = null, float delay = 0)
=======
        public void RaiseEvent(Signal payload, object sender = null, float delay = 0)
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
}