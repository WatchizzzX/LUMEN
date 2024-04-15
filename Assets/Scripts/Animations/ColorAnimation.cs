using System;
using UnityEngine;

namespace Animations
{
    public class ColorAnimation : BaseAnimation
    {
        public Color Color
        {
            get => _valueGetter.Invoke();
            set => _valueSetter?.Invoke(value);
        }

        public readonly Color TargetColor;
        private readonly Func<Color> _valueGetter;
        private readonly Action<Color> _valueSetter;

        public ColorAnimation(Func<Color> getter, Action<Color> setter, Color targetColor, float duration)
        {
            AnimationParameterType = AnimationParameterType.Color;
            
            TargetColor = targetColor;
            _valueSetter = setter;
            _valueGetter = getter;
            Duration = duration;
        }
    }
}