using System;
using Baracuda.Monitoring;
using DavidFDev.DevConsole;
using Enums;
using EventBusSystem;
using EventBusSystem.Signals.DeveloperSignals;
using EventBusSystem.Signals.GameSignals;
using EventBusSystem.Signals.SceneSignals;
using Managers.Settings;
using ServiceLocatorSystem;
using UnityEngine;
using Logger = Utils.Extra.Logger;

namespace Managers
{
    [MTag("Game Manager")]
    [MGroupName("Game Manager")]
    public class GameManager : EventBehaviour, IService
    {
        [NonSerialized] public GameManagerSettings Settings;

        [Monitor] private GameState _gameState;

        [Monitor] public bool InDeveloperMode { get; private set; }

        private TransitionManager _transitionManager;

        protected override void Awake()
        {
            base.Awake();

            if (!PlayerPrefs.HasKey("DevConsole"))
            {
                DevConsole.DisableConsole();
            }

#if UNITY_EDITOR || DEBUG
            DevConsole.EnableConsole();
#endif

            _gameState = GameState.MainMenu;

            DevConsole.OnConsoleOpened += OnDevConsoleChangeState;
            DevConsole.OnConsoleClosed += OnDevConsoleChangeState;

            AddDevCommands();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            this.StopMonitoring();
            RemoveDevCommands();
        }

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
                    name: "true/false",
                    helpText: "Enable/disable logging"),
                callback: value => Logger.logOnlyErrors = value));

            DevConsole.AddCommand(Command.Create(
                name: "respawn",
                aliases: "resp",
                helpText: "Respawn player in spawnpoint without transition",
                callback: () => RaiseEvent(new OnDevRespawn())));
            DevConsole.AddCommand(Command.Create<bool>(
                name: "devmode",
                aliases: "debugmode",
                helpText: "Enable debugging info",
                p1:
                Parameter.Create(
                    name: "true/false",
                    helpText: "Enable/disable developer mode"),
                callback: value =>
                {
                    InDeveloperMode = value;
                    RaiseEvent(new OnDevModeChanged(InDeveloperMode));
                }));
        }

        private void RemoveDevCommands()
        {
            DevConsole.RemoveCommand("set_settings");
            DevConsole.RemoveCommand("logging");
            DevConsole.RemoveCommand("devmode");
        }

        [ListenTo(SignalEnum.OnExitCutscene)]
        private void OnExitCutscene(EventModel eventModel)
        {
            var payload = (OnExitCutscene)eventModel.Payload;
            RaiseEvent(new OnSetScene(payload.NextSceneID, payload.CutsceneDuration));
        }

        [ListenTo(SignalEnum.OnSceneLoaded)]
        private void OnSceneLoaded(EventModel eventModel)
        {
            DevConsole.CloseConsole();

            var payload = (OnSceneLoaded)eventModel.Payload;

            ChangeGameState(!payload.IsGameLevel ? GameState.MainMenu : GameState.Level);
        }

        [ListenTo(SignalEnum.OnPauseKeyPressed)]
        private void OnPauseKeyPressed(EventModel eventModel)
        {
            if (_transitionManager == null)
                _transitionManager = ServiceLocator.Get<TransitionManager>();

            if (_transitionManager.IsTransitionStarted) return;

            switch (_gameState)
            {
                case GameState.Level:
                    ChangeGameState(GameState.Paused);
                    break;
                case GameState.Paused:
                    ChangeGameState(GameState.Level);
                    break;
            }
        }

        [ListenTo(SignalEnum.OnGameStateChanged)]
        private void OnGameStateChanged(EventModel eventModel)
        {
            Time.timeScale = ((OnGameStateChanged)eventModel.Payload).GameState switch
            {
                GameState.MainMenu or GameState.Level => 1f,
                GameState.Paused => 0f,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        [ListenTo(SignalEnum.OnRespawnPlayer)]
        private void OnRespawnPlayer(EventModel eventModel)
        {
            ChangeGameState(GameState.Level);
        }

        [ListenTo(SignalEnum.OnSetScene)]
        private void OnSetScene(EventModel eventModel)
        {
            ChangeGameState(GameState.Level);
        }

        [ListenTo(SignalEnum.OnDevModeChanged)]
        private void OnDevModeChanged(EventModel eventModel)
        {
            var payload = (OnDevModeChanged)eventModel.Payload;
            if(payload.InDeveloperMode)
                this.StartMonitoring();
            else
                this.StopMonitoring();
        }

        private void OnDevConsoleChangeState()
        {
            RaiseEvent(new OnDevConsoleOpened(DevConsole.IsOpen));
        }

        private void ChangeGameState(GameState newGameState)
        {
            _gameState = newGameState;
            RaiseEvent(new OnGameStateChanged(_gameState));
        }
    }
}