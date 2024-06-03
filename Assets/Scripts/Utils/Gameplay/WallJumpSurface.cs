using UnityEngine;

namespace Utils.Gameplay
{
    public class WallJumpSurface : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float additiveForwardVelocity;
        [SerializeField, Min(0f)] private float additiveUpVelocity;
        [SerializeField, Min(0f)] private float additiveSideVelocity;
        [SerializeField, Min(0f)] private float jumpHeight;

        public float AdditiveForwardVelocity => additiveForwardVelocity;
        public float AdditiveUpVelocity => additiveUpVelocity;
        public float AdditiveSideVelocity => additiveSideVelocity;
        public float JumpHeight => jumpHeight;
    }
}