using System;
using DG.Tweening;
using EventBusSystem;
using EventBusSystem.Signals.GameSignals;
using EventBusSystem.Signals.TransitionSignals;
using Managers.Settings;
using ServiceLocatorSystem;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour, IService
    {
        public CameraManagerSettings cameraManagerSettings;

        private EventBus _eventBus;
        private Camera _mainCamera;

        private void Awake()
        {
            _eventBus = ServiceLocator.Get<EventBus>();
            SubscribeToEventBus();
        }

        private void Start()
        {
            var spawnManager = ServiceLocator.Get<SpawnManager>();
            _mainCamera = spawnManager.MainCamera;
        }

        private void OnDestroy()
        {
            UnsubscribeFromEventBus();
        }

        private void OnFinishLevel(OnStartExitCutsceneSignal signal)
        {
            _mainCamera.DOColor(cameraManagerSettings.finishColor, signal.CutsceneDuration);
        }

        private void OnTransitionCutout(OnChangeTransitionStateSignal signal)
        {
            if (signal.TransitionState != TransitionState.Cutout) return;

            _mainCamera.backgroundColor = cameraManagerSettings.startColor;
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