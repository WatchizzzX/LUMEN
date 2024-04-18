using UnityEngine;

namespace Managers.Settings
{
    [CreateAssetMenu(fileName = "SpawnManagerSettings", menuName = "Settings/Create SpawnManager settings", order = 0)]
    public class SpawnManagerSettings : ScriptableObject
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject cameraPrefab;
        [SerializeField] private Vector3 spawnCameraPosition;
        [SerializeField] private Quaternion spawnCameraRotation;
        [SerializeField] private GameObject inputPrefab;
        [SerializeField] private GameObject inGameUIPrefab;

        public GameObject PlayerPrefab => playerPrefab;

        public GameObject CameraPrefab => cameraPrefab;

        public Vector3 SpawnCameraPosition => spawnCameraPosition;

        public Quaternion SpawnCameraRotation => spawnCameraRotation;

        public GameObject InputPrefab => inputPrefab;

        public GameObject InGameUIPrefab => inGameUIPrefab;
    }
}