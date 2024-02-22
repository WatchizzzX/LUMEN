using System.Linq;
using DG.Tweening;
using UnityEngine;
using Utils;
using Logger = Utils.Logger;

namespace PickupSystem
{
    /// <summary>
    /// Main controller in PickupSystem
    /// </summary>
    public class PickupController : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>
        /// Point where be a holding object
        /// </summary>
        [Header("Hold Points")] [SerializeField] [Tooltip("Point where be a holding object")]
        private Transform holdPoint;

        /// <summary>
        /// The point through which the object will pass before reaching the holding point
        /// </summary>
        [Tooltip("The point through which the object will pass before reaching the holding point")] [SerializeField]
        private Transform frontHoldPoint;

        /// <summary>
        /// Radius for pickup ability
        /// </summary>
        [Space(2f)] [Header("Pickup Settings")] [SerializeField, Min(0.1f)] [Tooltip("Radius for pickup ability")]
        private float pickupRadius = 1f;

        /// <summary>
        /// The force with which the object will be held at a point
        /// </summary>
        [Tooltip("The force with which the object will be held at a point")] [SerializeField]
        private float pickupForce = 150f;

        /// <summary>
        /// The time it takes for an object to travel from one point to another
        /// </summary>
        [Tooltip("The time it takes for an object to travel from one point to another")] [SerializeField]
        private float timeToMove = 0.2f;

        /// <summary>
        /// Pickable Layer
        /// </summary>
        [Tooltip("Pickable layer")] [SerializeField]
        private LayerMask pickableLayer;

        /// <summary>
        /// The viewing radius in which the controller is able to pick up an object
        /// </summary>
        [Tooltip("The viewing radius in which the controller is able to pick up an object")] [SerializeField]
        private float angelOfView = 65f;

        /// <summary>
        /// A buffer into which all pickable objects found in the area will be placed. Size affects performance
        /// </summary>
        [Space(2f)] [Header("Other Settings")] [SerializeField] [Tooltip("Buffer size. Optimal size is 3")]
        private int bufferSize = 2;

        #endregion

        #region Private Variables

        /// <summary>
        /// Held GameObject
        /// </summary>
        private GameObject _heldGameObject;

        /// <summary>
        /// Rigidbody of held GameObject
        /// </summary>
        private Rigidbody _heldRigidbody;

        /// <summary>
        /// Internal state if some object are held
        /// </summary>
        private bool _isHoldingObject;

        /// <summary>
        /// Founded pickable objects
        /// </summary>
        private Collider[] _colliders;

        /// <summary>
        /// Count of founded pickable object. Max is bufferSize
        /// </summary>
        private int _pickableObjectCount;

        /// <summary>
        /// Closest GameObject to controller
        /// </summary>
        private GameObject _closestPickableObject;

        #endregion

        #region MonoBehaviour

        private void Awake()
        {
            _colliders = new Collider[bufferSize];
        }

        private void Update()
        {
            if (_isHoldingObject)
                MoveObject();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Event handler on pickup event
        /// </summary>
        public void OnPickupEvent()
        {
            if (_isHoldingObject)
            {
                UnchildObject();
            }
            else
            {
                _pickableObjectCount =
                    Physics.OverlapSphereNonAlloc(transform.position, pickupRadius, _colliders, pickableLayer);

                if (_pickableObjectCount <= 0)
                {
                    return;
                }

                _closestPickableObject = _colliders.OrderBy(
                    obj => obj ? Vector3.Distance(obj.transform.position, transform.position) : Mathf.Infinity
                ).First().gameObject;

                if (!_closestPickableObject.TryGetComponent(out Rigidbody foundRigidbody))
                {
                    Logger.Log(LoggerChannel.PickableSystem, Priority.Warning,
                        $"{_closestPickableObject.name} was on PickableLayer, but doesn't have a Rigidbody component");

                    return;
                }

                if (!IsControllerFacingToObject(_closestPickableObject, angelOfView)) return;

                PickupObject(foundRigidbody);
            }
        }

        /// <summary>
        /// Check if current object turned towards to targetObject
        /// </summary>
        /// <param name="targetObject">Target object</param>
        /// <param name="fieldOfView">Angle of view to check</param>
        /// <returns>True if current object turning towards to targetObject, and false if not</returns>
        private bool IsControllerFacingToObject(GameObject targetObject, float fieldOfView)
        {
            var directionToTarget = targetObject.transform.position - transform.position;
            // ReSharper disable once Unity.InefficientPropertyAccess
            var angle = Vector3.Angle(transform.forward, directionToTarget);

            return angle <= fieldOfView;
        }

        /// <summary>
        /// Pickup closest object
        /// </summary>
        /// <param name="foundRigidbody">Founded closest Rigidbody</param>
        private void PickupObject(Rigidbody foundRigidbody)
        {
            _isHoldingObject = true;
            _heldGameObject = _closestPickableObject;
            _heldRigidbody = foundRigidbody;
            Logger.Log(LoggerChannel.PickableSystem, Priority.Info, $"Pickup object: {_heldGameObject.name}");

            _heldRigidbody.useGravity = false;
            _heldRigidbody.drag = 5;
            _heldRigidbody.constraints = RigidbodyConstraints.FreezeRotation;

            var animationSequence = DOTween.Sequence();
            animationSequence.AppendCallback(() => { _heldGameObject.transform.SetParent(frontHoldPoint); })
                .Append(_heldGameObject.transform.DOLocalMove(Vector3.zero, timeToMove))
                .AppendCallback(() => { _heldGameObject.transform.SetParent(holdPoint); })
                .Append(_heldGameObject.transform.DOLocalMove(Vector3.zero, timeToMove));
        }

        /// <summary>
        /// Start a sequence to translate object from holdPoint to frontHoldPoint and call DropObject, to enable physics for object
        /// </summary>
        private void UnchildObject()
        {
            _isHoldingObject = false;
            var animationSequence = DOTween.Sequence();
            animationSequence.AppendCallback(() => { _heldGameObject.transform.SetParent(frontHoldPoint); })
                .Append(_heldGameObject.transform.DOLocalMove(Vector3.zero, timeToMove))
                .AppendCallback(DropObject);
        }

        /// <summary>
        /// Drop object
        /// </summary>
        private void DropObject()
        {
            _heldGameObject.transform.SetParent(null);
            _heldRigidbody.useGravity = true;
            _heldRigidbody.drag = 1;
            _heldRigidbody.constraints = RigidbodyConstraints.None;

            _heldGameObject = null;
            _heldRigidbody = null;
        }

        /// <summary>
        /// Smoothly move object to holdPoint
        /// </summary>
        private void MoveObject()
        {
            if (!(Vector3.Distance(_heldGameObject.transform.position, holdPoint.position) > 0.1f)) return;

            var moveDirection = holdPoint.position - _heldGameObject.transform.position;
            _heldRigidbody.AddForce(moveDirection * pickupForce);
        }

        #endregion
    }
}