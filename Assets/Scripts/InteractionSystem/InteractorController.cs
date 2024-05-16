using System.Linq;
using UnityEngine;
using Utils;
using Utils.Extensions;
using Logger = Utils.Logger;

namespace InteractionSystem
{
    /// <summary>
    /// Interactor controller. Work with BasicInteractable implementations
    /// </summary>
    public class InteractorController : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>
        /// Interactive layer
        /// </summary>
        [Tooltip("Which layer will be as Interactive layer")] [SerializeField]
        private LayerMask interactableLayer;

        /// <summary>
        /// Interaction range
        /// </summary>
        [Tooltip("Range in which InteractorController work")] [SerializeField, Range(0.1f, 5f)]
        private float interactiveRange = 0.5f;

        /// <summary>
        /// Always interaction range
        /// </summary>
        [Tooltip("Range in which InteractorController always work")] [SerializeField, Range(0.1f, 5f)]
        private float alwaysInteractiveRange = 0.2f;

        [SerializeField, Range(1f, 360f)] private int angleInteractiveRange = 45;

        /// <summary>
        /// A buffer into which all interactive objects found in the area will be placed. Size affects performance
        /// </summary>
        [Tooltip("Buffer size. Optimal size is 3")] [SerializeField]
        private int bufferSize = 3;

        #endregion

        #region Private Variables

        /// <summary>
        /// Internal buffer of interactables objects
        /// </summary>
        private Collider[] _colliders;

        /// <summary>
        /// Count of founded interactable objects. Max is bufferSize
        /// </summary>
        private int _interactableObjectsCount;

        /// <summary>
        /// The nearest object among those found
        /// </summary>
        private GameObject _closestInteractableObject;

        /// <summary>
        /// Cached interactable implementation
        /// </summary>
        private IInteractable _interactable;

        #endregion

        #region MonoBehaviour

        private void Start()
        {
            _colliders = new Collider[bufferSize];
        }

        #endregion

        #region Methods

        /// <summary>
        /// A public method for processing interactive event
        /// </summary>
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

            var directionToInteractable =
                (_closestInteractableObject.transform.position.ToXZVector2() - transform.position.ToXZVector2())
                .normalized;

            if (Vector3.Distance(_closestInteractableObject.transform.position, transform.position) <=
                alwaysInteractiveRange)
            {
                CallToInteract();
                return;
            }

            var angle = Mathf.Abs(Vector2.Angle(transform.forward.ToXZVector2(), directionToInteractable));

            if (angle < angleInteractiveRange) CallToInteract();
        }

        private void CallToInteract()
        {
            if (_interactable == null)
            {
                Logger.Log(LoggerChannel.InteractableSystem, Priority.Warning,
                    $"{_closestInteractableObject.name} on Interactable layer, but don't have a InteractableScript");
                return;
            }

            if (_interactableObjectsCount == 0) return;

            _interactable.Interact(this);
        }

        #endregion
    }
}