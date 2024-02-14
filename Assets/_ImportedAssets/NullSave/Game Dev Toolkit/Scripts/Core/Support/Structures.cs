using UnityEngine;

namespace NullSave.GDTK
{

    public struct DestroySyncRequest
    {
        public GameObject target;

        public DestroySyncRequest(GameObject target)
        {
            this.target = target;
        }
    }

    public struct ActivationSyncRequest
    {
        public GameObject target;
        public bool active;

        public ActivationSyncRequest(GameObject target, bool active)
        {
            this.target = target;
            this.active = active;
        }
    }

}