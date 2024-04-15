using System;
using EventBusSystem;
using EventBusSystem.Signals.GameSignals;
using EventBusSystem.Signals.SceneSignals;
using EventBusSystem.Signals.TransitionSignals;
using Input;
using InteractionSystem;
using Managers.Settings;
using PickupSystem;
using Player;
using ServiceLocatorSystem;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace Managers
{
    public class SpawnManager : MonoBehaviour, IService
    {
        #region Public Variables

        [NonSerialized] public SpawnManagerSettings Settings;

        public PlayerController PlayerController { get; private set; }
        public PlayerAnimator PlayerAnimator { get; private set; }
        public PickupController PickupController { get; private set; }
        public InteractorController InteractorController { get; private set; }
        public CinemachineCamera PlayerCamera { get; private set; }
        public CinemachineCamera FinishCamera { get; private set; }
        public Camera MainCamera { get; private set; }
        public CinemachineBrain CameraBrain { get; private set; }
        public PlayerInputHandler PlayerInputHandler { get; private set; }

        #endregion

        #region Private Variables

        private EventBus _eventBus;

        private bool _isLevelChanging;

        private GameObject _spawnedPlayerGo;

        private GameObject _spawnedInputGo;

        private GameObject _spawnedCameraGo;

        private GameObject _spawnedPlayerObjectsGo;

        private bool _cachedRespawn;

        #endregion

        #region MonoBehaviour

        private void Awake()
        {
            _eventBus = ServiceLocator.Get<EventBus>();
            SubscribeToEventBus();
        }

        private void Start()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEventBus();
        }

        #endregion

        #region Methods

        private void SubscribeToEventBus()
        {
            _eventBus.Subscribe<OnSceneLoadedSignal>(OnSceneLoaded);
            _eventBus.Subscribe<OnRespawnPlayerSignal>(OnRespawnPlayer);
            _eventBus.Subscribe<OnChangeTransitionStateSignal>(OnChangeTransitionState);
        }

        private void UnsubscribeFromEventBus()
        {
            _eventBus.Unsubscribe<OnSceneLoadedSignal>(OnSceneLoaded);
            _eventBus.Unsubscribe<OnRespawnPlayerSignal>(OnRespawnPlayer);
            _eventBus.Unsubscribe<OnChangeTransitionStateSignal>(OnChangeTransitionState);
        }

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
                _spawnedPlayerGo = Instantiate(Settings.PlayerPrefab, Vector3.zero, Quaternion.identity);
                _spawnedPlayerGo.name = "Player";
                PlayerAnimator = _spawnedPlayerGo.GetComponent<PlayerAnimator>();
                PlayerController = _spawnedPlayerGo.GetComponent<PlayerController>();
                InteractorController = _spawnedPlayerGo.GetComponent<InteractorController>();
                PickupController = _spawnedPlayerGo.GetComponent<PickupController>();

                MainCamera.clearFlags = CameraClearFlags.Skybox;

                PlayerCamera.Follow = _spawnedPlayerGo.transform.Find("CameraTarget");

                PlayerInputHandler.onMoveEvent.AddListener(PlayerController.SetMoveDirection);
                PlayerInputHandler.onInteractEvent.AddListener(InteractorController.Interact);
                PlayerInputHandler.onPickupEvent.AddListener(PickupController.OnPickupEvent);
                PlayerInputHandler.onJumpEvent.AddListener(PlayerController.CallToJump);
                PlayerInputHandler.onSprintEvent.AddListener(PlayerController.SetSprint);
                
                _eventBus.Invoke(new OnSpawnPlayerSignal());
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        private void RespawnPlayer()
        {
            _spawnedPlayerGo.transform.position = Vector3.zero;
            PlayerCamera.Follow = _spawnedPlayerGo.transform.Find("CameraTarget");
            PlayerCamera.ForceCameraPosition(Settings.SpawnCameraPosition,
                Settings.SpawnCameraRotation);
        }

        #endregion

        #region Event Handlers

        private void OnSceneLoaded(OnSceneLoadedSignal signal)
        {
            if (signal.LoadedScene.name != "Start")
                SpawnPlayer();
        }

        private void OnRespawnPlayer(OnRespawnPlayerSignal signal)
        {
            _cachedRespawn = true;
        }

        private void OnChangeTransitionState(OnChangeTransitionStateSignal signal)
        {
            if (!signal.IsChangingScene)
            {
                if (!_cachedRespawn || signal.TransitionState != TransitionState.Cutout) return;
                RespawnPlayer();
                _cachedRespawn = false;
                return;
            }

            switch (signal.TransitionState)
            {
                case TransitionState.Started:
                    PlayerInputHandler.onMoveEvent.RemoveAllListeners();
                    PlayerInputHandler.onInteractEvent.RemoveAllListeners();
                    PlayerInputHandler.onJumpEvent.RemoveAllListeners();
                    PlayerInputHandler.onSprintEvent.RemoveAllListeners();
                    PlayerAnimator = null;
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
    }
}