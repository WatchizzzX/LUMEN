using UnityEngine;

namespace Animations
{
    public class PositionAnimation : BaseAnimation
    {
        public Vector3 Position
        {
            get => IsLocalPosition ? _transform.localPosition : _transform.position;
            set
            {
                if (IsLocalPosition)
                    _transform.localPosition = value;
                else
                    _transform.position = value;
            }
        }
        
        public readonly Vector3 TargetPosition;

        public readonly bool IsLocalPosition;

        private readonly Transform _transform;
        
        public PositionAnimation(Transform transform, Vector3 targetPosition, float duration, bool isLocalPosition = false)
        {
            AnimationParameterType = AnimationParameterType.Position;
            
            _transform = transform;
            TargetPosition = targetPosition;
            Duration = duration;
            IsLocalPosition = isLocalPosition;
        }
    }
}