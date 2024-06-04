using System.Collections.Generic;
using UnityEngine;

namespace Managers.Settings
{
    [CreateAssetMenu(fileName = "GameManagerSettings", menuName = "Settings/Create GameManager settings", order = 0)]
    public class GameManagerSettings : ScriptableObject
    {
        [Header("Durations of events")]
        [SerializeField] private float exitCutsceneDuration;
        
        [Header("Records of Levels")]
        [SerializeField] private LevelSettings[] levelsRecords;

        public float ExitCutsceneDuration => exitCutsceneDuration;

        public IReadOnlyCollection<LevelSettings> LevelsRecords => levelsRecords;
    }
}