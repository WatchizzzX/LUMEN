using UnityEngine;

namespace NullSave.GDTK
{
    [RequireComponent(typeof(RectTransform))]
    [AutoDocLocation("ui")]
    [AutoDoc("This component provides access for a UI element to be an Input Cursor")]
    public class UICursor : MonoBehaviour
    {

        #region Members

        [AutoDoc("Get/Set clickable element")]
        public GameObject ClickableElement { get; set; }

        [AutoDoc("Gets the pointer Rect Transform")]
        public RectTransform RectTransform { get; private set; }

        [AutoDoc("Gets if the pointer is over a clickable element")]
        public bool IsOverClickeableElement
        {
            get
            {
                return ClickableElement != null ? true : false;
            }
        }

        #endregion

        #region Public Methods

        [AutoDoc("Method called when clickable element changes")]
        public virtual void OnClickableElementChanged() { }

        [AutoDoc("Method called on click")]
        public virtual void OnClick() { }

        #endregion

        #region Private Methods

        protected virtual void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }

        #endregion

    }
}