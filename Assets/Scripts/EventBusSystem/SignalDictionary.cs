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
<<<<<<< Updated upstream
            {typeof(EventBusSystem.Signals.TransitionSignals.OnChangeTransitionState), SignalEnum.OnChangeTransitionState},
            {typeof(EventBusSystem.Signals.SceneSignals.OnSceneLoaded), SignalEnum.OnSceneLoaded},
            {typeof(EventBusSystem.Signals.SceneSignals.OnSetScene), SignalEnum.OnSetScene},
            {typeof(EventBusSystem.Signals.InputSignals.OnPauseKeyPressed), SignalEnum.OnPauseKeyPressed},
            {typeof(EventBusSystem.Signals.GameSignals.OnExitCutscene), SignalEnum.OnExitCutscene},
            {typeof(EventBusSystem.Signals.GameSignals.OnGameStateChanged), SignalEnum.OnGameStateChanged},
            {typeof(EventBusSystem.Signals.GameSignals.OnRespawnPlayer), SignalEnum.OnRespawnPlayer},
            {typeof(EventBusSystem.Signals.GameSignals.OnSpawnPlayer), SignalEnum.OnSpawnPlayer},
            {typeof(EventBusSystem.Signals.DeveloperSignals.OnDevConsoleOpened), SignalEnum.OnDevConsoleOpened},
=======
            {typeof(EventBusSystem.Signals.TransitionSignals.OnChangeTransitionStateSignal), SignalEnum.OnChangeTransitionStateSignal},
            {typeof(EventBusSystem.Signals.SceneSignals.OnSceneLoadedSignal), SignalEnum.OnSceneLoadedSignal},
            {typeof(EventBusSystem.Signals.SceneSignals.OnSetSceneSignal), SignalEnum.OnSetSceneSignal},
            {typeof(EventBusSystem.Signals.InputSignals.OnPauseKeyPressedSignal), SignalEnum.OnPauseKeyPressedSignal},
            {typeof(EventBusSystem.Signals.GameSignals.OnExitCutsceneSignal), SignalEnum.OnExitCutsceneSignal},
            {typeof(EventBusSystem.Signals.GameSignals.OnGameStateChangedSignal), SignalEnum.OnGameStateChangedSignal},
            {typeof(EventBusSystem.Signals.GameSignals.OnRespawnPlayerSignal), SignalEnum.OnRespawnPlayerSignal},
            {typeof(EventBusSystem.Signals.GameSignals.OnSpawnPlayerSignal), SignalEnum.OnSpawnPlayerSignal},
            {typeof(EventBusSystem.Signals.DeveloperSignals.OnDevConsoleOpenedSignal), SignalEnum.OnDevConsoleOpenedSignal},
