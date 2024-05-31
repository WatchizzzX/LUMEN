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

        private bool _isFirstContact;
        private bool _cachedContact;
        
        /// <summary>
        /// If there is contact, it returns True.
        /// </summary>
        public bool IsContact { get; private set; }
        
        /// <summary>
        /// If there is contact, it returns GameObject, otherwise it returns null.
        /// </summary>
        public GameObject ContactedGameObject { get; private set; }

        /// <summary>
        /// Returns normal vector of the contact surface. If there is no contact, it returns Vector3.Zero.
        /// </summary>
        public Vector3 Normal { get; private set; }
        
        private void Awake()
        {
            IsContact = false;
            Normal = Vector3.zero;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!IsCollisionWithLayerMask(collision, layersToReact)) return;
            IsContact = true;
            _isFirstContact = true;
            Normal = collision.contacts[0].normal;
            ContactedGameObject = collision.contacts[0].otherCollider.gameObject;
        }

        private void OnCollisionStay(Collision collision)
        {
            if (!IsCollisionWithLayerMask(collision, layersToReact)) return;
            IsContact = true;
            _isFirstContact = false;
            Normal = collision.contacts[0].normal;
            ContactedGameObject = collision.contacts[0].otherCollider.gameObject;
        }
        
        private void OnCollisionExit(Collision collision)
        {
            if (IsCollisionWithLayerMask(collision, layersToReact)) return;
            IsContact = false;
            _isFirstContact = false;
            Normal = Vector3.zero;
            ContactedGameObject = null;
        }

        private bool IsCollisionWithLayerMask(Collision collision, LayerMask layerMask)
        {
            return collision.contacts.Any(contact => (layerMask & (1 << contact.otherCollider.gameObject.layer)) != 0);
        }

        void IEarlyUpdateComponent.OnUpdate(float deltaTime)
        {
            if (enabled == false)
                return;
            
            if (_cachedContact != IsContact)
            {
                _cachedContact = true;
            }
            
            if (IsContact)
            {
                onLayerStuck?.Invoke();
            }
            
            if(IsContact && _isFirstContact)
            {
                onLayerTouch?.Invoke();
            }
            
            if (IsContact || _isFirstContact || !_cachedContact) return;
            
            onLayerLeft?.Invoke();
            _cachedContact = false;
        }
        
        int IEarlyUpdateComponent.Order => Order.Check;
    }
}