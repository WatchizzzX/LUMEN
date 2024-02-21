using UnityEngine;

namespace InteractionSystem.Interactables
{
    [RequireComponent(typeof(MeshRenderer))]
    public class InteractableLamp : BasicInteractable
    {
        [SerializeField] private Color offColor = Color.black;
        [SerializeField] private Color onColor = Color.yellow;

        private MeshRenderer _meshRenderer;
        private bool _isEnabled;

        protected override void Awake()
        {
            base.Awake();

            _meshRenderer = GetComponent<MeshRenderer>();
            _meshRenderer.material.color = offColor;
        }

        public override void Interact()
        {
            Interact(null);
        }

        public override void Interact(InteractorController interactor)
        {
            _isEnabled = !_isEnabled;
            _meshRenderer.material.color = _isEnabled ? onColor : offColor;
        }
    }
}