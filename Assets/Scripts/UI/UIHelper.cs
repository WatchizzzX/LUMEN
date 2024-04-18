using UnityEngine;

namespace UI
{
    public class UIHelper : MonoBehaviour
    {
        public void ChangeVisibility()
        {
            //It's awful
            transform.localScale = (transform.localScale == Vector3.one) ? Vector3.zero : Vector3.one;
        }
    }
}