namespace InteractionSystem
{
    /// <summary>
    /// Interface for interactable object. Work with InteractorController
    /// </summary>
    public interface IInteractable
    {
        /// <summary>
        /// Return cached InteractableType of interactable object
        /// </summary>
        /// <returns>InteractableType of interactable object</returns>
        public InteractableType GetInteractableType();
        
        /// <summary>
        /// Return cached message of interactable object
        /// </summary>
        /// <returns>Message of interactable object</returns>
        public string GetMessage();
        
        /// <summary>
        /// A method that calls the InteractorController during interaction and passes itself as a parameter
        /// </summary>
        /// <param name="interactor">InteractorController which interact with object</param>
        public void Interact(InteractorController interactor);
    }

    /// <summary>
    /// Interactable type of object. Define behaviour (WIP)
    /// </summary>
    public enum InteractableType
    {
        Button,
        Switch,
        Pad,
        LogicComponent
    }
}