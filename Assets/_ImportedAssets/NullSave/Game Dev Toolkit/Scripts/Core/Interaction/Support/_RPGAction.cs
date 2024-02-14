using System;
using System.Collections.Generic;
using UnityEngine;

namespace NullSave.GDTK
{
    [AutoDocLocation("interaction/rpg-actions")]
    [AutoDoc("The Interactable Object is a base class for creating RPG Object Actions.")]
    [RequireComponent(typeof(InteractableRPGObject))]
    public class RPGAction : MonoBehaviour
    {

        #region Fields

        [Tooltip("Event raised when interaction should be locked")] public SimpleEvent onLockStateChanged;

        private bool m_lock;

        #endregion

        #region Properties

        [AutoDoc("Callback to invoke when using this action")]
        public virtual Action callback { get; protected set; }

        [AutoDoc("Get/Set interaction lock state")]
        public bool lockInteractions
        {
            get { return m_lock; }
            set
            {
                if (m_lock == value) return;
                m_lock = value;
                onLockStateChanged?.Invoke();
            }
        }

        [AutoDoc("Parameter (usually string display text) to use with this action")]
        public virtual object parameter { get; protected set; }

        [AutoDoc("Gets the parent Interactable RPG Object")]
        public InteractableRPGObject parent { get; protected set; }

        [AutoDoc("List of other action to prevent")]
        public virtual List<RPGAction> preventActions { get; protected set; }

        #endregion

        #region Unity Methods

        public virtual void Awake()
        {
            parent = GetComponent<InteractableRPGObject>();
        }

        #endregion

        #region Public Methods

        [AutoDoc("This method is called whenever the Interaction UI is displayed")]
        public virtual void OnInteractionCalled() { }

        #endregion

    }
}