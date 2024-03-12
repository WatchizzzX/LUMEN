using EasyTransition;
using UnityEngine;

namespace Managers.Settings
{
    [CreateAssetMenu(fileName = "TransitionManagerSettings", menuName = "Settings/Create TransitionManager settings",
        order = 0)]
    public class TransitionManagerSettings : ScriptableObject
    {
        [SerializeField] private GameObject transitionPrefab;
        [SerializeField] private TransitionSettings defaultTransitionSettings;

        public GameObject TransitionPrefab => transitionPrefab;

        public TransitionSettings DefaultTransitionSettings => defaultTransitionSettings;
    }
}