>>>>>>> Stashed changes
        };

        public static readonly Dictionary<SignalEnum, Type> EnumToType = new()
        {
<<<<<<< Updated upstream
            {SignalEnum.OnChangeTransitionState, typeof(EventBusSystem.Signals.TransitionSignals.OnChangeTransitionState)},
            {SignalEnum.OnSceneLoaded, typeof(EventBusSystem.Signals.SceneSignals.OnSceneLoaded)},
            {SignalEnum.OnSetScene, typeof(EventBusSystem.Signals.SceneSignals.OnSetScene)},
            {SignalEnum.OnPauseKeyPressed, typeof(EventBusSystem.Signals.InputSignals.OnPauseKeyPressed)},
            {SignalEnum.OnExitCutscene, typeof(EventBusSystem.Signals.GameSignals.OnExitCutscene)},
            {SignalEnum.OnGameStateChanged, typeof(EventBusSystem.Signals.GameSignals.OnGameStateChanged)},
            {SignalEnum.OnRespawnPlayer, typeof(EventBusSystem.Signals.GameSignals.OnRespawnPlayer)},
            {SignalEnum.OnSpawnPlayer, typeof(EventBusSystem.Signals.GameSignals.OnSpawnPlayer)},
            {SignalEnum.OnDevConsoleOpened, typeof(EventBusSystem.Signals.DeveloperSignals.OnDevConsoleOpened)},
=======
            {SignalEnum.OnChangeTransitionStateSignal, typeof(EventBusSystem.Signals.TransitionSignals.OnChangeTransitionStateSignal)},
            {SignalEnum.OnSceneLoadedSignal, typeof(EventBusSystem.Signals.SceneSignals.OnSceneLoadedSignal)},
            {SignalEnum.OnSetSceneSignal, typeof(EventBusSystem.Signals.SceneSignals.OnSetSceneSignal)},
            {SignalEnum.OnPauseKeyPressedSignal, typeof(EventBusSystem.Signals.InputSignals.OnPauseKeyPressedSignal)},
            {SignalEnum.OnExitCutsceneSignal, typeof(EventBusSystem.Signals.GameSignals.OnExitCutsceneSignal)},
            {SignalEnum.OnGameStateChangedSignal, typeof(EventBusSystem.Signals.GameSignals.OnGameStateChangedSignal)},
            {SignalEnum.OnRespawnPlayerSignal, typeof(EventBusSystem.Signals.GameSignals.OnRespawnPlayerSignal)},
            {SignalEnum.OnSpawnPlayerSignal, typeof(EventBusSystem.Signals.GameSignals.OnSpawnPlayerSignal)},
            {SignalEnum.OnDevConsoleOpenedSignal, typeof(EventBusSystem.Signals.DeveloperSignals.OnDevConsoleOpenedSignal)},
>>>>>>> Stashed changes
        };

        public static readonly Dictionary<Type, Type> TypeToSerializedType = new()
        {
<<<<<<< Updated upstream
            {typeof(EventBusSystem.Signals.TransitionSignals.OnChangeTransitionState), typeof(EventBusSystem.SerializedSignals.SerializedOnChangeTransitionState)},
            {typeof(EventBusSystem.Signals.SceneSignals.OnSceneLoaded), typeof(EventBusSystem.SerializedSignals.SerializedOnSceneLoaded)},
            {typeof(EventBusSystem.Signals.SceneSignals.OnSetScene), typeof(EventBusSystem.SerializedSignals.SerializedOnSetScene)},
            {typeof(EventBusSystem.Signals.InputSignals.OnPauseKeyPressed), typeof(EventBusSystem.SerializedSignals.SerializedOnPauseKeyPressed)},
            {typeof(EventBusSystem.Signals.GameSignals.OnExitCutscene), typeof(EventBusSystem.SerializedSignals.SerializedOnExitCutscene)},
            {typeof(EventBusSystem.Signals.GameSignals.OnGameStateChanged), typeof(EventBusSystem.SerializedSignals.SerializedOnGameStateChanged)},
            {typeof(EventBusSystem.Signals.GameSignals.OnRespawnPlayer), typeof(EventBusSystem.SerializedSignals.SerializedOnRespawnPlayer)},
            {typeof(EventBusSystem.Signals.GameSignals.OnSpawnPlayer), typeof(EventBusSystem.SerializedSignals.SerializedOnSpawnPlayer)},
            {typeof(EventBusSystem.Signals.DeveloperSignals.OnDevConsoleOpened), typeof(EventBusSystem.SerializedSignals.SerializedOnDevConsoleOpened)},
=======
            {typeof(EventBusSystem.Signals.TransitionSignals.OnChangeTransitionStateSignal), typeof(EventBusSystem.SerializedSignals.SerializedOnChangeTransitionStateSignal)},
            {typeof(EventBusSystem.Signals.SceneSignals.OnSceneLoadedSignal), typeof(EventBusSystem.SerializedSignals.SerializedOnSceneLoadedSignal)},
            {typeof(EventBusSystem.Signals.SceneSignals.OnSetSceneSignal), typeof(EventBusSystem.SerializedSignals.SerializedOnSetSceneSignal)},
            {typeof(EventBusSystem.Signals.InputSignals.OnPauseKeyPressedSignal), typeof(EventBusSystem.SerializedSignals.SerializedOnPauseKeyPressedSignal)},
            {typeof(EventBusSystem.Signals.GameSignals.OnExitCutsceneSignal), typeof(EventBusSystem.SerializedSignals.SerializedOnExitCutsceneSignal)},
            {typeof(EventBusSystem.Signals.GameSignals.OnGameStateChangedSignal), typeof(EventBusSystem.SerializedSignals.SerializedOnGameStateChangedSignal)},
            {typeof(EventBusSystem.Signals.GameSignals.OnRespawnPlayerSignal), typeof(EventBusSystem.SerializedSignals.SerializedOnRespawnPlayerSignal)},
            {typeof(EventBusSystem.Signals.GameSignals.OnSpawnPlayerSignal), typeof(EventBusSystem.SerializedSignals.SerializedOnSpawnPlayerSignal)},
            {typeof(EventBusSystem.Signals.DeveloperSignals.OnDevConsoleOpenedSignal), typeof(EventBusSystem.SerializedSignals.SerializedOnDevConsoleOpenedSignal)},
>>>>>>> Stashed changes
        };

        public static readonly Dictionary<Type, Type> SerializedTypeToType = new()
        {
<<<<<<< Updated upstream
            {typeof(EventBusSystem.SerializedSignals.SerializedOnChangeTransitionState), typeof(EventBusSystem.Signals.TransitionSignals.OnChangeTransitionState)},
            {typeof(EventBusSystem.SerializedSignals.SerializedOnSceneLoaded), typeof(EventBusSystem.Signals.SceneSignals.OnSceneLoaded)},
            {typeof(EventBusSystem.SerializedSignals.SerializedOnSetScene), typeof(EventBusSystem.Signals.SceneSignals.OnSetScene)},
            {typeof(EventBusSystem.SerializedSignals.SerializedOnPauseKeyPressed), typeof(EventBusSystem.Signals.InputSignals.OnPauseKeyPressed)},
            {typeof(EventBusSystem.SerializedSignals.SerializedOnExitCutscene), typeof(EventBusSystem.Signals.GameSignals.OnExitCutscene)},
            {typeof(EventBusSystem.SerializedSignals.SerializedOnGameStateChanged), typeof(EventBusSystem.Signals.GameSignals.OnGameStateChanged)},
            {typeof(EventBusSystem.SerializedSignals.SerializedOnRespawnPlayer), typeof(EventBusSystem.Signals.GameSignals.OnRespawnPlayer)},
            {typeof(EventBusSystem.SerializedSignals.SerializedOnSpawnPlayer), typeof(EventBusSystem.Signals.GameSignals.OnSpawnPlayer)},
            {typeof(EventBusSystem.SerializedSignals.SerializedOnDevConsoleOpened), typeof(EventBusSystem.Signals.DeveloperSignals.OnDevConsoleOpened)},
=======
            {typeof(EventBusSystem.SerializedSignals.SerializedOnChangeTransitionStateSignal), typeof(EventBusSystem.Signals.TransitionSignals.OnChangeTransitionStateSignal)},
            {typeof(EventBusSystem.SerializedSignals.SerializedOnSceneLoadedSignal), typeof(EventBusSystem.Signals.SceneSignals.OnSceneLoadedSignal)},
            {typeof(EventBusSystem.SerializedSignals.SerializedOnSetSceneSignal), typeof(EventBusSystem.Signals.SceneSignals.OnSetSceneSignal)},
            {typeof(EventBusSystem.SerializedSignals.SerializedOnPauseKeyPressedSignal), typeof(EventBusSystem.Signals.InputSignals.OnPauseKeyPressedSignal)},
            {typeof(EventBusSystem.SerializedSignals.SerializedOnExitCutsceneSignal), typeof(EventBusSystem.Signals.GameSignals.OnExitCutsceneSignal)},
            {typeof(EventBusSystem.SerializedSignals.SerializedOnGameStateChangedSignal), typeof(EventBusSystem.Signals.GameSignals.OnGameStateChangedSignal)},
            {typeof(EventBusSystem.SerializedSignals.SerializedOnRespawnPlayerSignal), typeof(EventBusSystem.Signals.GameSignals.OnRespawnPlayerSignal)},
            {typeof(EventBusSystem.SerializedSignals.SerializedOnSpawnPlayerSignal), typeof(EventBusSystem.Signals.GameSignals.OnSpawnPlayerSignal)},
            {typeof(EventBusSystem.SerializedSignals.SerializedOnDevConsoleOpenedSignal), typeof(EventBusSystem.Signals.DeveloperSignals.OnDevConsoleOpenedSignal)},
>>>>>>> Stashed changes
        };
    }
}
