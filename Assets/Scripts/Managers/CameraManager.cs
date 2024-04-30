using System;
using Enums;
using EventBusSystem;
using EventBusSystem.Signals.GameSignals;
using EventBusSystem.Signals.TransitionSignals;
using Managers.Settings;
using ServiceLocatorSystem;

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

        [ListenTo(SignalEnum.OnExitCutscene)]
        private void OnExitCutscene(EventModel eventModel)
        {
            switch (((OnExitCutscene)eventModel.Payload).ExitCamera)
            {
                case ExitCamera.FarView:
                    _spawnManager.FinishCamera.Priority.Value = 1;
                    _spawnManager.PlayerCamera.Priority.Value = 0;
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
            _spawnManager.PlayerCamera.Priority.Value = 1;
        }

        [ListenTo(SignalEnum.OnRespawnPlayer)]
        private void OnRespawnPlayer(EventModel eventModel)
        {
            _spawnManager.PlayerCamera.Follow = null;
        }
    }
}