using UnityEditor;
using UnityEngine;

namespace NullSave.GDTK
{
    [CustomEditor(typeof(InterfaceManager))]
    public class InterfaceManagerEditor : GDTKEditor
    {

        #region Unity Methods

        public override void OnInspectorGUI()
        {
            MainContainerBegin();

            if (CollapsingSectionHeader("General", GetIcon("icons/behavior"), "persist"))
            {
                SimpleProperty("persist");
                SimpleProperty("inputManager");
                SimpleProperty("objectManager");
                SimpleProperty("m_localizationSettings");
            }

            if (CollapsingSectionHeader("UI Canvas", GetIcon("icons/ui"), "uiScaleMode"))
            {
                SimpleProperty("uiScaleMode", "Scale Mode");
                switch ((UIScaleMode)SimpleValue<int>("uiScaleMode"))
                {
                    case UIScaleMode.ConstantPhysicalSize:
                        SimpleProperty("physicalUnit");
                        SimpleProperty("fallbackScreenDPI");
                        SimpleProperty("defaultSpriteDPI");
                        SimpleProperty("referencePixelsPerUnit");
                        break;
                    case UIScaleMode.ConstantPixelSize:
                        SimpleProperty("scaleFactor");
                        SimpleProperty("referencePixelsPerUnit");
                        break;
                    case UIScaleMode.ScaleWithScreenSize:
                        SimpleProperty("referenceResolution");
                        SimpleProperty("screenMatchMode");
                        Rect r = EditorGUILayout.GetControlRect(true, EditorGUIUtility.singleLineHeight + 12);
                        DualLabeledSlider(r, serializedObject.FindProperty("matchWidthOrHeight"), new GUIContent("Match"), new GUIContent("Width"), new GUIContent("Height"));
                        SimpleProperty("referencePixelsPerUnit");
                        break;
                }
                SimpleProperty("includeRaycaster");
            }

            if (CollapsingSectionHeader("Interaction", GetIcon("icons/gears"), "interactorPrefab"))
            {
                SimpleProperty("interactorPrefab");
                SimpleProperty("interactionType");
                switch ((NavigationTypeSimple)SimpleValue<int>("interactionType"))
                {
                    case NavigationTypeSimple.ByButton:
                        SimpleProperty("interactionButton");
                        break;
                    case NavigationTypeSimple.ByKey:
                        SimpleProperty("interactionKey");
                        break;
                }
            }

            if (CollapsingSectionHeader("Tooltip", GetIcon("icons/dialogue"), "tooltipPrefab"))
            {
                SimpleProperty("tooltipPrefab");
                SimpleProperty("tipOffset");
                SimpleProperty("displayDelay");
            }

            if (CollapsingSectionHeader("Input Cursor", GetIcon("icons/pointer"), "m_useInputCursor"))
            {
                if (Application.isPlaying)
                {
                    InterfaceManager.ShowInputCursor = EditorGUILayout.Toggle("Use Input Cursor", InterfaceManager.ShowInputCursor);
                }
                else
                {
                    SimpleProperty("m_useInputCursor");
                }
                SimpleProperty("m_inputCursorSprite");
                SimpleProperty("m_inputCursorSize");
                SimpleProperty("inputCursorSensitivity");
                SimpleProperty("m_inputCursorHorizontal");
                SimpleProperty("m_inputCursorVertical");
                SimpleProperty("m_inputCursorSubmit");
                SimpleProperty("m_inputCursorCancel");
                SimpleProperty("m_inputCursorClick");
                SimpleProperty("m_inputCursorScroll");
                SimpleProperty("m_inputCursorStartCentered");
                SimpleProperty("m_inputCursorLockHardwarePointer");
            }

            if (CollapsingSectionHeader("Tab Stops", GetIcon("icons/list"), "activateFirstTabStop"))
            {
                SimpleProperty("activateFirstTabStop");
                SimpleProperty("tabStyle");
                switch ((NavigationTypeSimple)SimpleValue<int>("tabStyle"))
                {
                    case NavigationTypeSimple.ByButton:
                        SimpleProperty("tabButton");
                        break;
                    case NavigationTypeSimple.ByKey:
                        SimpleProperty("tabKey");
                        break;
                }
            }

            MainContainerEnd();
        }

        #endregion

    }
}