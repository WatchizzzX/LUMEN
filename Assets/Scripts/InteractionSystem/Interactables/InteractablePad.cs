using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using Utils;
using Logger = Utils.Logger;

namespace InteractionSystem.Interactables
{
    public class InteractablePad : MonoBehaviour
    {
        [SerializeField] private UnityEvent onPush;
        [SerializeField] private float pushOffset = 0.1f;
        [SerializeField] private float changeTime = 0.2f;
        [SerializeField] private Transform visualObject;

        private bool _isInitialized;

        private bool _isEnabled;

        private int _collisionCount;
        private Vector3 _startPosition;

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

        private void InvokeEvent()
        {
            onPush.Invoke();
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

        private void EvaluateCollisions()
        {
            if (!_isInitialized) return;
            
            var result = _collisionCount > 0;
            if (result == _isEnabled) return;

            _isEnabled = result;
            Animate();
            InvokeEvent();
        }

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
    }
}