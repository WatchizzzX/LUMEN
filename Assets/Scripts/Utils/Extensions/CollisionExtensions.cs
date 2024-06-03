using System.Linq;
using UnityEngine;

namespace Utils.Extensions
{
    public static class CollisionExtensions
    {
        public static bool IsCollisionWithLayerMask(this Collision collision, LayerMask layerMask)
        {
            return collision.contacts.Any(contact => (layerMask & (1 << contact.otherCollider.gameObject.layer)) != 0);
        }
    }
}