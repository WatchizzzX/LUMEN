//
// Game Developers Toolkit © 2023 by Thomas W Holtquist is licensed under CC BY-SA 4.0 
// https://www.nullsave.com
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NullSave.GDTK
{
    [AutoDocLocation("interaction/interactables")]
    [AutoDoc("The Interactable Door component enables a door to be opened/closed.")]
    public class InteractableDoor : InteractableObject
    {

        #region Fields

        [Tooltip("Boolean parameter to set on the animator")] public string openBoolAnim;
        [Tooltip("Determine if the door is open")][SerializeField] private bool isOpen;
        [Tooltip("Time it takes to open/close the door (interaction is disabled during this time)")] public float openCloseDuration;

        [Tooltip("Text to display when door can be opened")] public string openText;
        [Tooltip("Text to display when door can be closed")] public string closeText;

        [Tooltip("Sound to play on open")] public AudioClip openSound;
        [Tooltip("Sound to play on close")] public AudioClip closeSound;

        [Tooltip("Event raised when the door is opened")] public UnityEvent onOpen;
        [Tooltip("Event raised when the door has finished opening")] public UnityEvent onOpenComplete;
        [Tooltip("Event raised when the door is closed")] public UnityEvent onClose;
        [Tooltip("Event raised when the door has finished closing")] public UnityEvent onCloseComplete;

        private List<InteractableChild> children;
        private bool busy;

        #endregion

        #region Properties

        [AutoDoc("Attached Animator")]
        public Animator Animator { get; private set; }

        [AutoDoc("Text to display in the UI")]
        public override string InteractionText
        {
            get
            {
                if (isOpen)
                {
                    return closeText;
                }
                return openText;
            }
        }

        [AutoDoc("Is the object currently interactable")]
        public override bool IsInteractable
        {
            get
            {
                if (busy) return false;
                return interactable;
            }
            set
            {
                interactable = value;
            }
        }

        [AutoDoc("Is door currently open")]
        public bool IsOpen
        {
            get { return isOpen; }
            set
            {
                if (isOpen == value) return;
                busy = true;
                isOpen = value;
                if (Animator != null)
                {
                    Animator.SetBool(openBoolAnim, value);
                }
                StartCoroutine(MonitorAnimation());
                RaiseEvents();
            }
        }

        [AutoDoc("Attached OcclusionPortal")]
        public OcclusionPortal OcclusionPortal { get; private set; }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            Animator = GetComponent<Animator>();
            OcclusionPortal = GetComponent<OcclusionPortal>();

            if (OcclusionPortal != null)
            {
                OcclusionPortal.open = true;
            }

            children = new List<InteractableChild>();
            InteractableChild[] possibleChildren = GetComponentsInChildren<InteractableChild>();
            foreach (InteractableChild child in possibleChildren)
            {
                if (child.parentInteractable == this)
                {
                    children.Add(child);
                }
            }

            if (Animator != null)
            {
                Animator.SetBool(openBoolAnim, isOpen);
            }
        }

        public override void Reset()
        {
            base.Reset();
            openBoolAnim = "Open";
            openCloseDuration = 1;
            openText = "Open";
            closeText = "Close";
            audioPoolChannel = "Audio";
        }

        private void Start()
        {
            RaiseEvents(false);
            if (OcclusionPortal != null)
            {
                OcclusionPortal.open = IsOpen;
            }
        }

        #endregion

        #region Public Methods

        [AutoDoc("Interact with the object")]
        [AutoDocParameter("Interactor requesting action")]
        public override bool Interact(Interactor source)
        {
            if (!IsInteractable) return false;
            IsOpen = !isOpen;
            onInteract?.Invoke();
            return true;
        }

        #endregion

        #region Private Methods

        private IEnumerator MonitorAnimation()
        {
            if (isOpen)
            {
                if (OcclusionPortal != null) OcclusionPortal.open = true;
                if (children != null)
                {
                    foreach (InteractableChild child in children)
                    {
                        if (child.OcclusionPortal != null) child.OcclusionPortal.open = true;
                    }
                }
            }

            yield return new WaitForSeconds(openCloseDuration);

            if (!isOpen)
            {
                if (OcclusionPortal != null) OcclusionPortal.open = false;
                if (children != null)
                {
                    foreach (InteractableChild child in children)
                    {
                        if (child.OcclusionPortal != null) child.OcclusionPortal.open = false;
                    }
                }
            }

            busy = false;

            if(isOpen)
            {
                onOpenComplete?.Invoke();
            }
            else
            {
                onCloseComplete?.Invoke();
            }
        }

        private void RaiseEvents(bool includeSounds = true)
        {
            if (isOpen)
            {
                onOpen?.Invoke();
                if (includeSounds)
                {
                    Broadcaster.Broadcast(audioPoolChannel, "Play", new object[] { openSound, transform.position });
                }
            }
            else
            {
                onClose?.Invoke();
                if (includeSounds)
                {
                    Broadcaster.Broadcast(audioPoolChannel, "Play", new object[] { closeSound, transform.position });
                }
            }
        }

        #endregion

    }
}