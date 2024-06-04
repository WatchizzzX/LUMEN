using NaughtyAttributes;
using UnityEngine;

namespace Managers.Settings
{
    [CreateAssetMenu(fileName = "LevelSettings", menuName = "Settings/Create Level settings", order = 0)]
    public class LevelSettings : ScriptableObject
    {
        [SerializeField, Scene] private int sceneID;
        [SerializeField] private int firstStarRecord;
        [SerializeField] private int secondStarRecord;
        [SerializeField] private int thirdStarRecord;

        public int SceneID => sceneID;
        public int FirstStarRecord => firstStarRecord;
        public int SecondStarRecord => secondStarRecord;
        public int ThirdStarRecord => thirdStarRecord;
    }
}