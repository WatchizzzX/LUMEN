using System;
using Enums;
using EventBusSystem;
using EventBusSystem.Signals.SceneSignals;
using EventBusSystem.Signals.TransitionSignals;
using Input;
using InteractionSystem;
using Managers.Settings;
using PickupSystem;
using Player;
using ServiceLocatorSystem;
using Unity.Cinemachine;
using Unity.TinyCharacterController.Brain;
using UnityEngine;
using Utils.Extra;
using Utils.Gameplay;

namespace Managers
{
    public class SpawnManager : EventBehaviour, IService
    {
        #region Public Variables

        [NonSerialized] public SpawnManagerSettings Settings;

        public PlayerController PlayerController { get; private set; }
        public PickupController PickupController { get; private set; }
        public InteractorController InteractorController { get; private set; }
        public CinemachineCamera PlayerCamera { get; private set; }
        public CinemachineCamera FinishCamera { get; private set; }
        public CinemachineTargetGroup FinishCameraTargetGroup { get; private set; }
        public Camera MainCamera { get; private set; }
        public CinemachineBrain CameraBrain { get; private set; }
        public PlayerInputHandler PlayerInputHandler { get; private set; }

        #endregion

        #region Private Variables

        private Transform _spawnPoint;

        private GameObject _spawnedPlayerGo;

        private GameObject _spawnedInputGo;

        private GameObject _spawnedCameraGo;

        private GameObject _spawnedPlayerObjectsGo;

        private GameObject _spawnedInGameUIGo;

        private bool _cachedRespawn;

        private SceneManager _sceneManager;

        #endregion

        #region MonoBehaviour

        protected override void Awake()
        {
            base.Awake();
            _sceneManager = ServiceLocator.Get<SceneManager>();
        }

        private void Start()
        {
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            _spawnedPlayerObjectsGo = new GameObject("Player Objects");
            _spawnedPlayerObjectsGo.AddComponent<DontDestroyOnLoad>();

            _spawnedCameraGo = Instantiate(Settings.CameraPrefab, Vector3.zero,
                Quaternion.identity);
            _spawnedCameraGo.name = "Player Camera";
            _spawnedCameraGo.transform.SetParent(_spawnedPlayerObjectsGo.transform);
            PlayerCamera = _spawnedCameraGo.transform.Find("Player Camera").GetComponent<CinemachineCamera>();
            FinishCamera = _spawnedCameraGo.transform.Find("Finish Camera").GetComponent<CinemachineCamera>();
            FinishCameraTargetGroup = _spawnedCameraGo.transform.Find("Finish Camera Target Group").GetComponent<CinemachineTargetGroup>();
            var cameraTransform = _spawnedCameraGo.transform.Find("Main Camera");
            MainCamera = cameraTransform.GetComponent<Camera>();
            CameraBrain = cameraTransform.GetComponent<CinemachineBrain>();

            var exitTime = ServiceLocator.Get<GameManager>().Settings.ExitCutsceneDuration;
            CameraBrain.DefaultBlend =
                new CinemachineBlendDefinition(CinemachineBlendDefinition.Styles.EaseInOut, exitTime);

            _spawnedInputGo = Instantiate(Settings.InputPrefab, Vector3.zero, Quaternion.identity);
            _spawnedInputGo.name = "Input";
            _spawnedInputGo.transform.SetParent(_spawnedPlayerObjectsGo.transform);
            PlayerInputHandler = _spawnedInputGo.GetComponent<PlayerInputHandler>();
        }

        private void SpawnPlayer()
        {
            try
            {
                var spawnPoint = FindFirstObjectByType<SpawnPoint>(FindObjectsInactive.Exclude);

                if (spawnPoint)
                    _spawnPoint = spawnPoint.transform;

                _spawnedPlayerGo = Instantiate(Settings.PlayerPrefab,
                    !_spawnPoint ? Vector3.zero : _spawnPoint.transform.position, Quaternion.identity);
                _spawnedPlayerGo.name = "Player";
                PlayerController = _spawnedPlayerGo.GetComponent<PlayerController>();
                InteractorController = _spawnedPlayerGo.GetComponent<InteractorController>();
                PickupController = _spawnedPlayerGo.GetComponent<PickupController>();

                PlayerCamera.Follow = _spawnedPlayerGo.transform.Find("CameraTarget");

                PlayerInputHandler.onMoveEvent.AddListener(PlayerController.Move);
                PlayerInputHandler.onInteractEvent.AddListener(InteractorController.Interact);
                PlayerInputHandler.onPickupEvent.AddListener(PickupController.OnPickupEvent);
                PlayerInputHandler.onJumpEvent.AddListener(PlayerController.Jump);
                PlayerInputHandler.onSprintEvent.AddListener(PlayerController.Sprint);

                RaiseEvent(SignalEnum.OnSpawnPlayer);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        private void RespawnPlayer()
        {
            if (_sceneManager.IsSceneLoading) return;
            _spawnedPlayerGo.GetComponent<RigidbodyBrain>().Warp(_spawnPoint ? _spawnPoint.position : Vector3.zero);
            PlayerCamera.Follow = _spawnedPlayerGo.transform.Find("CameraTarget");
            PlayerCamera.ForceCameraPosition(Settings.SpawnCameraPosition,
                Settings.SpawnCameraRotation);
        }

        private void SpawnInGameUI()
        {
            _spawnedInGameUIGo = Instantiate(Settings.InGameUIPrefab, Vector3.zero, Quaternion.identity);
        }

        #endregion

        #region Event Handlers

        [ListenTo(SignalEnum.OnSceneLoaded)]
        private void OnSceneLoaded(EventModel eventModel)
        {
            if (!((OnSceneLoaded)eventModel.Payload).IsGameLevel) return;
            SpawnPlayer();
            SpawnInGameUI();
        }

        [ListenTo(SignalEnum.OnRespawnPlayer)]
        private void OnRespawnPlayer(EventModel eventModel)
        {
            _cachedRespawn = true;
        }

        [ListenTo(SignalEnum.OnChangeTransitionState)]
        private void OnChangeTransitionState(EventModel eventModel)
        {
            var payload = (OnChangeTransitionState)eventModel.Payload;
            if (!payload.IsChangingScene)
            {
                if (!_cachedRespawn || payload.TransitionState != TransitionState.Cutout) return;
                RespawnPlayer();
                _cachedRespawn = false;
                return;
            }

            switch (payload.TransitionState)
            {
                case TransitionState.Started:
                    PlayerInputHandler.onMoveEvent.RemoveAllListeners();
                    PlayerInputHandler.onInteractEvent.RemoveAllListeners();
                    PlayerInputHandler.onJumpEvent.RemoveAllListeners();
                    PlayerInputHandler.onSprintEvent.RemoveAllListeners();
                    PlayerController = null;
                    PickupController = null;
                    PlayerCamera.Follow = null;
                    break;
                case TransitionState.Cutout:
                    PlayerCamera.ForceCameraPosition(Settings.SpawnCameraPosition,
                        Settings.SpawnCameraRotation);
                    break;
                case TransitionState.Finished:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
        
        #if DEBUG || UNITY_EDITOR

        [ListenTo(SignalEnum.OnDevRespawn)]
        private void OnDevRespawn(EventModel eventModel)
        {
            _spawnedPlayerGo.GetComponent<RigidbodyBrain>().Warp(_spawnPoint ? _spawnPoint.position : Vector3.zero);
            PlayerCamera.ForceCameraPosition(Settings.SpawnCameraPosition,
                Settings.SpawnCameraRotation);
        }
        #endif
    }
}