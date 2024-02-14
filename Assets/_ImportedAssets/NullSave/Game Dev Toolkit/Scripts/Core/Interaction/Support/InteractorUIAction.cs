using UnityEngine;
using UnityEngine.Events;

namespace NullSave.GDTK
{
    [AutoDocLocation("interaction/ui")]
    [AutoDoc("This component allows you to respond to an action on MultiActionInteractors")]
    public class InteractorUIAction : MonoBehaviour
    {
        #region Enumerations

        public enum TextSource
        {
            InteractionText = 0,
            Args = 1,
        }

        #endregion

        #region Fields

        [Tooltip("Method of confirming interaction")] public NavigationTypeSimple interactionType;
        [Tooltip("Button to use for confirming interaction")] public string interactionButton;
        [Tooltip("Key to use for confirming interaction")] public KeyCode interactionKey;

        [Tooltip("Associated action index")] public int actionIndex;
        [Tooltip("Associated callback index to raise")] public int callbackIndex;

        [Tooltip("Label to display text")] public Label label;
        [Tooltip("Formatting to apply to label")][TextArea(2, 5)] public string format = "{0}";
        [Tooltip("Text source")] public TextSource textSource;
        [Tooltip("Index of Arg housing text")] public int argIndex;

        [Tooltip("Event raised when there is an associated action")] public UnityEvent onAssociatedAction;
        [Tooltip("Event raised when there is not an associated action")] public UnityEvent onNoAssociatedAction;

        #endregion

        #region Properties

        [AutoDoc("Get/Set parent Interactable UI")] public InteractorUI parent { get; set; }

        #endregion

        #region Unity Methods

        private void Reset()
        {
            format = "{0}";
            label = GetComponentInChildren<Label>();
        }

        private void Update()
        {
            switch (interactionType)
            {
                case NavigationTypeSimple.ByKey:
                    if (InterfaceManager.Input.GetKeyDown(interactionKey))
                    {
                        parent.UseAction(this);
                    }
                    break;
                case NavigationTypeSimple.ByButton:
                    if (InterfaceManager.Input.GetButtonDown(interactionButton))
                    {
                        parent.UseAction(this);
                    }
                    break;
            }
        }

        #endregion
    }
}