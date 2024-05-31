////////////////////////////////////////////////////////////////////////////////
// This code is auto-generated. Please don't change this code to avoid errors //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;

namespace EventBusSystem
{
    public static class SignalDictionary
    {
        public static readonly Dictionary<Type, SignalEnum> TypeToEnum = new()
        {
            {typeof(EventBusSystem.Signals.TransitionSignals.OnChangeTransitionState), SignalEnum.OnChangeTransitionState},
            {typeof(EventBusSystem.Signals.SceneSignals.OnSceneLoaded), SignalEnum.OnSceneLoaded},
            {typeof(EventBusSystem.Signals.SceneSignals.OnSetScene), SignalEnum.OnSetScene},
            {typeof(EventBusSystem.Signals.InputSignals.OnPauseKeyPressed), SignalEnum.OnPauseKeyPressed},
            {typeof(EventBusSystem.Signals.GameSignals.OnExitCutscene), SignalEnum.OnExitCutscene},
            {typeof(EventBusSystem.Signals.GameSignals.OnGameStateChanged), SignalEnum.OnGameStateChanged},
            {typeof(EventBusSystem.Signals.GameSignals.OnLevelCameraChange), SignalEnum.OnLevelCameraChange},
            {typeof(EventBusSystem.Signals.GameSignals.OnRespawnPlayer), SignalEnum.OnRespawnPlayer},
            {typeof(EventBusSystem.Signals.GameSignals.OnSpawnPlayer), SignalEnum.OnSpawnPlayer},
            {typeof(EventBusSystem.Signals.DeveloperSignals.OnDevConsoleOpened), SignalEnum.OnDevConsoleOpened},
            {typeof(EventBusSystem.Signals.DeveloperSignals.OnDevRespawn), SignalEnum.OnDevRespawn},
        };

        public static readonly Dictionary<SignalEnum, Type> EnumToType = new()
        {
            {SignalEnum.OnChangeTransitionState, typeof(EventBusSystem.Signals.TransitionSignals.OnChangeTransitionState)},
            {SignalEnum.OnSceneLoaded, typeof(EventBusSystem.Signals.SceneSignals.OnSceneLoaded)},
            {SignalEnum.OnSetScene, typeof(EventBusSystem.Signals.SceneSignals.OnSetScene)},
            {SignalEnum.OnPauseKeyPressed, typeof(EventBusSystem.Signals.InputSignals.OnPauseKeyPressed)},
            {SignalEnum.OnExitCutscene, typeof(EventBusSystem.Signals.GameSignals.OnExitCutscene)},
            {SignalEnum.OnGameStateChanged, typeof(EventBusSystem.Signals.GameSignals.OnGameStateChanged)},
            {SignalEnum.OnLevelCameraChange, typeof(EventBusSystem.Signals.GameSignals.OnLevelCameraChange)},
            {SignalEnum.OnRespawnPlayer, typeof(EventBusSystem.Signals.GameSignals.OnRespawnPlayer)},
            {SignalEnum.OnSpawnPlayer, typeof(EventBusSystem.Signals.GameSignals.OnSpawnPlayer)},
            {SignalEnum.OnDevConsoleOpened, typeof(EventBusSystem.Signals.DeveloperSignals.OnDevConsoleOpened)},
            {SignalEnum.OnDevRespawn, typeof(EventBusSystem.Signals.DeveloperSignals.OnDevRespawn)},
        };

        public static readonly Dictionary<Type, Type> TypeToSerializedType = new()
        {
            {typeof(EventBusSystem.Signals.TransitionSignals.OnChangeTransitionState), typeof(EventBusSystem.SerializedSignals.SerializedOnChangeTransitionState)},
            {typeof(EventBusSystem.Signals.SceneSignals.OnSceneLoaded), typeof(EventBusSystem.SerializedSignals.SerializedOnSceneLoaded)},
            {typeof(EventBusSystem.Signals.SceneSignals.OnSetScene), typeof(EventBusSystem.SerializedSignals.SerializedOnSetScene)},
            {typeof(EventBusSystem.Signals.InputSignals.OnPauseKeyPressed), typeof(EventBusSystem.SerializedSignals.SerializedOnPauseKeyPressed)},
            {typeof(EventBusSystem.Signals.GameSignals.OnExitCutscene), typeof(EventBusSystem.SerializedSignals.SerializedOnExitCutscene)},
            {typeof(EventBusSystem.Signals.GameSignals.OnGameStateChanged), typeof(EventBusSystem.SerializedSignals.SerializedOnGameStateChanged)},
            {typeof(EventBusSystem.Signals.GameSignals.OnLevelCameraChange), typeof(EventBusSystem.SerializedSignals.SerializedOnLevelCameraChange)},
            {typeof(EventBusSystem.Signals.GameSignals.OnRespawnPlayer), typeof(EventBusSystem.SerializedSignals.SerializedOnRespawnPlayer)},
            {typeof(EventBusSystem.Signals.GameSignals.OnSpawnPlayer), typeof(EventBusSystem.SerializedSignals.SerializedOnSpawnPlayer)},
            {typeof(EventBusSystem.Signals.DeveloperSignals.OnDevConsoleOpened), typeof(EventBusSystem.SerializedSignals.SerializedOnDevConsoleOpened)},
            {typeof(EventBusSystem.Signals.DeveloperSignals.OnDevRespawn), typeof(EventBusSystem.SerializedSignals.SerializedOnDevRespawn)},
        };

        public static readonly Dictionary<Type, Type> SerializedTypeToType = new()
        {
            {typeof(EventBusSystem.SerializedSignals.SerializedOnChangeTransitionState), typeof(EventBusSystem.Signals.TransitionSignals.OnChangeTransitionState)},
            {typeof(EventBusSystem.SerializedSignals.SerializedOnSceneLoaded), typeof(EventBusSystem.Signals.SceneSignals.OnSceneLoaded)},
            {typeof(EventBusSystem.SerializedSignals.SerializedOnSetScene), typeof(EventBusSystem.Signals.SceneSignals.OnSetScene)},
            {typeof(EventBusSystem.SerializedSignals.SerializedOnPauseKeyPressed), typeof(EventBusSystem.Signals.InputSignals.OnPauseKeyPressed)},
            {typeof(EventBusSystem.SerializedSignals.SerializedOnExitCutscene), typeof(EventBusSystem.Signals.GameSignals.OnExitCutscene)},
            {typeof(EventBusSystem.SerializedSignals.SerializedOnGameStateChanged), typeof(EventBusSystem.Signals.GameSignals.OnGameStateChanged)},
            {typeof(EventBusSystem.SerializedSignals.SerializedOnLevelCameraChange), typeof(EventBusSystem.Signals.GameSignals.OnLevelCameraChange)},
            {typeof(EventBusSystem.SerializedSignals.SerializedOnRespawnPlayer), typeof(EventBusSystem.Signals.GameSignals.OnRespawnPlayer)},
            {typeof(EventBusSystem.SerializedSignals.SerializedOnSpawnPlayer), typeof(EventBusSystem.Signals.GameSignals.OnSpawnPlayer)},
            {typeof(EventBusSystem.SerializedSignals.SerializedOnDevConsoleOpened), typeof(EventBusSystem.Signals.DeveloperSignals.OnDevConsoleOpened)},
            {typeof(EventBusSystem.SerializedSignals.SerializedOnDevRespawn), typeof(EventBusSystem.Signals.DeveloperSignals.OnDevRespawn)},
        };
    }
}
