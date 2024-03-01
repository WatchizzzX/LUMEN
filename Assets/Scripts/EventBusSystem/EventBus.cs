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

            foreach (var callback in signalCallback.Select(obj => obj.Callback as Action<T>))
            {
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
}