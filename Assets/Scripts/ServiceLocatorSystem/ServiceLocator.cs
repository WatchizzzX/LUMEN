using System;
using System.Collections.Generic;
using Utils;
using Logger = Utils.Logger;

namespace ServiceLocatorSystem
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<string, IService> Managers = new();

        public static T Get<T>() where T : IService
        {
            var key = typeof(T).Name;
            if (Managers.TryGetValue(key, out var manager)) return (T)manager;

            Logger.Log(LoggerChannel.ServiceLocator, Priority.Error,
                $"{key} service not registered in ServiceLocator");
            throw new KeyNotFoundException($"{key} service not registered in ServiceLocator");
        }
        
        public static bool TryGet<T>(out T foundedService) where T : IService
        {
            var key = typeof(T).Name;
            if (Managers.TryGetValue(key, out var manager))
            {
                foundedService = (T)manager;
                return true;
            }

            Logger.Log(LoggerChannel.ServiceLocator, Priority.Error,
                $"{key} service not registered in ServiceLocator");
            foundedService = default;
            return false;
        }

        public static void Register<T>(T service) where T : IService
        {
            var key = typeof(T).Name;
            if (Managers.ContainsKey(key))
            {
                Logger.Log(LoggerChannel.ServiceLocator, Priority.Warning,
                    $"Attempted to register {key} service which is already registered");
                return;
            }

            Managers.Add(key, service);
            Logger.Log(LoggerChannel.ServiceLocator, Priority.Info, $"Successfully register {key} service");
        }

        public static void Unregister<T>() where T : IService
        {
            var key = typeof(T).Name;
            if (!Managers.ContainsKey(key))
            {
                Logger.Log(LoggerChannel.ServiceLocator, Priority.Warning,
                    $"Attempted to unregister {key} service which is not registered");
                return;
            }

            Managers.Remove(key);
            Logger.Log(LoggerChannel.ServiceLocator, Priority.Info, $"Successfully unregister {key} service");
        }

        public static void Unregister<T>(T service) where T : IService
        {
            var key = typeof(T).Name;
            if (!Managers.ContainsValue(service))
            {
                Logger.Log(LoggerChannel.ServiceLocator, Priority.Warning,
                    $"Attempted to unregister {service} of type {key} which is not registered");
                return;
            }

            Managers.Remove(key);
            Logger.Log(LoggerChannel.ServiceLocator, Priority.Info, $"Successfully unregister {key} service");
        }
    }
}