using System;
using System.Collections;
using System.Linq;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using Utils;
using Logger = Utils.Logger;

namespace PickupSystem
{
    public class PickupController : MonoBehaviour
    {
        [SerializeField] private Transform holdPoint;
        [SerializeField] private Transform frontHoldPoint;
        [SerializeField, Min(0.1f)] private float pickupRadius = 1f;
        [SerializeField] private float pickupForce = 150f;
        [SerializeField] private int bufferSize = 2;
        [SerializeField] private LayerMask pickableLayer;
        [SerializeField] private float timeToMove = 0.2f;

        private GameObject _heldGameObject;
        private Rigidbody _heldRigidbody;

        private bool _isHoldingObject;

        private Collider[] _colliders;
        private int _pickableObjectCount;
        private GameObject _closestPickableObject;

        private void Awake()
        {
            _colliders = new Collider[bufferSize];
        }

        private void Update()
        {
            if (_isHoldingObject)
                MoveObject();
        }

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

                if (!IsControllerFacingToObject(_closestPickableObject, 45f)) return;

                PickupObject(foundRigidbody);
            }
        }

        private bool IsControllerFacingToObject(GameObject targetObject, float fieldOfView)
        {
            var directionToTarget = targetObject.transform.position - transform.position;
            var angle = Vector3.Angle(transform.forward, directionToTarget);

            return angle <= fieldOfView;
        }

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
            animationSequence.AppendCallback(() => { _heldGameObject.transform.SetParent(frontHoldPoint); }).
                Append(_heldGameObject.transform.DOLocalMove(Vector3.zero, timeToMove)).
                AppendCallback(() => {_heldGameObject.transform.SetParent(holdPoint);}).
                Append(_heldGameObject.transform.DOLocalMove(Vector3.zero, timeToMove));
        }

        private void UnchildObject()
        {
            _isHoldingObject = false;
            var animationSequence = DOTween.Sequence();
            animationSequence.AppendCallback(() => { _heldGameObject.transform.SetParent(frontHoldPoint); })
                .Append(_heldGameObject.transform.DOLocalMove(Vector3.zero, timeToMove))
                .AppendCallback(DropObject);
        }

        private void DropObject()
        {
            _heldGameObject.transform.SetParent(null);
            _heldRigidbody.useGravity = true;
            _heldRigidbody.drag = 1;
            _heldRigidbody.constraints = RigidbodyConstraints.None;

            _heldGameObject = null;
            _heldRigidbody = null;
        }

        private void MoveObject()
        {
            if (!(Vector3.Distance(_heldGameObject.transform.position, holdPoint.position) > 0.1f)) return;

            var moveDirection = holdPoint.position - _heldGameObject.transform.position;
            _heldRigidbody.AddForce(moveDirection * pickupForce);
        }
    }
}