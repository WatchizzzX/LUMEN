namespace InteractionSystem
{
    public interface IInteractable
    {
        public InteractableType GetInteractableType();
        
        public string GetMessage();
        
        public void Interact(InteractorController interactor);
    }

    public enum InteractableType
    {
        Object
    }
}