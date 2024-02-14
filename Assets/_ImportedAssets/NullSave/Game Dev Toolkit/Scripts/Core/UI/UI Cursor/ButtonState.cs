using UnityEngine.EventSystems;

namespace NullSave.GDTK
{

    [AutoDocSuppress]
    public class ButtonState
    {

        #region Properties

        public MouseButtonEventData eventData { get; set; }

        public PointerEventData.InputButton button { get; set; }

        #endregion

    }

}