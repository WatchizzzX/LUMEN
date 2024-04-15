using DG.Tweening;
using UnityEngine;

namespace Animations
{
    public class RotationAnimation : BaseAnimation
    {
        public Quaternion Rotation
        {
            get => IsLocalRotation ? _transform.localRotation : _transform.rotation;
            set
            {
                if (IsLocalRotation)
                    _transform.localRotation = value;
                else
                    _transform.rotation = value;
            }
        }

        public readonly Vector3 TargetRotation;

        public readonly RotateMode RotateMode;
        
        private readonly bool IsLocalRotation;

        private readonly Transform _transform;

        public RotationAnimation(Transform transform, Vector3 targetRotation, float duration, RotateMode rotateMode,
            bool isLocalRotation = false)
        {
            AnimationParameterType = AnimationParameterType.Rotation;
            
            _transform = transform;
            TargetRotation = targetRotation;
            Duration = duration;
            RotateMode = rotateMode;
            IsLocalRotation = isLocalRotation;
        }
    }
}