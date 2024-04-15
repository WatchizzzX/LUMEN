using System;
using DavidFDev.DevConsole;
using EventBusSystem;
using EventBusSystem.Signals.DeveloperSignals;
using EventBusSystem.Signals.GameSignals;
using EventBusSystem.Signals.SceneSignals;
using Managers.Settings;
using ServiceLocatorSystem;
using UnityEngine;
using UnityEngine.Serialization;
using Logger = Utils.Logger;

namespace Managers
{
    public class GameManager : MonoBehaviour, IService
    {
        [NonSerialized] public GameManagerSettings Settings;

        private EventBus _eventBus;

        private void Awake()
        {
            _eventBus = ServiceLocator.Get<EventBus>();

            DevConsole.OnConsoleOpened += OnDevConsoleChangeState;
            DevConsole.OnConsoleClosed += OnDevConsoleChangeState;

            SubscribeToEventBus();

#if UNITY_EDITOR || DEBUG

            AddDevCommands();
#endif
        }

        private void OnDestroy()
        {
            UnsubscribeFromEventBus();
            
#if UNITY_EDITOR || DEBUG

            RemoveDevCommands();
#endif
        }

#if UNITY_EDITOR || DEBUG
        private void AddDevCommands()
        {
            DevConsole.AddCommand(Command.Create<GraphicChanger.GraphicsSettings>(
                name: "set_settings",
                aliases: "settings",
                helpText: "Set graphics settings",
                p1: Parameter.Create(
                    name: "Level of graphics", helpText: "Level of graphics"),
                callback: GraphicChanger.SetQuality));
            
            DevConsole.AddCommand(Command.Create<bool>(
                name: "logging",
                aliases: "log",
                helpText: "Enable logging messages",
                p1: Parameter.Create(
                    name: "bool",
                    helpText: "Enable logging"),
                callback: value => Logger.logOnlyErrors = value));
        }

        private void RemoveDevCommands()
        {
            DevConsole.RemoveCommand("set_settings");
            DevConsole.RemoveCommand("logging");
        }
#endif

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