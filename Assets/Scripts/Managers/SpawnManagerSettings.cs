using UnityEngine;

namespace Managers
{
    [CreateAssetMenu(fileName = "SpawnManagerSettings", menuName = "Settings/Create SpawnManager settings", order = 0)]
    public class SpawnManagerSettings : ScriptableObject
    {
        public GameObject playerPrefab;
        public GameObject cameraPrefab;
        public GameObject inputPrefab;
    }
}