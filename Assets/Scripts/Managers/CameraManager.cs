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

        private void OnFinishLevel(OnStartExitCutsceneSignal signal)
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

        private void SubscribeToEventBus()
        {
            _eventBus.Subscribe<OnStartExitCutsceneSignal>(OnFinishLevel);
            _eventBus.Subscribe<OnChangeTransitionStateSignal>(OnTransitionCutout);
        }

        private void UnsubscribeFromEventBus()
        {
            _eventBus.Unsubscribe<OnStartExitCutsceneSignal>(OnFinishLevel);
            _eventBus.Unsubscribe<OnChangeTransitionStateSignal>(OnTransitionCutout);
        }
    }
}