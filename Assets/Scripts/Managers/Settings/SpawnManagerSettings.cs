using UnityEngine;

namespace Managers.Settings
{
    [CreateAssetMenu(fileName = "SpawnManagerSettings", menuName = "Settings/Create SpawnManager settings", order = 0)]
    public class SpawnManagerSettings : ScriptableObject
    {
        public GameObject playerPrefab;
        public GameObject cameraPrefab;
        public Vector3 spawnCameraPosition;
        public GameObject inputPrefab;
    }
}