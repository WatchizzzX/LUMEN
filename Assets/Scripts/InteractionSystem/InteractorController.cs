using System.Linq;
using UnityEngine;

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

        //private Tweener _tweener;
        
        //private Typewriter _typewriter;

        private void Start()
        {
            _colliders = new Collider[bufferSize];
        }

        private void Update()
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
            _interactableType = _interactable.GetInteractableType();
        }

        public void Interact()
        {
            if (_interactableObjectsCount == 0) return;
            
            _interactable.Interact(this);
        }
    }
}