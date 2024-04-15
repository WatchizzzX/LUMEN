using EventBusSystem;
using EventBusSystem.Signals.SceneSignals;
using Managers;
using Managers.Settings;
using NaughtyAttributes;
using ServiceLocatorSystem;
using UnityEngine;
using Utils;
using SceneManager = Managers.SceneManager;

namespace Bootstrapper
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private TransitionManagerSettings transitionManagerSettings;
        [SerializeField] private SpawnManagerSettings spawnManagerSettings;
        [SerializeField] private CameraManagerSettings cameraManagerSettings;
        [SerializeField] private GameManagerSettings gameManagerSettings;
        [SerializeField, Scene] private int startupScene;

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
            _eventBus = new EventBus();
            ServiceLocator.Register(_eventBus);

            var managersGo = new GameObject("Managers", typeof(DontDestroyOnLoad));
            
            var gameManagerGo = new GameObject("Game Manager", typeof(GameManager));
            gameManagerGo.transform.SetParent(managersGo.transform);
            var gameManager = gameManagerGo.GetComponent<GameManager>();
            gameManager.Settings = gameManagerSettings;
            ServiceLocator.Register(gameManager);

            var sceneManagerGo = new GameObject("Scene Manager", typeof(SceneManager));
            sceneManagerGo.transform.SetParent(managersGo.transform);
            var sceneManager = sceneManagerGo.GetComponent<SceneManager>();
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
            _eventBus.Invoke(new OnSetSceneSignal(startupScene, 0f));
            Destroy(this);
        }
    }
}