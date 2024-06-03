using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using Utils.Extra;
using Logger = Utils.Extra.Logger;

namespace InteractionSystem.Interactables
{
    /// <summary>
    /// Standard implementation of pushable ground pad, with simple animation, and event OnPush
    /// </summary>
    public class InteractablePad : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>
        /// Event on change state (Push/Unpush)
        /// </summary>
        [Tooltip("On change state (Push/Unpush)")] [SerializeField]
        private UnityEvent onPush;

        /// <summary>
        /// The height to which the button will sink down
        /// </summary>
        [Tooltip("The height to which the button will sink down")] [SerializeField]
        private float pushOffset = 0.1f;

        /// <summary>
        /// The time it takes for the button to sink down
        /// </summary>
        [Tooltip("The time it takes for the button to sink down")] [SerializeField]
        private float changeTime = 0.2f;

        /// <summary>
        /// A visual object that will sink down
        /// </summary>
        [Tooltip("A visual object that will sink down")] [SerializeField]
        private Transform visualObject;

        #endregion

        #region Private Variables

        /// <summary>
        /// Is correctly initialized?
        /// </summary>
        private bool _isInitialized;

        /// <summary>
        /// Internal state of pad
        /// </summary>
        private bool _isEnabled;

        /// <summary>
        /// Internal counter of collision counts. Required to correctly check with multiple objects
        /// </summary>
        private int _collisionCount;
        
        /// <summary>
        /// Cached start position of visual object
        /// </summary>
        private Vector3 _startPosition;

        #endregion

        #region MonoBehaviour

        private void Awake()
        {
            if (visualObject == null)
            {
                Logger.Log(LoggerChannel.InteractableSystem, Priority.Error,
                    $"(InteractablePad) On {name} didn't assigned visual object");
                return;
            }

            _startPosition = visualObject.transform.position;
            _isInitialized = true;
        }
        
        private void OnCollisionEnter(Collision other)
        {
            _collisionCount++;
            EvaluateCollisions();
        }

        private void OnCollisionExit(Collision other)
        {
            _collisionCount--;
            EvaluateCollisions();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Invoke event
        /// </summary>
        private void InvokeEvent()
        {
            onPush.Invoke();
        }

        /// <summary>
        /// Evaluate collisions and update internal state
        /// </summary>
        private void EvaluateCollisions()
        {
            if (!_isInitialized) return;

            var result = _collisionCount > 0;
            if (result == _isEnabled) return;

            _isEnabled = result;
            Animate();
            InvokeEvent();
        }

        /// <summary>
        /// Animate visual object. Based on internal state
        /// </summary>
        private void Animate()
        {
            var targetVector = _startPosition;
            if (_isEnabled)
            {
                targetVector.y -= pushOffset;
                visualObject.transform.DOMove(targetVector, changeTime);
            }
            else
            {
                targetVector.y += pushOffset;
                visualObject.transform.DOMove(targetVector, changeTime);
            }
        }

        #endregion
    }
}