using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace NullSave.GDTK
{
    [CustomEditor(typeof(RPGOpenCloseAction))]
    [CanEditMultipleObjects]
    public class RPGOpenCloseActionEditor : GDTKEditor
    {

        #region Fields

        ReorderableList openAnims;
        ReorderableList closeAnims;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            openAnims = new ReorderableList(serializedObject, serializedObject.FindProperty("openAnimModifiers"), true, true, true, true);
            openAnims.elementHeight = (EditorGUIUtility.singleLineHeight + 2) * 3;
            openAnims.drawHeaderCallback = (Rect rect) => { EditorGUI.LabelField(rect, "Open Animator Modifiers"); };
            openAnims.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = openAnims.serializedProperty.GetArrayElementAtIndex(index);
                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, rect.height), element, new GUIContent(""));
            };

            closeAnims = new ReorderableList(serializedObject, serializedObject.FindProperty("closeAnimModifiers"), true, true, true, true);
            closeAnims.elementHeight = (EditorGUIUtility.singleLineHeight + 2) * 3;
            closeAnims.drawHeaderCallback = (Rect rect) => { EditorGUI.LabelField(rect, "Close Animator Modifiers"); };
            closeAnims.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = closeAnims.serializedProperty.GetArrayElementAtIndex(index);
                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, rect.height), element, new GUIContent(""));
            };
        }

        public override void OnInspectorGUI()
        {
            MainContainerBegin();

            SectionHeader("Behavior", GetIcon("icons/behavior"));
            SimpleProperty("m_opened");
            SimpleProperty("openText");
            SimpleProperty("closeText");
            SimpleProperty("openCloseDuration");

            GUILayout.Space(10);
            SectionHeader("Animator Modifiers", GetIcon("icons/animate"));
            openAnims.DoLayoutList();
            closeAnims.DoLayoutList();

            GUILayout.Space(10);
            SectionHeader("Audio", GetIcon("icons/audio"));
            SimpleProperty("audioPoolChannel");
            SimpleProperty("openSound");
            SimpleProperty("closeSound");

            GUILayout.Space(10);
            SectionHeader("Forbid Actions", GetIcon("icons/prevent"));
            SimpleProperty("forbidWhenOpen");
            SimpleProperty("forbidWhenClosed");

            GUILayout.Space(10);
            SectionHeader("Events", GetIcon("icons/event"));
            SimpleProperty("onOpen");
            SimpleProperty("onClose");

            MainContainerEnd();
        }

        #endregion

    }
}