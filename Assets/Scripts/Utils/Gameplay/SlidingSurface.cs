using UnityEngine;
using UnityEngine.Serialization;

namespace Utils.Gameplay
{
    public class SlidingSurface : MonoBehaviour
    {
        [SerializeField, Min(1f)] private float speedMultiplier = 1;
        [SerializeField, Min(0f)] private float jumpMultiplier;

        public float SpeedMultiplier => speedMultiplier;
        public float JumpMultiplier => jumpMultiplier;
    }
}