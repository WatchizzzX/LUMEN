using Animations;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Animators
{
    public class DecalAnimator : BaseAnimator
    {
        [SerializeField] private Color onColor;
        [SerializeField] private Color offColor;
        [SerializeField] private DecalProjector decalProjector;
        
        private Material _material;

        private static readonly int Color = Shader.PropertyToID("_Color");

        protected override void Awake()
        {
            base.Awake();
            _material = new Material(decalProjector.material)
            {
                name = "Instance"
            };
            decalProjector.material = _material;
        }

        protected override BaseAnimation CreateAnimation(bool value, float duration)
        {
            return new ColorAnimation(() => _material.GetColor(Color),
                x => _material.SetColor(Color, x),
                value ? onColor : offColor,
                duration);
        }
    }
}