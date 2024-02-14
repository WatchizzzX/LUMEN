using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace NullSave.GDTK
{

    [AutoDocSuppress]
    public class MouseState
    {

        #region Members

        private List<ButtonState> m_TrackedButtons;

        #endregion

        #region Constructor

        public MouseState()
        {
            m_TrackedButtons = new List<ButtonState>();
        }

        #endregion

        #region Public Methods

        public bool AnyPressesThisFrame()
        {
            for (int index = 0; index < this.m_TrackedButtons.Count; ++index)
            {
                if (this.m_TrackedButtons[index].eventData.PressedThisFrame())
                    return true;
            }
            return false;
        }

        public bool AnyReleasesThisFrame()
        {
            for (int index = 0; index < this.m_TrackedButtons.Count; ++index)
            {
                if (this.m_TrackedButtons[index].eventData.ReleasedThisFrame())
                    return true;
            }
            return false;
        }

        public ButtonState GetButtonState(PointerEventData.InputButton button)
        {
            ButtonState buttonState = null;
            for (int index = 0; index < this.m_TrackedButtons.Count; ++index)
            {
                if (this.m_TrackedButtons[index].button == button)
                {
                    buttonState = this.m_TrackedButtons[index];
                    break;
                }
            }
            if (buttonState == null)
            {
                buttonState = new ButtonState()
                {
                    button = button,
                    eventData = new MouseButtonEventData()
                };
                this.m_TrackedButtons.Add(buttonState);
            }
            return buttonState;
        }

        public void SetButtonState(PointerEventData.InputButton button, PointerEventData.FramePressState stateForMouseButton, PointerEventData data)
        {
            ButtonState buttonState = this.GetButtonState(button);
            buttonState.eventData.buttonState = stateForMouseButton;
            buttonState.eventData.buttonData = data;
        }

        #endregion

    }

}