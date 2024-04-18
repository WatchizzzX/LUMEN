using System;
using DG.Tweening;
using EventBusSystem;
using EventBusSystem.Signals.GameSignals;
using EventBusSystem.Signals.TransitionSignals;
using Managers.Settings;
using ServiceLocatorSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    public class CameraManager : MonoBehaviour, IService
    {
        [NonSerialized]
        public CameraManagerSettings Settings;

        private EventBus _eventBus;
        private SpawnManager _spawnManager;

        private void Awake()
        {
            _eventBus = ServiceLocator.Get<EventBus>();
            SubscribeToEventBus();
        }

        private void Start()
        {
            _spawnManager = ServiceLocator.Get<SpawnManager>();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEventBus();
        }

        private void OnExitCutscene(OnExitCutsceneSignal signal)
        {
            switch (signal.ExitCamera)
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

        private void OnTransitionCutout(OnChangeTransitionStateSignal signal)
        {
            if (signal.TransitionState != TransitionState.Cutout) return;

            _spawnManager.FinishCamera.Priority.Value = 0;
            _spawnManager.PlayerCamera.Priority.Value = 1;
        }

        private void OnRespawnPlayer(OnRespawnPlayerSignal signal)
        {
            _spawnManager.PlayerCamera.Follow = null;
        }

        private void SubscribeToEventBus()
        {
            _eventBus.Subscribe<OnExitCutsceneSignal>(OnExitCutscene);
            _eventBus.Subscribe<OnChangeTransitionStateSignal>(OnTransitionCutout);
            _eventBus.Subscribe<OnRespawnPlayerSignal>(OnRespawnPlayer, 1);
        }

        private void UnsubscribeFromEventBus()
        {
            _eventBus.Unsubscribe<OnExitCutsceneSignal>(OnExitCutscene);
            _eventBus.Unsubscribe<OnChangeTransitionStateSignal>(OnTransitionCutout);
            _eventBus.Unsubscribe<OnRespawnPlayerSignal>(OnRespawnPlayer);
        }
    }
}