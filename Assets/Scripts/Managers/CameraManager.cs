using System;
using System.Collections;
using Enums;
using EventBusSystem;
using EventBusSystem.Signals.GameSignals;
using EventBusSystem.Signals.TransitionSignals;
using Managers.Settings;
using ServiceLocatorSystem;
using Unity.Cinemachine;
using UnityEngine;
using Utils;

namespace Managers
{
    public class CameraManager : EventBehaviour, IService
    {
        [NonSerialized] public CameraManagerSettings Settings;

        private SpawnManager _spawnManager;

        private void Start()
        {
            _spawnManager = ServiceLocator.Get<SpawnManager>();
        }

        private IEnumerator StartExitAnimation()
        {
            _spawnManager.FinishCamera.Follow = _spawnManager.PlayerCamera.Follow;

            _spawnManager.FinishCameraTargetGroup.AddMember(FindFirstObjectByType<PillarTarget>().transform, 2f, 40f);
            _spawnManager.FinishCameraTargetGroup.AddMember(_spawnManager.PlayerCamera.Follow, 1f, 1.5f);

            yield return new WaitForSecondsRealtime(0.1f);
            
            _spawnManager.PlayerCamera.Priority.Value = 0;
            _spawnManager.FinishCamera.Priority.Value = 1;
        }

        [ListenTo(SignalEnum.OnExitCutscene)]
        private void OnExitCutscene(EventModel eventModel)
        {
            switch (((OnExitCutscene)eventModel.Payload).ExitCamera)
            {
                case ExitCamera.FarView:
                    StartCoroutine(StartExitAnimation());
                    break;
                case ExitCamera.StaticView:
                    _spawnManager.PlayerCamera.Follow = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [ListenTo(SignalEnum.OnChangeTransitionState)]
        private void OnTransitionCutout(EventModel eventModel)
        {
            if (((OnChangeTransitionState)eventModel.Payload).TransitionState != TransitionState.Cutout) return;

            _spawnManager.FinishCamera.Priority.Value = 0;
            _spawnManager.FinishCameraTargetGroup.Targets.Clear();
            _spawnManager.PlayerCamera.Priority.Value = 1;
        }

        [ListenTo(SignalEnum.OnRespawnPlayer)]
        private void OnRespawnPlayer(EventModel eventModel)
        {
            _spawnManager.PlayerCamera.Follow = null;
        }
    }
}