using System;
using DavidFDev.DevConsole;
using Enums;
using EventBusSystem;
using EventBusSystem.Signals.DeveloperSignals;
using EventBusSystem.Signals.GameSignals;
using EventBusSystem.Signals.InputSignals;
using EventBusSystem.Signals.SceneSignals;
using Managers.Settings;
using ServiceLocatorSystem;
using UnityEngine;
using Logger = Utils.Logger;

namespace Managers
{
    public class GameManager : MonoBehaviour, IService
    {
        [NonSerialized] public GameManagerSettings Settings;

        private EventBus _eventBus;

        private GameState _gameState;

        private void Awake()
        {
            _eventBus = ServiceLocator.Get<EventBus>();

            _gameState = GameState.Normal;

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
            _eventBus.Subscribe<OnExitCutsceneSignal>(OnExitCutscene);
            _eventBus.Subscribe<OnPauseKeyPressedSignal>(OnPauseKeyPressed);
            _eventBus.Subscribe<OnGameStateChangedSignal>(OnGameStateChanged);
            _eventBus.Subscribe<OnRespawnPlayerSignal>(OnRespawnPlayer);
            _eventBus.Subscribe<OnSetSceneSignal>(OnSetScene);
        }

        private void UnsubscribeFromEventBus()
        {
            _eventBus.Unsubscribe<OnSceneLoadedSignal>(OnSceneLoaded);
            _eventBus.Unsubscribe<OnExitCutsceneSignal>(OnExitCutscene);
            _eventBus.Unsubscribe<OnPauseKeyPressedSignal>(OnPauseKeyPressed);
            _eventBus.Unsubscribe<OnGameStateChangedSignal>(OnGameStateChanged);
            _eventBus.Unsubscribe<OnRespawnPlayerSignal>(OnRespawnPlayer);
            _eventBus.Unsubscribe<OnSetSceneSignal>(OnSetScene);
        }

        private void OnExitCutscene(OnExitCutsceneSignal signal)
        {
            _eventBus.Invoke(new OnSetSceneSignal(signal.NextSceneID, signal.CutsceneDuration));
        }

        private void OnSceneLoaded(OnSceneLoadedSignal signal)
        {
            DevConsole.CloseConsole();
        }

        private void OnPauseKeyPressed(OnPauseKeyPressedSignal signal)
        {
            ChangeGameState(_gameState switch
            {
                GameState.Normal => GameState.Paused,
                GameState.Paused => GameState.Normal,
                _ => _gameState
            });
        }

        private void OnGameStateChanged(OnGameStateChangedSignal signal)
        {
            switch (signal.GameState)
            {
                case GameState.Normal:
                    Time.timeScale = 1f;
                    break;
                case GameState.Paused:
                    Time.timeScale = 0f;
                    break;
            }
        }

        private void OnRespawnPlayer(OnRespawnPlayerSignal signal)
        {
            ChangeGameState(GameState.Normal);
        }

        private void OnSetScene(OnSetSceneSignal signal)
        {
            ChangeGameState(GameState.Normal);
        }

        private void OnDevConsoleChangeState()
        {
            _eventBus.Invoke(new OnDevConsoleOpenedSignal(DevConsole.IsOpen));
        }

        private void ChangeGameState(GameState newGameState)
        {
            _gameState = newGameState;
            _eventBus.Invoke(new OnGameStateChangedSignal(_gameState));
        }
    }
}