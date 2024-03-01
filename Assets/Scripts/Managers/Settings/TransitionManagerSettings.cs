using EasyTransition;
using UnityEngine;

namespace Managers.Settings
{
    [CreateAssetMenu(fileName = "TransitionManagerSettings", menuName = "Settings/Create TransitionManager settings", order = 0)]
    public class TransitionManagerSettings : ScriptableObject
    {
        public GameObject transitionPrefab;
        public TransitionSettings defaultTransitionSettings;
    }
}