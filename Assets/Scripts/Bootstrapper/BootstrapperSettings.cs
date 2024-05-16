using EasyTransition;
using Managers.Settings;
using NaughtyAttributes;
using UnityEngine;

namespace Bootstrapper
{
    [CreateAssetMenu(fileName = "BootstrapperSettings", menuName = "Settings/Create Bootstrapper settings", order = 0)]
    public class BootstrapperSettings : ScriptableObject
    {
        [Header("Manager Settings")] [SerializeField]
        private TransitionManagerSettings transitionManagerSettings;

        [SerializeField] private SpawnManagerSettings spawnManagerSettings;
        [SerializeField] private CameraManagerSettings cameraManagerSettings;
        [SerializeField] private GameManagerSettings gameManagerSettings;
        [SerializeField] private SceneManagerSettings sceneManagerSettings;

        [Space(2f), Header("Bootstrapper Settings")] [SerializeField, Scene]
        private int startupScene;

        [SerializeField] private float delayBeforeLoading;
        [SerializeField] private TransitionSettings overrideFirstTransition;

        public TransitionManagerSettings TransitionManagerSettings => transitionManagerSettings;
        public SpawnManagerSettings SpawnManagerSettings => spawnManagerSettings;
        public CameraManagerSettings CameraManagerSettings => cameraManagerSettings;
        public GameManagerSettings GameManagerSettings => gameManagerSettings;
        public SceneManagerSettings SceneManagerSettings => sceneManagerSettings;
        public int StartupScene => startupScene;
        public float DelayBeforeLoading => delayBeforeLoading;
        public TransitionSettings OverrideFirstTransition => overrideFirstTransition;
    }
}