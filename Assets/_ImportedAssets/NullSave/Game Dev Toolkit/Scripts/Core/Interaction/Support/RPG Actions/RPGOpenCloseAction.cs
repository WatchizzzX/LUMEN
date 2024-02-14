using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NullSave.GDTK
{
    [AutoDocLocation("interaction/rpg-actions")]
    [AutoDoc("The RPG Action adds opening and closing to an Interactable RPG Object.")]
    public class RPGOpenCloseAction : RPGAction
    {

        #region Fields

        [SerializeField] private Animator m_animator;

        // Open/Close
        [Tooltip("Indicates if the object is currently open")][SerializeField] private bool m_opened;
        [Tooltip("Text to display when object can be opened")] public string openText;
        [Tooltip("Text to display when object can be closed")] public string closeText;
        [Tooltip("Time it takes to open/close the object (interaction is disabled during this time)")] public float openCloseDuration;
        [Tooltip("List of modifiers to apply to the animator when opening")] public List<AnimatorModifier> openAnimModifiers;
        [Tooltip("List of modifiers to apply to the animator when closing")] public List<AnimatorModifier> closeAnimModifiers;
        [Tooltip("Name of the broadcaster channel to use with the audio ppol")] public string audioPoolChannel;
        [Tooltip("Sound to play on open")] public AudioClip openSound;
        [Tooltip("Sound to play on close")] public AudioClip closeSound;
        [Tooltip("Event raised when the object opens")] public UnityEvent onOpen;
        [Tooltip("Event raised when the object closes")] public UnityEvent onClose;

        [Tooltip("List of actions to forbid when opened")] public List<RPGAction> forbidWhenOpen;
        [Tooltip("List of actions to forbid when closed")] public List<RPGAction> forbidWhenClosed;

        #endregion

        #region Properties

        [AutoDoc("List of other action to prevent")]
        public override List<RPGAction> preventActions {
            get {
                if (m_opened) return forbidWhenOpen;
                return forbidWhenClosed;
            }
        }

        #endregion

        #region Unity Methods

        public override void Awake()
        {
            base.Awake();
            m_animator = GetComponentInChildren<Animator>();
        }

        private void OnEnable()
        {
            RebuildInteraction();
        }

        private void Reset()
        {
            openText = "Open";
            closeText = "Close";
        }

        #endregion

        #region Public Methods

        [AutoDoc("Close the object (if opened)")]
        public void Close()
        {
            if (!m_opened) return;

            lockInteractions = true;
            m_opened = false;
            if (m_animator != null)
            {
                foreach (var mod in closeAnimModifiers)
                {
                    mod.ApplyMod(m_animator);
                }
            }

            onClose?.Invoke();
            Broadcaster.Broadcast(audioPoolChannel, "Play", new object[] { closeSound, transform.position });
            RebuildInteraction();
            StartCoroutine(WaitForOpenClose());
        }

        [AutoDoc("Open the object")]
        public void Open()
        {
            if (m_opened) return;

            lockInteractions = true;
            m_opened = true;
            if (m_animator != null)
            {
                foreach (var mod in openAnimModifiers)
                {
                    mod.ApplyMod(m_animator);
                }
            }

            onOpen?.Invoke();
            Broadcaster.Broadcast(audioPoolChannel, "Play", new object[] { openSound, transform.position });
            RebuildInteraction();
            StartCoroutine(WaitForOpenClose());
        }

        #endregion

        #region Private Methods

        private void RebuildInteraction()
        {
            parameter = m_opened ? closeText : openText;
            if (m_opened)
            {
                callback = Close;
            }
            else
            {
                callback = Open;
            }

        }

        private IEnumerator WaitForOpenClose()
        {
            yield return new WaitForSeconds(openCloseDuration);
            lockInteractions = false;

        }

        #endregion

    }
}