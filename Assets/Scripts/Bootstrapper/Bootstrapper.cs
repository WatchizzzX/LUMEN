using EasyTransition;
using EventBusSystem;
using EventBusSystem.Signals.SceneSignals;
using Managers;
using Managers.Settings;
using NaughtyAttributes;
using ServiceLocatorSystem;
using UnityEngine;
using Utils;
using SceneManager = Managers.SceneManager;
using TransitionManager = Managers.TransitionManager;

namespace Bootstrapper
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private TransitionManagerSettings transitionManagerSettings;
        [SerializeField] private SpawnManagerSettings spawnManagerSettings;
        [SerializeField] private CameraManagerSettings cameraManagerSettings;
        [SerializeField] private GameManagerSettings gameManagerSettings;
        [SerializeField] private SceneManagerSettings sceneManagerSettings;
        [SerializeField, Scene] private int startupScene;
        [SerializeField] private float delayBeforeLoading;
        [SerializeField] private TransitionSettings overrideFirstTransition;

        private EventBus _eventBus;
        
        private void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            LoadStartupScene();
        }

        private void Initialize()
        {
            var managersGo = new GameObject("Managers", typeof(DontDestroyOnLoad));

            var eventBusGo = new GameObject("Event Bus", typeof(EventBus));
            eventBusGo.transform.SetParent(managersGo.transform);
            _eventBus = eventBusGo.GetComponent<EventBus>();
            ServiceLocator.Register(_eventBus);
            
            var gameManagerGo = new GameObject("Game Manager", typeof(GameManager));
            gameManagerGo.transform.SetParent(managersGo.transform);
            var gameManager = gameManagerGo.GetComponent<GameManager>();
            gameManager.Settings = gameManagerSettings;
            ServiceLocator.Register(gameManager);

            var sceneManagerGo = new GameObject("Scene Manager", typeof(SceneManager));
            sceneManagerGo.transform.SetParent(managersGo.transform);
            var sceneManager = sceneManagerGo.GetComponent<SceneManager>();
            sceneManager.Settings = sceneManagerSettings;
            ServiceLocator.Register(sceneManager);
            
            var transitionManagerGo = new GameObject("Transition Manager", typeof(TransitionManager));
            transitionManagerGo.transform.SetParent(managersGo.transform);
            var transitionManager = transitionManagerGo.GetComponent<TransitionManager>();
            transitionManager.Settings = transitionManagerSettings;
            ServiceLocator.Register(transitionManager);
            
            var spawnManagerGo = new GameObject("Spawn Manager", typeof(SpawnManager));
            spawnManagerGo.transform.SetParent(managersGo.transform);
            var spawnManager = spawnManagerGo.GetComponent<SpawnManager>();
            spawnManager.Settings = spawnManagerSettings;
            ServiceLocator.Register(spawnManager);
            
            var cameraManagerGo = new GameObject("Camera Manager", typeof(CameraManager));
            cameraManagerGo.transform.SetParent(managersGo.transform);
            var cameraManager = cameraManagerGo.GetComponent<CameraManager>();
            cameraManager.Settings = cameraManagerSettings;
            ServiceLocator.Register(cameraManager);

            var animationManagerGo = new GameObject("Animation Manager", typeof(AnimationManager));
            animationManagerGo.transform.SetParent(managersGo.transform);
            var animationManager = animationManagerGo.GetComponent<AnimationManager>();
            ServiceLocator.Register(animationManager);
        }

        private void LoadStartupScene()
        {
            _eventBus.RaiseEvent(new OnSetScene(startupScene, delayBeforeLoading, overrideFirstTransition));
            Destroy(this);
        }
    }
}