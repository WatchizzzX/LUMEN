using UnityEngine;
using UnityEngine.Events;

namespace NullSave.GDTK
{
    [AutoDocLocation("interaction/rpg-actions")]
    [AutoDoc("The RPG Action adds trapped and disarming to an Interactable RPG Object.")]
    public class RPGTrappedAction : RPGAction
    {

        #region Fields

        [Tooltip("Indicates if the object is currently trapped")][SerializeField] private bool m_trapped;
        [Tooltip("Text to display when object can be disarmed")] public string disarmText;
        [Tooltip("Chance of success")][Range(0, 1)][SerializeField] private float successChance;
        [Tooltip("Name of the broadcaster channel to use with the audio ppol")] public string audioPoolChannel;
        [Tooltip("Sound to play on successful lock picking")] public AudioClip successSound;
        [Tooltip("Sound to play on failed lock picking")] public AudioClip failureSound;
        [Tooltip("Event raised when disarming succeeds")] public UnityEvent onSuccess;
        [Tooltip("Event raised when disarming fails")] public UnityEvent onFail;
        [Tooltip("Event raised when trap is triggered")] public UnityEvent onTriggered;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            RebuildInteraction();
        }

        private void Reset()
        {
            disarmText = "Disarm";
            successChance = 0.9f;
        }

        #endregion

        #region Public Methods

        [AutoDoc("Disarm trap on the object")]
        public void Disarm()
        {
            if (successChance >= Random.Range(0, 1f))
            {
                m_trapped = false;
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

        [AutoDoc("Trigger the trap")]
        public void Trigger()
        {
            if (!m_trapped) return;
            m_trapped = false;
            onTriggered?.Invoke();
            RebuildInteraction();
        }

        #endregion

        #region Private Methods

        private void RebuildInteraction()
        {
            if (m_trapped)
            {
                parameter = disarmText;
                callback = Disarm;
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