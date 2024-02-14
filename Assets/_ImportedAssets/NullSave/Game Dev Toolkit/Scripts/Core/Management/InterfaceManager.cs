//
// Game Developers Toolkit © 2023 by Thomas W Holtquist is licensed under CC BY-SA 4.0 
// https://www.nullsave.com
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NullSave.GDTK
{
    [AutoDoc("The Interface Manager provide a centralized location for dealing with Input, Object Management, Interaction, and UI. This object will automatically create itself will default settings whenever data is requested from it if there is not already an instance in your scene. A secondary canvas is also created specifically for system UI components such as Tooltips.")]
    [AutoDocYouTube("ueXf6JprAyA")]
    [DefaultExecutionOrder(-999)]
    public class InterfaceManager : MonoBehaviour
    {

        #region Fields

        [Tooltip("Mark this object as DontDestroyOnLoad")] public bool persist;

        // Input
        [Tooltip("Input manager to use")] public InputManager inputManager;

        // Object
        [Tooltip("Object manager to use")] public ObjectManager objectManager;

        [Tooltip("Localization settings to use")][SerializeField] private LocalizationSettings m_localizationSettings;

        // Basic UI
        [Tooltip("Use the a canvas already in the scene")] public bool useExistingCanvas;
        [Tooltip("Canvas to use for UI")] public Canvas useCanvas;

        [Tooltip("Determines how UI elements in the Canvas are scaled.")]
        [SerializeField] private UIScaleMode uiScaleMode;

        [Tooltip("If a sprite has this 'Pixels Per Unit' setting, then one pixel in the sprite will cover one unit in the UI.")]
        [SerializeField] protected float referencePixelsPerUnit;

        [Tooltip("Scales all UI elements in the Canvas by this factor.")]
        [SerializeField] protected float scaleFactor;

        [Tooltip("The resolution the UI layout is designed for. If the screen resolution is larger, the UI will be scaled up, and if it's smaller, the UI will be scaled down. This is done in accordance with the Screen Match Mode.")]
        [SerializeField] protected Vector2 referenceResolution;

        [Tooltip("A mode used to scale the canvas area if the aspect ratio of the current resolution doesn't fit the reference resolution.")]
        [SerializeField] protected UIScreenMatchMode screenMatchMode;

        [Tooltip("Determines if the scaling is using the width or height as reference, or a mix in between.")]
        [Range(0, 1)]
        [SerializeField] protected float matchWidthOrHeight;

        [Tooltip("The physical unit to specify positions and sizes in.")]
        [SerializeField] protected UIUnit physicalUnit;

        [Tooltip("The DPI to assume if the screen DPI is not known.")]
        [SerializeField] protected float fallbackScreenDPI;

        [Tooltip("The pixels per inch to use for sprites that have a 'Pixels Per Unit' setting that matches the 'Reference Pixels Per Unit' setting.")]
        [SerializeField] protected float defaultSpriteDPI;

        [Tooltip("The amount of pixels per unit to use for dynamically created bitmaps in the UI, such as Text.")]
        [SerializeField] protected float dynamicPixelsPerUnit;

        [Tooltip("Add a Raycaster component to the canvas.")]
        public bool includeRaycaster;

        // Tooltip
        [Tooltip("Prefab to use when displaying a tooltip.")] public TooltipDisplay tooltipPrefab;
        [Tooltip("Pixel offset from cursor to use when displaying tooltip.")] public Vector2 tipOffset;
        [Tooltip("Seconds to wait after cursor is over item to display tooltip")] public float displayDelay;

        private TooltipDisplay tooltip;
        private TooltipDisplay customTooltip;

        private Vector2 lastPos;
        private TooltipClient lastTip;
        private bool suppressUI;

        // Interaction
        [Tooltip("Interaction UI prefab to use by default")] public InteractorUI interactorPrefab;
        [Tooltip("Method of confirming interaction")] public NavigationTypeSimple interactionType;
        [Tooltip("Button to use for confirming interaction")] public string interactionButton;
        [Tooltip("Key to use for confirming interaction")] public KeyCode interactionKey;

        // Input Cursor
        [Tooltip("Use Input Cursor (responds to mouse and hardward input such as a controller)")][SerializeField] private bool m_useInputCursor;
        [Tooltip("Sprite to use for cursor")][SerializeField] private Sprite m_inputCursorSprite;
        [Tooltip("Size of the cursor")][SerializeField] private Vector2 m_inputCursorSize;
        [Tooltip("Sensitivity of the cursor")][Range(0.5f, 15f)] public float inputCursorSensitivity;
        [Tooltip("Horizontal input name")][SerializeField] private string m_inputCursorHorizontal;
        [Tooltip("Vertical input name")][SerializeField] private string m_inputCursorVertical;
        [Tooltip("Submit input name")][SerializeField] private string m_inputCursorSubmit;
        [Tooltip("Cancel input name")][SerializeField] private string m_inputCursorCancel;
        [Tooltip("Click input name")][SerializeField] private string m_inputCursorClick;
        [Tooltip("Scroll input name")][SerializeField] private string m_inputCursorScroll;
        [Tooltip("Start with the cursor in the center of the screen")][SerializeField] private bool m_inputCursorStartCentered;
        [Tooltip("Lock the hardware cursor")][SerializeField] private bool m_inputCursorLockHardwarePointer;

        // Tabstop
        [Tooltip("Automatically activate the first TabStop")] public bool activateFirstTabStop;
        [Tooltip("Method of performing tab stops")] public NavigationTypeSimple tabStyle;
        [Tooltip("Button to use for tabbing")] public string tabButton;
        [Tooltip("Key to use for tabbing")] public KeyCode tabKey;

        private static InterfaceManager current;

        private Canvas uiCanvas;
        private CanvasScaler canvasScaler;

        private List<GameObject> activeModals;
        private GameObject currentModal;

        private UICursorStandaloneInputModule inputModule;
        private UICursor m_inputCursor;
        private bool m_hideInputCursor;

        // Thread Syncing
        protected List<object> syncRequests;

        #endregion

        #region Properties

        [AutoDoc("Gets the currently active modal")]
        public static GameObject ActiveModal { get => Current.currentModal; }

        [AutoDoc("Returns the current instance")]
        public static InterfaceManager Current
        {
            get
            {
                if (Application.isPlaying && current == null)
                {
                    // Check if something called us before we awoke.
                    current = ToolRegistry.GetComponent<InterfaceManager>();
                    if (current != null) return current;

                    GameObject go = new GameObject("GDTK Interface Manager");
                    current = go.AddComponent<InterfaceManager>();
                    current.Reset();
                }

                return current;
            }
        }

        [AutoDoc("Returns the current Input Manager")]
        public static InputManager Input { get { return Current.inputManager; } }

        [AutoDoc("Get/Set Interface Flags")]
        public InterfaceStateFlags InterfaceFlags { get; set; }

        [AutoDoc("Returns current Localization Settings")]
        public static LocalizationSettings localizationSettings
        {
            get
            {
                return Current?.m_localizationSettings;
            }
        }

        [AutoDoc("Returns if Interface Flags contains LockPlayerController")]
        public static bool LockPlayerController
        {
            get { return Current.InterfaceFlags.HasFlag(InterfaceStateFlags.LockPlayerController); }
            set
            {
                if (value)
                {
                    Current.InterfaceFlags |= InterfaceStateFlags.LockPlayerController;
                }
                else
                {
                    Current.InterfaceFlags &= InterfaceStateFlags.LockPlayerController;

                }
            }
        }

        [AutoDoc("Returns current Object Manager")]
        public static ObjectManager ObjectManagement { get { return Current.objectManager; } }

        [AutoDoc("Returns if Interface Flags contains PreventInteractionUI")]
        public static bool PreventInteractions
        {
            get { return Current.InterfaceFlags.HasFlag(InterfaceStateFlags.PreventInteractionUI); }
            set
            {
                if (value)
                {
                    Current.InterfaceFlags |= InterfaceStateFlags.PreventInteractionUI;
                }
                else
                {
                    Current.InterfaceFlags &= InterfaceStateFlags.PreventInteractionUI;

                }
            }
        }

        [AutoDoc("Returns if Interface Flags contains PreventPrompts")]
        public static bool PreventPrompts { get { return Current.InterfaceFlags.HasFlag(InterfaceStateFlags.PreventPrompts); } }

        [AutoDoc("Returns if Interface Flags contains PreventWindows")]
        public static bool PreventWindows { get { return Current.InterfaceFlags.HasFlag(InterfaceStateFlags.PreventWindows); } }

        [AutoDoc("Returns if a prompt is open")]
        public static bool PromptOpen { get; set; }

        [AutoDoc("Gets/Sets if Input Cursor should be shown")]
        public static bool ShowInputCursor
        {
            get { return !Current.m_hideInputCursor; }
            set
            {
                Current.m_hideInputCursor = !value;

                if (Current.m_inputCursor != null)
                {
                    Current.m_inputCursor.gameObject.SetActive(value);
                    Current.inputModule.enabled = value;
                }
                else
                {
                    Current.CreateInputUI();
                }
            }
        }

        [AutoDoc("Gets/Sets UI suppression state")]
        public static bool SuppressUI
        {
            get { return Current.suppressUI; }
            set
            {
                if (Current.suppressUI == value) return;
                Current.suppressUI = value;
                UICanvas.gameObject.SetActive(!value);
            }
        }

        [AutoDoc("Returns current UI Canvas")]
        public static Canvas UICanvas
        {
            get
            {
                if (Current.uiCanvas == null)
                {
                    if (Current.useExistingCanvas)
                    {
                        Current.uiCanvas = current.useCanvas;
                    }
                    else
                    {
                        Current.CreateUICanvas();
                    }
                }
                return Current.uiCanvas;
            }
        }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            m_hideInputCursor = !m_useInputCursor;

            if (persist)
            {
                if (current != null && current != this)
                {
                    Destroy(gameObject);
                    return;
                }

                if (Application.isPlaying)
                {
                    transform.SetParent(null);
                    SceneManager.sceneLoaded += SceneManager_sceneLoaded;
                    SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
                    DontDestroyOnLoad(this);
                }
            }
            else
            {
                if (current != null && current != this)
                {
                    Destroy(gameObject);
                    return;
                }
            }

            syncRequests = new List<object>();
            ToolRegistry.RegisterComponent(this);
            current = this;
        }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            ResetObjects();
        }

        private void OnDestroy()
        {
            if (uiCanvas != null)
            {
                Destroy(uiCanvas.gameObject);
            }
            ToolRegistry.RemoveComponent(this);
        }

        private void Reset()
        {
            inputManager = Resources.Load<InputManager>("Input Managers/Unity Input Manager");
            objectManager = Resources.Load<ObjectManager>("Object Managers/Unity Object Manager");
            m_localizationSettings = Resources.Load<LocalizationSettings>("Localization/Default Localization");
            uiScaleMode = UIScaleMode.ScaleWithScreenSize;
            referenceResolution = new Vector2(1920, 1080);
            referencePixelsPerUnit = 100;
            scaleFactor = 1;
            screenMatchMode = UIScreenMatchMode.MatchWidthOrHeight;
            physicalUnit = UIUnit.Points;
            fallbackScreenDPI = 96;
            defaultSpriteDPI = 96;
            dynamicPixelsPerUnit = 1;

            interactorPrefab = Resources.Load<InteractorUI>("Interaction/Interactor UI");

            tooltipPrefab = Resources.Load<TooltipDisplay>("Tooltips/Tooltip_WhiteOnBlack");
            tipOffset = new Vector2(16, -40);

            includeRaycaster = true;
            activateFirstTabStop = true;

            m_inputCursorSize = new Vector2(32, 32);
            inputCursorSensitivity = 5;
            m_inputCursorHorizontal = "Horizontal";
            m_inputCursorVertical = "Vertical";
            m_inputCursorSubmit = "Submit";
            m_inputCursorCancel = "Cancel";
            m_inputCursorStartCentered = true;
            m_inputCursorLockHardwarePointer = true;
        }

        private void SceneManager_sceneUnloaded(Scene arg0)
        {
            // Destroy existing canvas reference so we can create a new one.
            if (!persist && UICanvas != null && UICanvas.gameObject != null)
            {
                Destroy(UICanvas.gameObject);
                uiCanvas = null;
            }
        }

        private void Start()
        {
            ResetObjects();
        }

        private void Update()
        {
            try
            {
                if (syncRequests == null)
                {
                    syncRequests = new List<object>();
                }
                foreach (object request in syncRequests.ToArray())
                {
                    PeformRequest(request, false);
                    syncRequests.Remove(request);
                }
            }
            catch (Exception e)
            {
                StringExtensions.LogWarning(gameObject, "SyncUpdate", e.Message);
            }

            if (EventSystem.current == null || EventSystem.current.currentInputModule == null) return;

            if (tabStyle == NavigationTypeSimple.ByButton && inputManager.GetButtonDown(tabButton))
            {
                NextTab();
            }
            else if (tabStyle == NavigationTypeSimple.ByKey && inputManager.GetKeyDown(tabKey))
            {
                NextTab();
            }

            if (tooltip != null)
            {
                Vector2 position = Vector2.zero;
#if ENABLE_INPUT_SYSTEM
                if (EventSystem.current.currentInputModule is UICursorInputModule uicim)
                {
                    position = uicim.PointerPosition;
                }
                else
                {
                    position = UnityEngine.InputSystem.Mouse.current.position.ReadValue();
                }

#else
                if (EventSystem.current.currentInputModule is UICursorInputModule uicim)
                {
                    position = uicim.PointerPosition;
                }
                else
                {
                    position = EventSystem.current.currentInputModule.input.mousePosition;
                }
#endif
                if (position == lastPos)
                {
                    if (lastTip != null && (lastTip.gameObject == null || !lastTip.gameObject.activeSelf))
                    {
                        HideTooltip();
                        lastPos = new Vector2(-100, -100);
                    }
                    return;
                }

                var eventData = new PointerEventData(EventSystem.current);
                eventData.position = position;

                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventData, results);

                if (results.Count > 0)
                {
                    lastTip = results[0].gameObject.GetComponent<TooltipClient>();
                    if (lastTip != null)
                    {
                        if (lastTip.customTooltip != null && customTooltip == null)
                        {
                            lastTip.onInitCustomTip?.Invoke();
                            customTooltip = Instantiate(lastTip.customTooltip, uiCanvas.transform);
                            customTooltip.gameObject.name = "_TOOLTIP_";
                            customTooltip.gameObject.SetActive(false);
                            customTooltip.gameObject.hideFlags = HideFlags.HideInHierarchy;
                        }

                        if (!TargetActive())
                        {
                            lastPos = position;
                            StartCoroutine(WaitToDisplay(uiCanvas, lastTip.modifyDelay));
                        }
                        else
                        {
                            PositionTooltip(position);
                        }
                    }
                }
                else
                {
                    if (lastTip != null) lastTip.onHide?.Invoke();
                    lastTip = null;
                }

                if (lastTip == null)
                {
                    HideTooltip();
                }
                lastPos = position;
            }
        }

        #endregion

        #region Public Methods

        [AutoDoc("Hide any currently active tooltip")]
        public static void HideTooltip()
        {
            if (current == null) return;
            current.lastTip = null;
            current.tooltip.gameObject.SetActive(false);
            if (current.customTooltip != null)
            {
                Destroy(current.customTooltip.gameObject);
                current.customTooltip = null;
            }
        }

        [AutoDoc("Check if a GameObject is blocked by a modal")]
        [AutoDocParameter("GameObject to check")]
        public static bool IsBlockedByModal(GameObject obj)
        {
            if (current == null) return false;
            GameObject curModal = current.currentModal;

            if (curModal == null) return false;
            if (curModal == obj || obj.IsChildOf(curModal)) return false;

            return true;
        }

        /// <summary>
        /// Loads a scene
        /// </summary>
        /// <param name="index"></param>
        [AutoDoc("Loads a scene by index")]
        [AutoDocParameter("Index of scene to load")]
        public void LoadScene(int index)
        {
            SceneManager.LoadScene(index);
        }

        /// <summary>
        /// Loads a scene
        /// </summary>
        /// <param name="sceneName"></param>
        [AutoDoc("Loads a scene by name")]
        [AutoDocParameter("Name of scene to load")]
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        [AutoDoc("Set focus to next Tab Stop")]
        public void NextTab()
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                ActivateFirstTab();
                return;
            }

            ITabStop currentStop = null;
            foreach (Component c in EventSystem.current.currentSelectedGameObject.GetComponents<Component>())
            {
                if (c is ITabStop stop)
                {
                    currentStop = stop;
                    break;
                }
            }

            if (currentStop == null)
            {
                ActivateFirstTab();
                return;
            }

            List<ITabStop> stops = ToolRegistry.GetComponents<ITabStop>().Where(x => x != currentStop && x.parentStopId == currentStop.parentStopId && x.tabStopId >= currentStop.tabStopId).OrderBy(x => x.tabStopId).ToList();
            if (stops.Count > 0)
            {
                ActivateStop(stops[0]);
            }
            else
            {
                stops = ToolRegistry.GetComponents<ITabStop>().Where(x => x != currentStop && x.parentStopId == currentStop.parentStopId && x.tabStopId <= currentStop.tabStopId).OrderBy(x => x.tabStopId).ToList();
                if (stops.Count > 0)
                {
                    ActivateStop(stops[0]);
                }
            }
        }

        [AutoDoc("Remove object from list of active modals")]
        [AutoDocParameter("GameObject to remove")]
        public static void RemoveActiveModal(GameObject obj)
        {
            if (current.activeModals == null) current.activeModals = new List<GameObject>();
            if (!current.activeModals.Contains(obj)) return;

            current.activeModals.Remove(obj);

            if (current.currentModal == obj)
            {
                current.currentModal = current.activeModals.Count == 0 ? null : current.activeModals[current.activeModals.Count - 1];
            }
        }

        [AutoDoc("Sets a GameObject as an active modal")]
        [AutoDocParameter("GameObject to set")]
        public static void SetActiveModal(GameObject obj)
        {
            if (current.activeModals == null) current.activeModals = new List<GameObject>();
            if (!current.activeModals.Contains(obj))
            {
                current.activeModals.Add(obj);
            }
            current.currentModal = obj;
        }

        [AutoDoc("Sync a request to the main GUI thread")]
        [AutoDocParameter("Request to sync")]
        public virtual void SyncRequest(object request)
        {
            if (request == null) return;

            if (Thread.CurrentThread.ManagedThreadId == 1)
            {
                PeformRequest(request, true);
            }
            else
            {
                syncRequests.Add(request);
            }
        }

        #endregion

        #region Private Methods

        private void ActivateFirstTab()
        {
            if (EventSystem.current == null) return;

            int lowestId = int.MaxValue;
            ITabStop activate = null;
            foreach (ITabStop tabStop in ToolRegistry.GetComponents<ITabStop>())
            {
                if (tabStop.parentStopId == 0 && tabStop.tabStopId < lowestId)
                {
                    lowestId = tabStop.tabStopId;
                    activate = tabStop;
                }
            }

            ActivateStop(activate);
        }

        private void ActivateStop(ITabStop activate)
        {
            if (activate != null)
            {
                if (activate.attachedObject != null)
                {
                    EventSystem.current.SetSelectedGameObject(activate.attachedObject);
                    activate.attachedObject.GetComponent<Selectable>().Select();
                }
            }
        }

        private void CreateUICanvas()
        {
            GameObject goCanvas = new GameObject("GDTK UI Canvas");

            uiCanvas = goCanvas.AddComponent<Canvas>();
            uiCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            uiCanvas.sortingOrder = 999;

            canvasScaler = goCanvas.AddComponent<CanvasScaler>();
            canvasScaler.defaultSpriteDPI = defaultSpriteDPI;
            canvasScaler.dynamicPixelsPerUnit = dynamicPixelsPerUnit;
            canvasScaler.fallbackScreenDPI = fallbackScreenDPI;
            canvasScaler.matchWidthOrHeight = matchWidthOrHeight;
            canvasScaler.physicalUnit = (CanvasScaler.Unit)physicalUnit;
            canvasScaler.referencePixelsPerUnit = referencePixelsPerUnit;
            canvasScaler.referenceResolution = referenceResolution;
            canvasScaler.scaleFactor = scaleFactor;
            canvasScaler.screenMatchMode = (CanvasScaler.ScreenMatchMode)screenMatchMode;
            canvasScaler.uiScaleMode = (CanvasScaler.ScaleMode)uiScaleMode;

            if (includeRaycaster)
            {
                goCanvas.AddComponent<GraphicRaycaster>();
            }

            if (m_useInputCursor)
            {
                CreateInputUI();
            }

            if (persist)
            {
                DontDestroyOnLoad(goCanvas);
            }
        }

        private void CreateInputUI()
        {
            GameObject goCursor = new GameObject("Input Cursor");
            goCursor.transform.SetParent(uiCanvas.transform);
            Image imgCursor = goCursor.AddComponent<Image>();
            imgCursor.sprite = m_inputCursorSprite;
            imgCursor.raycastTarget = false;
            imgCursor.preserveAspect = true;
            RectTransform rt = goCursor.GetComponent<RectTransform>();
            if (rt == null) rt = goCursor.AddComponent<RectTransform>();
            rt.anchorMin = rt.anchorMax = rt.pivot = new Vector2(0, 1);
            rt.sizeDelta = m_inputCursorSize;
            rt.anchoredPosition = Vector2.zero;
            m_inputCursor = goCursor.AddComponent<UICursor>();

            EventSystem es = EventSystem.current;
            if (es == null)
            {
                GameObject goEventSystem = new GameObject("EventSystem");
                es = goEventSystem.AddComponent<EventSystem>();
            }

            es.sendNavigationEvents = false;
            inputModule = es.gameObject.GetComponent<UICursorStandaloneInputModule>();
            if (inputModule == null) inputModule = es.gameObject.AddComponent<UICursorStandaloneInputModule>();
            inputModule.horizontalAxis = m_inputCursorHorizontal;
            inputModule.verticalAxis = m_inputCursorVertical;
            inputModule.clickButton = m_inputCursorClick;
            inputModule.scrollAxis = m_inputCursorScroll;
            inputModule.submitButton = m_inputCursorSubmit;
            inputModule.cancelButton = m_inputCursorCancel;
            inputModule.startCentered = m_inputCursorStartCentered;
            inputModule.HideHardwarePointer = m_inputCursorLockHardwarePointer;
            inputModule.m_cursor = m_inputCursor;

            foreach (BaseInputModule input in es.gameObject.GetComponents<BaseInputModule>())
            {
                if (input != inputModule)
                {
                    input.enabled = false;
                }
            }
        }

        private void PeformRequest(object request, bool direct)
        {
            if (request != null)
            {
                try
                {
                    if (request is SimpleEvent e)
                    {
                        if (e != null)
                        {
                            e?.Invoke();
                        }
                    }
                    else if (request is Action action)
                    {
                        action?.Invoke();
                    }
                    else if (request is DestroySyncRequest destroyRequest)
                    {
                        ObjectManagement.DestroyObject(destroyRequest.target);
                    }
                    else if (request is ActivationSyncRequest activation)
                    {
                        activation.target.SetActive(activation.active);
                    }
                    else
                    {
                        StringExtensions.LogWarning(gameObject, "SyncUpdate", "Unknown request type: " + request.GetType().Name);
                    }
                }
                catch (Exception e)
                {
                    StringExtensions.LogWarning(gameObject, "SyncUpdate", request.GetType().Name + " :: " + e.Message);
                }
            }
        }

        private void PositionTooltip(Vector2 position)
        {
            RectTransform target = customTooltip == null ? tooltip.RectTransform : customTooltip.RectTransform;
            Vector3 desiredPos = position + tipOffset;
            desiredPos.x = Mathf.Clamp(desiredPos.x, 0, uiCanvas.pixelRect.width - target.sizeDelta.x * uiCanvas.scaleFactor);
            desiredPos.y = Mathf.Clamp(desiredPos.y, target.sizeDelta.y * uiCanvas.scaleFactor, uiCanvas.pixelRect.height);

            target.position = desiredPos;
        }

        private void ResetObjects()
        {
            if (!useExistingCanvas && uiCanvas == null)
            {
                CreateUICanvas();
            }

            if (tooltipPrefab != null && tooltip == null)
            {
                tooltip = Instantiate(tooltipPrefab, uiCanvas.transform);
                tooltip.gameObject.name = "_TOOLTIP_";
                tooltip.gameObject.SetActive(false);
                tooltip.gameObject.hideFlags = HideFlags.HideInHierarchy;
            }

            if (customTooltip != null)
            {
                Destroy(customTooltip.gameObject);
            }

            if (activateFirstTabStop) ActivateFirstTab();

            if (inputManager != null)
            {
                inputManager.Initialize();
            }
        }

        private bool TargetActive()
        {
            if (customTooltip != null) return customTooltip.gameObject.activeSelf;
            return tooltip.gameObject.activeSelf;
        }

        private IEnumerator WaitToDisplay(Canvas canvas, float modDelay)
        {
            bool ignore = false;
            float elapsed = 0;
            while (elapsed < displayDelay + modDelay)
            {
                yield return new WaitForEndOfFrame();
                elapsed += Time.deltaTime;
                if (lastTip == null)
                {
                    ignore = true;
                    break;
                }
            }

            if (!ignore && lastTip != null)
            {
                TooltipDisplay target = customTooltip == null ? tooltip : customTooltip;
                lastTip.onPostInit?.Invoke(target);

                lastTip.onPreDisplay?.Invoke();
                target.ShowTip(lastTip.tipText);

                Vector3 desiredPos = lastPos + tipOffset;
                desiredPos.x = Mathf.Clamp(desiredPos.x, 0, canvas.pixelRect.width - target.RectTransform.sizeDelta.x * canvas.scaleFactor);
                desiredPos.y = Mathf.Clamp(desiredPos.y, target.RectTransform.sizeDelta.y * canvas.scaleFactor, canvas.pixelRect.height);

                target.RectTransform.position = desiredPos;
                target.gameObject.SetActive(true);

                lastTip.onDisplay?.Invoke();
            }
        }

        #endregion

    }
}