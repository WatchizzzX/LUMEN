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
    [AddComponentMenu(MenuList.MenuCheck + nameof(WallCheck))]
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
        
        private Vector3 _normal;
        private Vector3 _hitPoint;
        private Collider _contactObject ;
        
        /// <summary>
        /// If there is contact, it returns True.
        /// </summary>
        public bool IsContact { get; private set; }

        /// <summary>
        /// Returns normal vector of the contact surface. If there is no contact, it returns Vector3.Zero.
        /// </summary>
        public Vector3 Normal => _normal;

        public GameObject ContactObject => _contactObject.gameObject;

        public Vector3 HitPoint => _hitPoint;
        
        private void Awake()
        {
            TryGetComponent(out _brain);
            TryGetComponent(out _transform);
            TryGetComponent(out _settings);
        }

        void IEarlyUpdateComponent.OnUpdate(float deltaTime)
        {
            
        }
        
        int IEarlyUpdateComponent.Order => Order.Check;
    }
}