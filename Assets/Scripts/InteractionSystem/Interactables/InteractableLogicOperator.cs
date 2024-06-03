using System;

namespace InteractionSystem.Interactables
{
    public class InteractableLogicOperator: BasicInteractable
    {
        protected override void Awake()
        {
            base.Awake();
            InteractableType = InteractableType.LogicComponent;
        }

        public override void Interact()
        {
            return;
        }

        public override void Interact(InteractorController interactor)
        {
            return;
        }

        protected override void OnInteract()
        {
            return;
        }
    }
}