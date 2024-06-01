////////////////////////////////////////////////////////////////////////////////
// This code is auto-generated. Please don't change this code to avoid errors //
////////////////////////////////////////////////////////////////////////////////
using System;
using EasyTransition;
using Enums;
using Unity.Cinemachine;
using NaughtyAttributes;
using UnityEngine;

namespace EventBusSystem.SerializedSignals
{

  [Serializable]
  public class SerializedOnChangeTransitionState : SerializedSignal
  {
      [SerializeField] public TransitionState TransitionState;
      [SerializeField] public Boolean IsChangingScene;
  }

  [Serializable]
  public class SerializedOnSceneLoaded : SerializedSignal
  {
      [SerializeField][Scene] public int LoadedScene;
      [SerializeField] public Boolean IsGameLevel;
  }

  [Serializable]
  public class SerializedOnSetScene : SerializedSignal
  {
      [SerializeField] public Int32 NewSceneID;
      [SerializeField] public Single Delay;
      [SerializeField] public TransitionSettings OverrideTransitionSettings;
  }

  [Serializable]
  public class SerializedOnPauseKeyPressed : SerializedSignal
  {
  }

  [Serializable]
  public class SerializedOnExitCutscene : SerializedSignal
  {
      [SerializeField] public Int32 NextSceneID;
      [SerializeField] public Single CutsceneDuration;
      [SerializeField] public ExitCamera ExitCamera;
  }

  [Serializable]
  public class SerializedOnGameStateChanged : SerializedSignal
  {
      [SerializeField] public GameState GameState;
  }

  [Serializable]
  public class SerializedOnLevelCameraChange : SerializedSignal
  {
      [SerializeField] public CinemachineCamera TargetCamera;
  }

  [Serializable]
  public class SerializedOnRespawnPlayer : SerializedSignal
  {
      [SerializeField] public Single TransitionStartDelay;
  }

  [Serializable]
  public class SerializedOnSpawnPlayer : SerializedSignal
  {
  }

  [Serializable]
  public class SerializedOnDevConsoleOpened : SerializedSignal
  {
      [SerializeField] public Boolean IsOpened;
  }

  [Serializable]
  public class SerializedOnDevModeChanged : SerializedSignal
  {
      [SerializeField] public Boolean InDeveloperMode;
  }

  [Serializable]
  public class SerializedOnDevRespawn : SerializedSignal
  {
  }

}
