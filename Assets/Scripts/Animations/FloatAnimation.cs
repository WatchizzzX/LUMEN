using System;

namespace Animations
{
    public class FloatAnimation : BaseAnimation
    {
        public float Value
        {
            get => _valueGetter.Invoke();
            set => _valueSetter?.Invoke(value);
        }

        public readonly float TargetValue;
        private readonly Func<float> _valueGetter;
        private readonly Action<float> _valueSetter;
        
        public FloatAnimation(Func<float> getter, Action<float> setter, float targetValue, float duration)
        {
            AnimationParameterType = AnimationParameterType.Float;
            
            TargetValue = targetValue;
            _valueSetter = setter;
            _valueGetter = getter;
            Duration = duration;
        }
    }
}