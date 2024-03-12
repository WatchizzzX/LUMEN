using UnityEngine;

namespace PickupSystem
{
    public class PickupObject : MonoBehaviour
    {
        private Vector3 _spawnPosition;

        private void Awake()
        {
            _spawnPosition = transform.position;
        }

        public void Respawn()
        {
            transform.position = _spawnPosition;
        }
    }
}