using System;
using DavidFDev.DevConsole;
using EventBusSystem;
using EventBusSystem.Signals.DeveloperSignals;
using EventBusSystem.Signals.SceneSignals;
using ServiceLocatorSystem;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour, IService
    {
        private EventBus _eventBus;

        private void Awake()
        {
            _eventBus = ServiceLocator.Get<EventBus>();

            DevConsole.OnConsoleOpened += OnDevConsoleChangeState;
            DevConsole.OnConsoleClosed += OnDevConsoleChangeState;
            
            SubscribeToEventBus();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEventBus();
        }

        private void SubscribeToEventBus()
        {
            _eventBus.Subscribe<OnSceneLoadedSignal>(OnSceneLoaded);
        }

        private void UnsubscribeFromEventBus()
        {
            _eventBus.Unsubscribe<OnSceneLoadedSignal>(OnSceneLoaded);
        }

        private void OnSceneLoaded(OnSceneLoadedSignal signal)
        {
            DevConsole.CloseConsole();
        }

        private void OnDevConsoleChangeState()
        {
            _eventBus.Invoke(new DevConsoleSignal(DevConsole.IsOpen));
        }
    }
}