using UnityEngine.EventSystems;

namespace NullSave.GDTK
{
    [AutoDocSuppress]
    public class MouseButtonEventData
    {

        #region Members

        public PointerEventData.FramePressState buttonState;
        public PointerEventData buttonData;

        #endregion

        #region Public Methods

        /// <summary>
        ///   <para>Was the button pressed this frame?</para>
        /// </summary>
        public bool PressedThisFrame()
        {
            if (this.buttonState != PointerEventData.FramePressState.Pressed)
            {
                return this.buttonState == PointerEventData.FramePressState.PressedAndReleased;
            }
            return true;
        }

        /// <summary>
        ///   <para>Was the button released this frame?</para>
        /// </summary>
        public bool ReleasedThisFrame()
        {
            if (this.buttonState != PointerEventData.FramePressState.Released)
            {
                return this.buttonState == PointerEventData.FramePressState.PressedAndReleased;
            }
            return true;
        }

        #endregion

    }
}