using System.Linq;
using UnityEngine;
using Utils;
using Logger = Utils.Logger;

namespace InteractionSystem
{
    public class InteractorController : MonoBehaviour
    {
        [SerializeField] private LayerMask interactableLayer;
        [SerializeField, Range(0.1f, 5f)] private float interactiveRange = 0.5f;
        [SerializeField] private int bufferSize = 3;

        private Collider[] _colliders;
        private int _interactableObjectsCount;
        private GameObject _closestInteractableObject;
        private IInteractable _interactable;
        private InteractableType _interactableType;

        private void Start()
        {
            _colliders = new Collider[bufferSize];
        }

        public void Interact()
        {
            _interactableObjectsCount =
                Physics.OverlapSphereNonAlloc(transform.position, interactiveRange, _colliders, interactableLayer);

            if (_interactableObjectsCount <= 0)
            {
                return;
            }

            _closestInteractableObject = _colliders.OrderBy(obj =>
                obj ? Vector3.Distance(obj.transform.position, transform.position) : Mathf.Infinity
            ).First().gameObject;

            _interactable = _closestInteractableObject.GetComponent<IInteractable>();

            if (_interactable == null)
            {
                Logger.Log(LoggerChannel.InteractableSystem, Priority.Warning,
                    $"{_closestInteractableObject.name} on Interactable layer, but don't have a InteractableScript");
                return;
            }

            _interactableType = _interactable.GetInteractableType();
            
            if (_interactableObjectsCount == 0) return;

            _interactable.Interact(this);
        }
    }
}