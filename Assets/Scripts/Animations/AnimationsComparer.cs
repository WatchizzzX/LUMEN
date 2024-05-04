using System;
using System.Collections.Generic;
using UnityEngine.TextCore.Text;

namespace Animations
{
    public class AnimationsComparer : IEqualityComparer<BaseAnimation>
    {
        public bool Equals(BaseAnimation x, BaseAnimation y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;

            if (!x.AnimationParameterType.Equals(y.AnimationParameterType)) return false;

            if (!x.Duration.Equals(y.Duration)) return false;

            switch (x.AnimationParameterType)
            {
                case AnimationParameterType.Color:
                    if (!CompareColor(x as ColorAnimation, y as ColorAnimation)) return false;
                    break;
                case AnimationParameterType.Rotation:
                    if (!CompareRotation(x as RotationAnimation, y as RotationAnimation)) return false;
                    break;
                case AnimationParameterType.Float:
                    if (!CompareFloat(x as FloatAnimation, y as FloatAnimation)) return false;
                    break;
                case AnimationParameterType.Position:
                    if (!ComparePosition(x as PositionAnimation, y as PositionAnimation)) return false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return true;
        }

        public int GetHashCode(BaseAnimation obj)
        {
            return obj.AnimationParameterType switch
            {
                AnimationParameterType.Color => HashCode.Combine((int)obj.AnimationParameterType, obj.Duration,
                    ((ColorAnimation)obj).Color, ((ColorAnimation)obj).TargetColor),
                AnimationParameterType.Rotation => HashCode.Combine((int)obj.AnimationParameterType, obj.Duration,
                    ((RotationAnimation)obj).Rotation, ((RotationAnimation)obj).TargetRotation, ((RotationAnimation)obj).RotateMode),
                AnimationParameterType.Float => HashCode.Combine((int)obj.AnimationParameterType, obj.Duration,
                    ((FloatAnimation)obj).Value, ((FloatAnimation)obj).TargetValue),
                AnimationParameterType.Position => HashCode.Combine((int)obj.AnimationParameterType, obj.Duration,
                    ((PositionAnimation)obj).Position, ((PositionAnimation)obj).TargetPosition),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private static bool CompareColor(ColorAnimation x, ColorAnimation y)
        {
            var initialColorComparison = x.Color.Equals(y.Color);
            if (!initialColorComparison) return false;
            
            var targetColorComparison = x.TargetColor.Equals(y.TargetColor);
            return targetColorComparison;
        }

        private static bool CompareRotation(RotationAnimation x, RotationAnimation y)
        {
            var initialRotationComparison = x.Rotation.Equals(y.Rotation);
            if (!initialRotationComparison) return false;
            
            var targetRotationComparison = x.TargetRotation.Equals(y.TargetRotation);
            if (!targetRotationComparison) return false;
            
            var rotateModeComparison = x.RotateMode.Equals(y.RotateMode);
            return rotateModeComparison;
        }
        
        private static bool ComparePosition(PositionAnimation x, PositionAnimation y)
        {
            var initialPositionComparison = x.Position.Equals(y.Position);
            if (!initialPositionComparison) return false;
            
            var targetPositionComparison = x.TargetPosition.Equals(y.TargetPosition);
            return targetPositionComparison;
        }
        
        private static bool CompareFloat(FloatAnimation x, FloatAnimation y)
        {
            var initialValueComparison = x.Value.Equals(y.Value);
            if (!initialValueComparison) return false;
            
            var targetValueComparison = x.TargetValue.Equals(y.TargetValue);
            return targetValueComparison;
        }
    }
}