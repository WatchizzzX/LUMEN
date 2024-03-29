using UnityEngine;

namespace Managers.Settings
{
    [CreateAssetMenu(fileName = "GameManagerSettings", menuName = "Settings/Create GameManager settings", order = 0)]
    public class GameManagerSettings : ScriptableObject
    {
        [SerializeField] private float exitCutsceneDuration;

        public float ExitCutsceneDuration => exitCutsceneDuration;
    }
}