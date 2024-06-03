using System;
using Animations;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace Animators
{
    [RequireComponent(typeof(LineRenderer))]
    public class LineRendererAnimator : BaseAnimator
    {
        private enum MaterialAnimationType
        {
            Color,
            EmissionColor
        }

        [SerializeField] private MaterialAnimationType animationType;

        [ShowIf(nameof(ShowColorsInInspector))] [SerializeField]
        private Color onColor;

        [ShowIf(nameof(ShowColorsInInspector))] [SerializeField]
        private Color offColor;

        [SerializeField] private LineRenderer lineRenderer;
        
        private Material _material;
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

        private void OnValidate()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        protected override void Awake()
        {
            base.Awake();
            _material = lineRenderer.material;
            if(animationType == MaterialAnimationType.EmissionColor)
                _material.EnableKeyword("_Emission");
        }

        protected override BaseAnimation CreateAnimation(bool value, float duration)
        {
            var targetColor = value ? onColor : offColor;
            return animationType switch
            {
                MaterialAnimationType.Color => new ColorAnimation(() => _material.color, x => _material.color = x,
                    targetColor, duration),
                MaterialAnimationType.EmissionColor => new ColorAnimation(() => _material.GetColor(EmissionColor),
                    x => _material.SetColor(EmissionColor, x), targetColor, duration),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private bool ShowColorsInInspector()
        {
            return animationType is MaterialAnimationType.Color
                or MaterialAnimationType.EmissionColor;
        }
    }
}