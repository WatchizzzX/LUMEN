using UnityEngine;
using UnityEngine.Events;

namespace NullSave.GDTK
{
    [AutoDocLocation("interaction/rpg-actions")]
    [AutoDoc("The RPG Action adds lockpicking to an Interactable RPG Object.")]
    public class RPGLockpickAction : RPGAction
    {

        #region Fields

        [Tooltip("Can the object be lockpicked")] public bool canLockPick;
        [Tooltip("Text to display when object can be lock picked")] public string lockPickText;
        [Tooltip("Chance of success")][Range(0, 1)][SerializeField] private float successChance;
        [Tooltip("Name of the broadcaster channel to use with the audio ppol")] public string audioPoolChannel;
        [Tooltip("Sound to play on successful lock picking")] public AudioClip successSound;
        [Tooltip("Sound to play on failed lock picking")] public AudioClip failureSound;
        [Tooltip("Event raised when lock picking succeeds")] public UnityEvent onSuccess;
        [Tooltip("Event raised when lock picking fails")] public UnityEvent onFail;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            RebuildInteraction();
        }

        private void Reset()
        {
            lockPickText = "Lockpick";
            successChance = 0.9f;
        }

        #endregion

        #region Public Methods

        [AutoDoc("Pick lock on the object")]
        public void PickLock()
        {
            if (successChance >= Random.Range(0, 1f))
            {
                Broadcaster.Broadcast(audioPoolChannel, "Play", new object[] { successSound, transform.position });
                onSuccess?.Invoke();
            }
            else
            {
                Broadcaster.Broadcast(audioPoolChannel, "Play", new object[] { failureSound, transform.position });
                onFail?.Invoke();
            }

            RebuildInteraction();
        }

        #endregion

        #region Private Methods

        private void RebuildInteraction()
        {
            parameter = lockPickText;
            callback = PickLock;
        }

        #endregion

    }

}