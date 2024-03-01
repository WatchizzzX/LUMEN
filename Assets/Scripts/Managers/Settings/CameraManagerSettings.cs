using UnityEngine;

namespace Managers.Settings
{
    [CreateAssetMenu(fileName = "CameraManagerSettings", menuName = "Settings/Create CameraManager settings", order = 0)]
    public class CameraManagerSettings : ScriptableObject
    {
        public Color startColor;
        public Color finishColor;
    }
}