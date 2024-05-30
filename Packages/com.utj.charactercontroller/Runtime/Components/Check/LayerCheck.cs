using System.Linq;
using Unity.TinyCharacterController.Interfaces.Components;
using Unity.TinyCharacterController.Interfaces.Core;
using Unity.TinyCharacterController.Utility;
using UnityEngine;
using UnityEngine.Events;

namespace Unity.TinyCharacterController.Check
{
    /// <summary>
    /// A component that performs collision detection with special layer.
    /// When a collision with a layer occurs, callbacks are triggered during the collision,
    /// while in contact with the layer, and when the character moves away from the layer.
    /// </summary>
    [AddComponentMenu(MenuList.MenuCheck + nameof(LayerCheck))]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CharacterSettings))]
    [Unity.VisualScripting.RenamedFrom("TinyCharacterController.LayerCheck")]
    [Unity.VisualScripting.RenamedFrom("TinyCharacterController.Check.LayerCheck")]
    public class LayerCheck : MonoBehaviour, IEarlyUpdateComponent, IWallCheck
    {
        [Header("Settings")] [SerializeField] private LayerMask layersToReact;

        [Header("Callbacks")] public UnityEvent onLayerTouch;

        public UnityEvent onLayerLeft;
        public UnityEvent onLayerStuck;
        
        private int _order;
        private IBrain _brain;
        private ITransform _transform;
        private CharacterSettings _settings;

        private bool _isFirstContact;
        
        private Vector3 _normal;
        
        /// <summary>
        /// If there is contact, it returns True.
        /// </summary>
        public bool IsContact { get; private set; }

        /// <summary>
        /// Returns normal vector of the contact surface. If there is no contact, it returns Vector3.Zero.
        /// </summary>
        public Vector3 Normal => _normal;
        
        private void Awake()
        {
            TryGetComponent(out _brain);
            TryGetComponent(out _transform);
            TryGetComponent(out _settings);
            IsContact = false;
            _normal = Vector3.zero;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!IsCollisionWithLayerMask(collision, layersToReact)) return;
            IsContact = true;
            _isFirstContact = true;
            foreach (var contact in collision.contacts)
            {
                _normal += contact.normal;
            }
            
            _normal.Normalize();
        }

        private void OnCollisionStay(Collision collision)
        {
            if (!IsCollisionWithLayerMask(collision, layersToReact)) return;
            IsContact = true;
            _isFirstContact = false;
            foreach (var contact in collision.contacts)
            {
                _normal += contact.normal;
            }

            _normal.Normalize();
        }
        
        private void OnCollisionExit(Collision collision)
        {
            if (IsCollisionWithLayerMask(collision, layersToReact)) return;
            IsContact = false;
            _isFirstContact = false;
            _normal = Vector3.zero;
        }

        private bool IsCollisionWithLayerMask(Collision collision, LayerMask layerMask)
        {
            return collision.contacts.Any(contact => (layerMask & (1 << contact.otherCollider.gameObject.layer)) != 0);
        }

        void IEarlyUpdateComponent.OnUpdate(float deltaTime)
        {
            if (enabled == false)
                return;
            
            if (IsContact)
            {
                onLayerStuck?.Invoke();
            }
            
            if(IsContact && _isFirstContact)
            {
                onLayerTouch?.Invoke();
            }
            
            if (!IsContact && !_isFirstContact)
            {
                onLayerLeft?.Invoke();
            }
        }
        
        //write a gizmo method
        private void OnDrawGizmosSelected()
        {
            if (Application.isPlaying == false)
                return;
            
            if (!IsContact) return;
            Gizmos.color = Color.red;
            Gizmos.DrawRay(_transform.Position, _normal);
        }
        
        int IEarlyUpdateComponent.Order => Order.Check;
    }
}