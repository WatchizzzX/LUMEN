using EventBusSystem;
using EventBusSystem.Signals.SceneSignals;
using Managers;
using NaughtyAttributes;
using ServiceLocatorSystem;
using UnityEngine;
using SceneManager = Managers.SceneManager;

namespace Bootstrapper
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private TransitionManagerSettings transitionManagerSettings;
        [SerializeField] private SpawnManagerSettings spawnManagerSettings;
        [SerializeField, Scene] private int startupScene;
        
        private GameManager _gameManager;
        private SceneManager _sceneManager;
        private TransitionManager _transitionManager;
        private SpawnManager _spawnManager;
        private EventBus _eventBus;

        private void Awake()
        {
            DontDestroyOnLoad(this);

            _eventBus = new EventBus();
            ServiceLocator.Register(_eventBus);
            
            var gameManagerGo = new GameObject("Game Manager", typeof(GameManager));
            gameManagerGo.transform.SetParent(transform);
            _gameManager = gameManagerGo.GetComponent<GameManager>();
            ServiceLocator.Register(_gameManager);

            var sceneManagerGo = new GameObject("Scene Manager", typeof(SceneManager));
            sceneManagerGo.transform.SetParent(transform);
            _sceneManager = sceneManagerGo.GetComponent<SceneManager>();
            ServiceLocator.Register(_sceneManager);
            
            var spawnManagerGo = new GameObject("Spawn Manager", typeof(SpawnManager));
            spawnManagerGo.transform.SetParent(transform);
            _spawnManager = spawnManagerGo.GetComponent<SpawnManager>();
            _spawnManager.spawnManagerSettings = spawnManagerSettings;
            ServiceLocator.Register(_spawnManager);

            var transitionManagerGo = new GameObject("Transition Manager", typeof(TransitionManager));
            transitionManagerGo.transform.SetParent(transform);
            _transitionManager = transitionManagerGo.GetComponent<TransitionManager>();
            _transitionManager.transitionManagerSettings = transitionManagerSettings;
            ServiceLocator.Register(_transitionManager);
        }

        private void Start()
        {
            LoadStartupScene();
        }

        private void LoadStartupScene()
        {
            _eventBus.Invoke(new SetSceneSignal(startupScene));
        }
    }
}