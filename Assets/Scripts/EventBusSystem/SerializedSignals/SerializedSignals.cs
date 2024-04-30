////////////////////////////////////////////////////////////////////////////////
// This code is auto-generated. Please don't change this code to avoid errors //
////////////////////////////////////////////////////////////////////////////////
using System;
using EasyTransition;
using Enums;
using EventBusSystem.SerializedSignals;
using EventBusSystem.Signals.GameSignals;
using EventBusSystem.Signals.TransitionSignals;
using NaughtyAttributes;
using UnityEngine;

namespace EventBusSystem.SerializedSignals
{

  [Serializable]
<<<<<<< Updated upstream
  public class SerializedOnChangeTransitionState : SerializedSignal
=======
  public class SerializedOnChangeTransitionStateSignal : SerializedSignal
>>>>>>> Stashed changes
  {
      [SerializeField] public TransitionState TransitionState;
      [SerializeField] public Boolean IsChangingScene;
  }

  [Serializable]
<<<<<<< Updated upstream
  public class SerializedOnSceneLoaded : SerializedSignal
=======
  public class SerializedOnSceneLoadedSignal : SerializedSignal
>>>>>>> Stashed changes
  {
      [SerializeField][Scene] public int LoadedScene;
      [SerializeField] public Boolean IsGameLevel;
  }

  [Serializable]
<<<<<<< Updated upstream
  public class SerializedOnSetScene : SerializedSignal
=======
  public class SerializedOnSetSceneSignal : SerializedSignal
>>>>>>> Stashed changes
  {
      [SerializeField] public Int32 NewSceneID;
      [SerializeField] public Single Delay;
      [SerializeField] public TransitionSettings OverrideTransitionSettings;
  }

  [Serializable]
<<<<<<< Updated upstream
  public class SerializedOnPauseKeyPressed : SerializedSignal
=======
  public class SerializedOnPauseKeyPressedSignal : SerializedSignal
>>>>>>> Stashed changes
  {
  }

  [Serializable]
<<<<<<< Updated upstream
  public class SerializedOnExitCutscene : SerializedSignal
=======
  public class SerializedOnExitCutsceneSignal : SerializedSignal
>>>>>>> Stashed changes
  {
      [SerializeField] public Int32 NextSceneID;
      [SerializeField] public Single CutsceneDuration;
      [SerializeField] public ExitCamera ExitCamera;
  }

  [Serializable]
<<<<<<< Updated upstream
  public class SerializedOnGameStateChanged : SerializedSignal
=======
  public class SerializedOnGameStateChangedSignal : SerializedSignal
>>>>>>> Stashed changes
  {
      [SerializeField] public GameState GameState;
  }

  [Serializable]
<<<<<<< Updated upstream
  public class SerializedOnRespawnPlayer : SerializedSignal
=======
  public class SerializedOnRespawnPlayerSignal : SerializedSignal
>>>>>>> Stashed changes
  {
      [SerializeField] public Single TransitionStartDelay;
  }

  [Serializable]
<<<<<<< Updated upstream
  public class SerializedOnSpawnPlayer : SerializedSignal
=======
  public class SerializedOnSpawnPlayerSignal : SerializedSignal
>>>>>>> Stashed changes
  {
  }

  [Serializable]
<<<<<<< Updated upstream
  public class SerializedOnDevConsoleOpened : SerializedSignal
=======
  public class SerializedOnDevConsoleOpenedSignal : SerializedSignal
>>>>>>> Stashed changes
  {
      [SerializeField] public Boolean IsOpened;
  }

}
