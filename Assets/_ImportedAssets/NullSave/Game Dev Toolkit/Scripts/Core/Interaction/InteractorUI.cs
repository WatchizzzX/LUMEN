//
// Game Developers Toolkit © 2023 by Thomas W Holtquist is licensed under CC BY-SA 4.0 
// https://www.nullsave.com
//

using System;
using UnityEngine;
using UnityEngine.Events;

namespace NullSave.GDTK
{
    [AutoDocLocation("interaction/ui")]
    [AutoDoc("The InteractorUI component provides UI prompts for the player for interactable objects. This prefab should be set on the Interface Manager.")]
    public class InteractorUI : MonoBehaviour
    {

        #region Fields

        [Tooltip("Destination of the text")] public Label uiText;
        [Tooltip("Format to use when setting text. {0} is replaced with the supplied interactable object text")] public string format;
        [Tooltip("Require button/key to be held to interact")] public bool requireHold;
        [Tooltip("Seconds to require hold before interaction")] public float holdTime;
        [Tooltip("Progressbar for hold time")] public Progressbar holdProgressbar;

        [Tooltip("Event raised when held time changes")] public UnityEvent onHeldTimeChanged;

        private float m_timeHeld;

        #endregion

        #region Properties

        [AutoDoc("Get/Set if the interaction button is being held")]
        public bool holding { get; set; }

        [AutoDoc("Get if this UI is overriding the default interaction input")]
        public virtual bool overrideInteraction { get; }

        [AutoDoc("Get/Set the interactor displaying this UI")]
        public Interactor source { get; set; }

        [AutoDoc("Get/Set the target interactable object")]
        public InteractableObject target { get; set; }

        [AutoDoc("Get/Set the amount of time the interaction button has been held")]
        public float timeHeld
        {
            get { return m_timeHeld; }
            set
            {
                m_timeHeld = value;
                onHeldTimeChanged?.Invoke();
                if(holdProgressbar != null)
                {
                    holdProgressbar.value = m_timeHeld / holdTime;
                }
            }
        }

        #endregion

        #region Unity Methods

        public virtual void OnEnable()
        {
            timeHeld = 0;
        }

        public virtual void Reset()
        {
            format = "{0}";
            holdTime = 1;
        }

        #endregion

        #region Public Methods

        [AutoDoc("Initialize the object")]
        [AutoDocParameter("Parameters to pass")]
        [AutoDocParameter("Callbacks to pass")]
        public virtual void Initialize(object[] args, Action[] callbacks) { }

        [AutoDoc("Interact with the target")]
        public virtual void InteractWithTarget()
        {
            if (target != null && source != null) target.Interact(source);
        }

        [AutoDoc("Set the text for the UI")]
        [AutoDocParameter("Text value")]
        public virtual void SetText(string value)
        {
            if (uiText == null) return;
            uiText.text = format.Replace("{0}", value);
        }

        [AutoDoc("Perform interaction using an Interactor UI Action")]
        [AutoDocParameter("Action to use")]
        public virtual void UseAction(InteractorUIAction action) { }

        #endregion

    }
}