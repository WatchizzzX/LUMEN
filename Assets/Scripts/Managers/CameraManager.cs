using System;
using DG.Tweening;
using Enums;
using EventBusSystem;
using EventBusSystem.Signals.GameSignals;
using EventBusSystem.Signals.TransitionSignals;
using Managers.Settings;
using ServiceLocatorSystem;
using UnityEngine;
using UnityEngine.Serialization;

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

<<<<<<< Updated upstream
<<<<<<< Updated upstream
        private void OnDestroy()
        {
            UnsubscribeFromEventBus();
        }

        private void OnExitCutscene(OnExitCutsceneSignal signal)
        {
            switch (signal.ExitCamera)
=======
        [ListenTo(SignalEnum.OnExitCutscene)]
        private void OnExitCutscene(EventModel eventModel)
        {
            switch (((OnExitCutscene)eventModel.Payload).ExitCamera)
>>>>>>> Stashed changes
=======
        [ListenTo(SignalEnum.OnExitCutsceneSignal)]
        public void OnExitCutscene(EventModel eventModel)
        {
            switch (((OnExitCutsceneSignal)eventModel.Payload).ExitCamera)
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
<<<<<<< Updated upstream
        private void OnTransitionCutout(OnChangeTransitionStateSignal signal)
        {
            if (signal.TransitionState != TransitionState.Cutout) return;
=======
        [ListenTo(SignalEnum.OnChangeTransitionState)]
        private void OnTransitionCutout(EventModel eventModel)
        {
            if (((OnChangeTransitionState)eventModel.Payload).TransitionState != TransitionState.Cutout) return;
>>>>>>> Stashed changes
=======
        [ListenTo(SignalEnum.OnChangeTransitionStateSignal)]
        public void OnTransitionCutout(EventModel eventModel)
        {
            if (((OnChangeTransitionStateSignal)eventModel.Payload).TransitionState != TransitionState.Cutout) return;
>>>>>>> Stashed changes

            _spawnManager.FinishCamera.Priority.Value = 0;
            _spawnManager.PlayerCamera.Priority.Value = 1;
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
            _spawnManager.PlayerCamera.Follow = null;
        }
    }
}