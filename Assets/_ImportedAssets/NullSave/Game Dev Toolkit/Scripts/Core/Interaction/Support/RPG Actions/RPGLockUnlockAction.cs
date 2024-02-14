using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NullSave.GDTK
{
    [AutoDocLocation("interaction/rpg-actions")]
    [AutoDoc("The RPG Action adds locking and unlocking to an Interactable RPG Object.")]
    public class RPGLockUnlockAction : RPGAction
    {

        #region Fields

        [Tooltip("Indicates if the object is locked and cannot be opened")][SerializeField] private bool m_locked;
        [Tooltip("Can the object be locked by interaction")] public bool canLock;
        [Tooltip("Text to display when object can be unlocked")] public string unlockText;
        [Tooltip("Text to display when object can be locked")] public string lockText;
        [Tooltip("Name of the broadcaster channel to use with the audio ppol")] public string audioPoolChannel;
        [Tooltip("Sound to play on lock/unlock")] public AudioClip lockSound;
        [Tooltip("Event raised when the object becomes locked")] public UnityEvent onLocked;
        [Tooltip("Event raised when the object becomes unlocked")] public UnityEvent onUnlocked;

        [Tooltip("List of actions to forbid when locked")] public List<RPGAction> forbidWhenLocked;
        [Tooltip("List of actions to forbid when unlocked")] public List<RPGAction> forbidWhenUnlocked;

        #endregion

        #region Properties

        [AutoDoc("List of other action to prevent")]
        public override List<RPGAction> preventActions
        {
            get
            {
                if (m_locked) return forbidWhenLocked;
                return forbidWhenUnlocked;
            }
        }

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            RebuildInteraction();
        }

        private void Reset()
        {
            unlockText = "Unlock";
            lockText = "Lock";
        }

        #endregion

        #region Public Methods

        [AutoDoc("Force unlock the object")]
        public void ForceUnlock()
        {
            m_locked = false;
            RebuildInteraction();
        }

        [AutoDoc("Lock the object")]
        public void Lock()
        {
            m_locked = true;
            RebuildInteraction();
        }

        [AutoDoc("Unlock the object")]
        public void Unlock()
        {
            m_locked = false;
            RebuildInteraction();
        }

        #endregion

        #region Private Methods

        private void RebuildInteraction()
        {
            if (m_locked)
            {
                parameter = unlockText;
                callback = Unlock;
            }
            else if (canLock)
            {
                parameter = lockText;
                callback = Lock;
            }
            else
            {
                parameter = null;
                callback = null;
            }
        }

        #endregion

    }
}