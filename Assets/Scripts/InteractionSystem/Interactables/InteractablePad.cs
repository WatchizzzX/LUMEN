using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace InteractionSystem.Interactables
{
    public class InteractablePad :BasicInteractable
    {
        [SerializeField] private UnityEvent onPush;
        [SerializeField] private float pushOffset = 0.1f;
        [SerializeField] private float changeTime = 0.2f;

        private bool _isEnabled;
        
        public override void Interact()
        {
        }

        public override void Interact(InteractorController interactor)
        {
        }

        private void InvokeEvent()
        {
            onPush.Invoke();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;
            
            _isEnabled = true;
            InvokeEvent();
            Animate();
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;
            
            _isEnabled = false;
            InvokeEvent();
            Animate();
        }

        private void Animate()
        {
            var targetVector = transform.position;
            if (_isEnabled)
            {
                targetVector.y -= pushOffset;
                transform.DOMove(targetVector, changeTime);
            }
            else
            {
                targetVector.y += pushOffset;
                transform.DOMove(targetVector, changeTime);
            }
        }
    }
}