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
    public class GameManager : EventBehaviour, IService
    {
        [NonSerialized] public GameManagerSettings Settings;

        private GameState _gameState;

        protected override void Awake()
        {
            base.Awake();

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

<<<<<<< Updated upstream
<<<<<<< Updated upstream
        private void SubscribeToEventBus()
=======
        [ListenTo(SignalEnum.OnExitCutsceneSignal)]
        public void OnExitCutscene(EventModel eventModel)
>>>>>>> Stashed changes
        {
            var payload = (OnExitCutsceneSignal)eventModel.Payload;
            RaiseEvent(new OnSetSceneSignal(payload.NextSceneID, payload.CutsceneDuration));
        }

<<<<<<< Updated upstream
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
=======
        [ListenTo(SignalEnum.OnExitCutscene)]
        private void OnExitCutscene(EventModel eventModel)
        {
            var payload = (OnExitCutscene)eventModel.Payload;
            RaiseEvent(new OnSetScene(payload.NextSceneID, payload.CutsceneDuration));
        }

        [ListenTo(SignalEnum.OnSceneLoaded)]
        private void OnSceneLoaded(EventModel eventModel)
>>>>>>> Stashed changes
=======
        [ListenTo(SignalEnum.OnSceneLoadedSignal)]
        public void OnSceneLoaded(EventModel eventModel)
>>>>>>> Stashed changes
        {
            DevConsole.CloseConsole();
        }

<<<<<<< Updated upstream
<<<<<<< Updated upstream
        private void OnPauseKeyPressed(OnPauseKeyPressedSignal signal)
=======
        [ListenTo(SignalEnum.OnPauseKeyPressed)]
        private void OnPauseKeyPressed(EventModel eventModel)
>>>>>>> Stashed changes
=======
        [ListenTo(SignalEnum.OnPauseKeyPressedSignal)]
        public void OnPauseKeyPressed(EventModel eventModel)
>>>>>>> Stashed changes
        {
            ChangeGameState(_gameState switch
            {
                GameState.Normal => GameState.Paused,
                GameState.Paused => GameState.Normal,
                _ => _gameState
            });
        }

<<<<<<< Updated upstream
<<<<<<< Updated upstream
        private void OnGameStateChanged(OnGameStateChangedSignal signal)
        {
            switch (signal.GameState)
=======
        [ListenTo(SignalEnum.OnGameStateChanged)]
        private void OnGameStateChanged(EventModel eventModel)
        {
            switch (((OnGameStateChanged)eventModel.Payload).GameState)
>>>>>>> Stashed changes
=======
        [ListenTo(SignalEnum.OnGameStateChangedSignal)]
        public void OnGameStateChanged(EventModel eventModel)
        {
            switch (((OnGameStateChangedSignal)eventModel.Payload).GameState)
>>>>>>> Stashed changes
            {
                case GameState.Normal:
                    Time.timeScale = 1f;
                    break;
                case GameState.Paused:
                    Time.timeScale = 0f;
                    break;
            }
        }

<<<<<<< Updated upstream
<<<<<<< Updated upstream
        private void OnRespawnPlayer(OnRespawnPlayerSignal signal)
=======
        [ListenTo(SignalEnum.OnRespawnPlayer)]
        private void OnRespawnPlayer(EventModel eventModel)
>>>>>>> Stashed changes
=======
        [ListenTo(SignalEnum.OnRespawnPlayerSignal)]
        public void OnRespawnPlayer(EventModel eventModel)
>>>>>>> Stashed changes
        {
            ChangeGameState(GameState.Normal);
        }

<<<<<<< Updated upstream
<<<<<<< Updated upstream
        private void OnSetScene(OnSetSceneSignal signal)
=======
        [ListenTo(SignalEnum.OnSetScene)]
        private void OnSetScene(EventModel eventModel)
>>>>>>> Stashed changes
=======
        [ListenTo(SignalEnum.OnSetSceneSignal)]
        public void OnSetScene(EventModel eventModel)
>>>>>>> Stashed changes
        {
            ChangeGameState(GameState.Normal);
        }

        private void OnDevConsoleChangeState()
        {
<<<<<<< Updated upstream
<<<<<<< Updated upstream
            _eventBus.Invoke(new OnDevConsoleOpenedSignal(DevConsole.IsOpen));
=======
            RaiseEvent(new OnDevConsoleOpened(DevConsole.IsOpen));
>>>>>>> Stashed changes
=======
            RaiseEvent(new OnDevConsoleOpenedSignal(DevConsole.IsOpen));
>>>>>>> Stashed changes
        }

        private void ChangeGameState(GameState newGameState)
        {
            _gameState = newGameState;
<<<<<<< Updated upstream
<<<<<<< Updated upstream
            _eventBus.Invoke(new OnGameStateChangedSignal(_gameState));
=======
            RaiseEvent(new OnGameStateChanged(_gameState));
>>>>>>> Stashed changes
=======
            RaiseEvent(new OnGameStateChangedSignal(_gameState));
>>>>>>> Stashed changes
        }
    }
}