using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NullSave.GDTK
{
    [AutoDocSuppress]
    public abstract class UICursorInputModule : BaseInputModule
    {

        #region Constants

        public const int kMouseLeftId = -1;
        public const int kMouseRightId = -2;
        public const int kMouseMiddleId = -3;
        public const int kFakeTouchesId = -4;

        #endregion

        #region Fields

        protected Dictionary<int, PointerEventData> m_PointerData = new Dictionary<int, PointerEventData>();
        private MouseState m_MouseState;
        private bool m_previousSendNavigationEvents;

        /// <summary>
		/// Name of the horizontal axis for cursor movement
		/// </summary>
		[SerializeField, Tooltip("Name of the horizontal axis for cursor movement")]
        protected string m_HorizontalAxis;

        /// <summary>
        /// Name of the vertical axis for cursor movement
        /// </summary>
        [SerializeField, Tooltip("Name of the vertical axis for cursor movement")]
        protected string m_VerticalAxis;

        /// <summary>
        /// Name of the button for click
        /// </summary>
        [SerializeField, Tooltip("Name of the button for click")]
        protected string m_ClickButton;


        /// <summary>
        /// Name of the axis for scroll
        /// </summary>
        [SerializeField, Tooltip("Name of the axis for scroll")]
        protected string m_ScrollAxis;

        /// <summary>
        /// The input x and y for cursor movement.
        /// </summary>
        protected float m_inputX, m_inputY;

        /// <summary>
        /// Input for scroll
        /// </summary>
        protected Vector2 m_Scroll;

        /// <summary>
        /// Current cursor position 
        /// </summary>
        protected Vector2 m_CursorPosition;

        /// <summary>
        /// Cursor image
        /// </summary>
        [Tooltip("Cursor image")]
        public UICursor m_cursor;

        /// <summary>
        /// Last mouse position
        /// </summary>
        private Vector2 m_PreviousMousePosition;

        /// <summary>
        /// Speed on scrolling
        /// </summary>
        [Tooltip("Speed on scrolling")]
        public float m_scrollSpeed;

        #endregion

        #region Properties

        public Vector2 PointerPosition { get { return m_CursorPosition; } }

        /// <summary>
		/// Name of the horizontal axis for cursor movement
		/// </summary>
		public string horizontalAxis
        {
            get { return m_HorizontalAxis; }
            set { m_HorizontalAxis = value; }
        }

        /// <summary>
        /// Name of the vertical axis for cursor movement
        /// </summary>
        public string verticalAxis
        {
            get { return m_VerticalAxis; }
            set { m_VerticalAxis = value; }
        }

        /// <summary>
        /// Name of the button for click
        /// </summary>
        public string clickButton
        {
            get { return m_ClickButton; }
            set { m_ClickButton = value; }
        }

        /// <summary>
        /// Name of the axis for scroll
        /// </summary>
        public string scrollAxis
        {
            get { return m_ScrollAxis; }
            set { m_ScrollAxis = value; }
        }

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            m_MouseState = new MouseState();
        }

        protected virtual new void Reset()
        {
            m_previousSendNavigationEvents = false;
            m_HorizontalAxis = "Horizontal";
            m_VerticalAxis = "Vertical";
            m_ClickButton = "Submit";
            m_ScrollAxis = "Mouse ScrollWheel";
            m_scrollSpeed = 5;
        }

        #endregion

        #region Public Methods

        public override bool IsPointerOverGameObject(int pointerId)
        {
            PointerEventData pointerEventData = this.GetLastPointerEventData(pointerId);
            if (pointerEventData != null)
                return (Object)pointerEventData.pointerEnter != (Object)null;
            return false;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder("<b>Pointer Input Module of type: </b>" + (object)this.GetType());
            stringBuilder.AppendLine();
            using (Dictionary<int, PointerEventData>.Enumerator enumerator = this.m_PointerData.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    KeyValuePair<int, PointerEventData> current = enumerator.Current;
                    if (current.Value != null)
                    {
                        stringBuilder.AppendLine("<B>Pointer:</b> " + (object)current.Key);
                        stringBuilder.AppendLine(current.Value.ToString());
                    }
                }
            }
            return stringBuilder.ToString();
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///   <para>Clear all pointers and deselect any selected objects in the EventSystem.</para>
        /// </summary>
        protected void ClearSelection()
        {
            BaseEventData baseEventData = this.GetBaseEventData();
            using (Dictionary<int, PointerEventData>.ValueCollection.Enumerator enumerator = this.m_PointerData.Values.GetEnumerator())
            {
                while (enumerator.MoveNext())
                    this.HandlePointerExitAndEnter(enumerator.Current, (GameObject)null);
            }
            this.m_PointerData.Clear();
            this.eventSystem.SetSelectedGameObject((GameObject)null, baseEventData);
        }

        /// <summary>
        ///   <para>Deselect the current selected GameObject if the currently pointed-at GameObject is different.</para>
        /// </summary>
        /// <param name="currentOverGo">The GameObject the pointer is currently over.</param>
        /// <param name="pointerEvent">Current event data.</param>
        protected void DeselectIfSelectionChanged(GameObject currentOverGo, BaseEventData pointerEvent)
        {
            if (!((Object)ExecuteEvents.GetEventHandler<ISelectHandler>(currentOverGo) != (Object)this.eventSystem.currentSelectedGameObject))
                return;
            this.eventSystem.SetSelectedGameObject((GameObject)null, pointerEvent);
        }

        protected bool GetPointerData(int id, out PointerEventData data, bool create)
        {
            if (this.m_PointerData.TryGetValue(id, out data) || !create)
                return false;
            data = new PointerEventData(this.eventSystem)
            {
                pointerId = id
            };
            this.m_PointerData.Add(id, data);
            return true;
        }

        /// <summary>
        /// Gets the horizontal axis to move cursor
        /// </summary>
        /// <returns>The horizontal axis.</returns>
        protected virtual float GetHorizontalAxis()
        {
            return InterfaceManager.Input.GetAxis(horizontalAxis);
        }

        /// <summary>
        /// Gets the vertical axis to move cursor
        /// </summary>
        /// <returns>The horizontal axis.</returns>
        protected virtual float GetVerticalAxis()
        {
            return InterfaceManager.Input.GetAxis(verticalAxis);
        }

        /// <summary>
        /// Gets the name of the axis for scroll
        /// </summary>
        /// <returns>The scroll axis.</returns>
        protected virtual float GetScrollAxis()
        {
            return InterfaceManager.Input.GetAxis(scrollAxis);
        }

        /// <summary>
        /// Gets the button down for click
        /// </summary>
        /// <returns>True or false.</returns>
        protected virtual bool GetClickButtonDown()
        {
            return InterfaceManager.Input.GetButtonDown(clickButton);
        }

        /// <summary>
        /// Gets the button up for click
        /// </summary>
        /// <returns>True or false.</returns>
        protected virtual bool GetClickButtonUp()
        {
            return InterfaceManager.Input.GetButtonUp(clickButton);
        }

        /// <summary>
        ///   <para>Remove the PointerEventData from the cache.</para>
        /// </summary>
        /// <param name="data"></param>
        protected void RemovePointerData(PointerEventData data)
        {
            this.m_PointerData.Remove(data.pointerId);
        }

        protected PointerEventData GetTouchPointerEventData(Touch input, out bool pressed, out bool released)
        {
            PointerEventData data;
            bool pointerData = this.GetPointerData(input.fingerId, out data, true);
            data.Reset();
            pressed = pointerData || input.phase == TouchPhase.Began;
            released = input.phase == TouchPhase.Canceled || input.phase == TouchPhase.Ended;
            if (pointerData)
            {
                data.position = input.position;
            }
            data.delta = !pressed ? input.position - data.position : Vector2.zero;
            data.position = input.position;
            data.button = PointerEventData.InputButton.Left;
            this.eventSystem.RaycastAll(data, this.m_RaycastResultCache);
            RaycastResult firstRaycast = BaseInputModule.FindFirstRaycast(this.m_RaycastResultCache);
            data.pointerCurrentRaycast = firstRaycast;
            m_CursorPosition = data.position;
            this.m_RaycastResultCache.Clear();
            return data;
        }

        /// <summary>
        ///   <para>Copy one PointerEventData to another.</para>
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        protected void CopyFromTo(PointerEventData from, PointerEventData to)
        {
            to.position = from.position;
            to.delta = from.delta;
            to.scrollDelta = from.scrollDelta;
            to.pointerCurrentRaycast = from.pointerCurrentRaycast;
            to.pointerEnter = from.pointerEnter;
        }

        /// <summary>
        ///   <para>Given a mouse button return the current state for the frame.</para>
        /// </summary>
        /// <param name="buttonId">Mouse Button id.</param>
        protected static PointerEventData.FramePressState StateForMouseButton(int buttonId, UICursorInputModule inputModule)
        {
            bool mouseButtonDown = false;
            bool mouseButtonUp = false;

            if (!inputModule.eventSystem.sendNavigationEvents)
            {
                mouseButtonDown = inputModule.GetClickButtonDown();
                mouseButtonUp = inputModule.GetClickButtonUp();
            }
            else
            {
                if (inputModule.m_previousSendNavigationEvents == false)
                {
                    mouseButtonUp = true;
                }
            }

            inputModule.m_previousSendNavigationEvents = inputModule.eventSystem.sendNavigationEvents;

            if (Input.mousePresent)
            {
                if (!mouseButtonDown || buttonId > 0)
                {
                    mouseButtonDown = Input.GetMouseButtonDown(buttonId);
                }

                if (!mouseButtonUp || buttonId > 0)
                {
                    mouseButtonUp = Input.GetMouseButtonUp(buttonId);
                }
            }

            if (mouseButtonDown && mouseButtonUp)
                return PointerEventData.FramePressState.PressedAndReleased;
            if (mouseButtonDown)
            {
                if (buttonId == 0)
                {
                    inputModule.OnClickPress();
                }
                return PointerEventData.FramePressState.Pressed;
            }
            return mouseButtonUp ? PointerEventData.FramePressState.Released : PointerEventData.FramePressState.NotChanged;
        }

        /// <summary>
        ///   <para>Return the current MouseState.</para>
        /// </summary>
        /// <param name="id"></param>
        protected virtual MouseState GetMousePointerEventData()
        {
            return this.GetMousePointerEventData(0);
        }

        /// <summary>
        /// Raises the click event.
        /// </summary>
        protected abstract void OnClickPress();

        /// <summary>
        ///   <para>Return the current MouseState.</para>
        /// </summary>
        /// <param name="id"></param>
        protected virtual MouseState GetMousePointerEventData(int id)
        {
            if (!eventSystem.sendNavigationEvents)
            {
                m_inputX = GetHorizontalAxis();
                m_inputY = GetVerticalAxis();
            }

            m_Scroll.y = GetScrollAxis();

            if (m_inputX == 0 && m_inputY == 0 && input.mousePosition != m_PreviousMousePosition)
            {
                m_CursorPosition = input.mousePosition;
            }
            m_PreviousMousePosition = input.mousePosition;

            SetCursorBounds();

            PointerEventData data1;
            bool pointerData = this.GetPointerData(-1, out data1, true);
            data1.Reset();

            Vector2 mousePosition = m_CursorPosition;
            //Vector2 mousePosition = (Vector2) Input.mousePosition;

            if (pointerData)
            {
                data1.position = mousePosition;
                //data1.position = (Vector2)Input.mousePosition;
            }

            data1.delta = mousePosition - data1.position;
            data1.position = mousePosition;

            data1.scrollDelta = Input.mouseScrollDelta;
            if (!Mathf.Approximately(m_Scroll.sqrMagnitude, 0.0f))
            {
                m_Scroll.y *= m_scrollSpeed;
                data1.scrollDelta = m_Scroll;
            }

            data1.button = PointerEventData.InputButton.Left;
            eventSystem.RaycastAll(data1, this.m_RaycastResultCache);
            RaycastResult firstRaycast = FindFirstRaycast(m_RaycastResultCache);
            data1.pointerCurrentRaycast = firstRaycast;
            m_RaycastResultCache.Clear();
            PointerEventData data2;
            GetPointerData(-2, out data2, true);
            CopyFromTo(data1, data2);
            data2.button = PointerEventData.InputButton.Right;
            PointerEventData data3;
            GetPointerData(-3, out data3, true);
            CopyFromTo(data1, data3);
            data3.button = PointerEventData.InputButton.Middle;
            m_MouseState.SetButtonState(PointerEventData.InputButton.Left, StateForMouseButton(0, this), data1);
            m_MouseState.SetButtonState(PointerEventData.InputButton.Right, StateForMouseButton(1, this), data2);
            m_MouseState.SetButtonState(PointerEventData.InputButton.Middle, StateForMouseButton(2, this), data3);
            return m_MouseState;
        }

        protected void SetCursorBounds()
        {
            if (m_CursorPosition.x < 0)
            {
                m_CursorPosition.x = 0;
            }

            if (m_CursorPosition.y < 0)
            {
                m_CursorPosition.y = 0;
            }

            if (m_CursorPosition.x > Screen.width)
            {
                m_CursorPosition.x = Screen.width;
            }

            if (m_CursorPosition.y > Screen.height)
            {
                m_CursorPosition.y = Screen.height;
            }
        }

        /// <summary>
        ///   <para>Return the last PointerEventData for the given touch / mouse id.</para>
        /// </summary>
        /// <param name="id"></param>
        protected PointerEventData GetLastPointerEventData(int id)
        {
            PointerEventData data;
            this.GetPointerData(id, out data, false);
            return data;
        }

        private static bool ShouldStartDrag(Vector2 pressPos, Vector2 currentPos, float threshold, bool useDragThreshold)
        {
            if (!useDragThreshold)
                return true;
            return (double)(pressPos - currentPos).sqrMagnitude >= (double)threshold * (double)threshold;
        }

        /// <summary>
        ///   <para>Process movement for the current frame with the given pointer event.</para>
        /// </summary>
        /// <param name="pointerEvent"></param>
        protected virtual void ProcessMove(PointerEventData pointerEvent)
        {
            GameObject gameObject = pointerEvent.pointerCurrentRaycast.gameObject;
            this.HandlePointerExitAndEnter(pointerEvent, gameObject);
        }

        /// <summary>
        ///   <para>Process the drag for the current frame with the given pointer event.</para>
        /// </summary>
        /// <param name="pointerEvent"></param>
        protected virtual void ProcessDrag(PointerEventData pointerEvent)
        {
            bool flag = pointerEvent.IsPointerMoving();
            if (flag && (Object)pointerEvent.pointerDrag != (Object)null && (!pointerEvent.dragging && UICursorInputModule.ShouldStartDrag(pointerEvent.pressPosition, pointerEvent.position, (float)eventSystem.pixelDragThreshold, pointerEvent.useDragThreshold)))
            {
                ExecuteEvents.Execute<IBeginDragHandler>(pointerEvent.pointerDrag, (BaseEventData)pointerEvent, ExecuteEvents.beginDragHandler);
                pointerEvent.dragging = true;
            }
            if (!pointerEvent.dragging || !flag || !((Object)pointerEvent.pointerDrag != (Object)null))
                return;
            if ((Object)pointerEvent.pointerPress != (Object)pointerEvent.pointerDrag)
            {
                ExecuteEvents.Execute<IPointerUpHandler>(pointerEvent.pointerPress, (BaseEventData)pointerEvent, ExecuteEvents.pointerUpHandler);
                pointerEvent.eligibleForClick = false;
                pointerEvent.pointerPress = (GameObject)null;
                pointerEvent.rawPointerPress = (GameObject)null;
            }
            ExecuteEvents.Execute<IDragHandler>(pointerEvent.pointerDrag, (BaseEventData)pointerEvent, ExecuteEvents.dragHandler);
        }

        #endregion

    }
}