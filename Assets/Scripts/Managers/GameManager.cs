using System;
using DavidFDev.DevConsole;
using EventBusSystem;
using EventBusSystem.Signals.DeveloperSignals;
using EventBusSystem.Signals.GameSignals;
using EventBusSystem.Signals.SceneSignals;
using Managers.Settings;
using ServiceLocatorSystem;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour, IService
    {
        public GameManagerSettings gameManagerSettings;
        
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
            _eventBus.Subscribe<OnStartExitCutsceneSignal>(OnStartExitCutscene);
        }

        private void UnsubscribeFromEventBus()
        {
            _eventBus.Unsubscribe<OnSceneLoadedSignal>(OnSceneLoaded);
            _eventBus.Unsubscribe<OnStartExitCutsceneSignal>(OnStartExitCutscene);
        }

        private void OnStartExitCutscene(OnStartExitCutsceneSignal signal)
        {
            _eventBus.Invoke(new OnSetSceneSignal(signal.NextSceneID, signal.CutsceneDuration));
        }

        private void OnSceneLoaded(OnSceneLoadedSignal signal)
        {
            DevConsole.CloseConsole();
        }

        private void OnDevConsoleChangeState()
        {
            _eventBus.Invoke(new OnDevConsoleOpenedSignal(DevConsole.IsOpen));
        }
    }
}