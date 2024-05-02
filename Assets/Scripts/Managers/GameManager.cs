using System;
using DavidFDev.DevConsole;
using Enums;
using EventBusSystem;
using EventBusSystem.Signals.DeveloperSignals;
using EventBusSystem.Signals.GameSignals;
using EventBusSystem.Signals.SceneSignals;
using Managers.Settings;
using ServiceLocatorSystem;
using UnityEngine;
using Logger = Utils.Logger;

namespace Managers
{
    public class GameManager : EventBehaviour, IService
    {
        [NonSerialized] public GameManagerSettings Settings;

        private GameState _gameState;

        private TransitionManager _transitionManager;

        protected override void Awake()
        {
            base.Awake();

            if (!PlayerPrefs.HasKey("DevConsole"))
            {
                DevConsole.DisableConsole();
            }

            _gameState = GameState.Normal;

            DevConsole.OnConsoleOpened += OnDevConsoleChangeState;
            DevConsole.OnConsoleClosed += OnDevConsoleChangeState;

#if UNITY_EDITOR || DEBUG

            AddDevCommands();
#endif
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

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
        }

        [ListenTo(SignalEnum.OnPauseKeyPressed)]
        private void OnPauseKeyPressed(EventModel eventModel)
        {
            if (_transitionManager == null)
                _transitionManager = ServiceLocator.Get<TransitionManager>();

            if (_transitionManager.IsTransitionStarted) return;
            
            ChangeGameState(_gameState switch
            {
                GameState.Normal => GameState.Paused,
                GameState.Paused => GameState.Normal,
                _ => _gameState
            });
        }

        [ListenTo(SignalEnum.OnGameStateChanged)]
        private void OnGameStateChanged(EventModel eventModel)
        {
            switch (((OnGameStateChanged)eventModel.Payload).GameState)
            {
                case GameState.Normal:
                    Time.timeScale = 1f;
                    break;
                case GameState.Paused:
                    Time.timeScale = 0f;
                    break;
            }
        }

        [ListenTo(SignalEnum.OnRespawnPlayer)]
        private void OnRespawnPlayer(EventModel eventModel)
        {
            ChangeGameState(GameState.Normal);
        }

        [ListenTo(SignalEnum.OnSetScene)]
        private void OnSetScene(EventModel eventModel)
        {
            ChangeGameState(GameState.Normal);
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