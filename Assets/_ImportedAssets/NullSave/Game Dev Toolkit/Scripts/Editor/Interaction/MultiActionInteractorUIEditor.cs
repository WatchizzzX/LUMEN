using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace NullSave.GDTK
{
    [CustomEditor(typeof(MultiActionInteractorUI))]
    public class MultiActionInteractorUIEditor : GDTKEditor
    {

        #region Fields

        ReorderableList mapsList;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            mapsList = new ReorderableList(serializedObject, serializedObject.FindProperty("actions"), true, false, true, true);
            mapsList.elementHeight = EditorGUIUtility.singleLineHeight + 2;
            mapsList.drawHeaderCallback = (Rect rect) => { EditorGUI.LabelField(rect, "Actions"); };
            mapsList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = mapsList.serializedProperty.GetArrayElementAtIndex(index);

                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element);
                rect.y += EditorGUIUtility.singleLineHeight + 2;
            };
        }

        public override void OnInspectorGUI()
        {
            MainContainerBegin();

            SectionHeader("Actions", GetIcon("icons/behavior"));
            mapsList.DoLayoutList();

            MainContainerEnd();
        }

        #endregion

    }
}