using UnityEditor;
using UnityEngine;

namespace NullSave.GDTK
{
    [CustomEditor(typeof(BroadcastMessageList))]
    public class BroadcastMessageListEditor : GDTKEditor
    {

        #region Unity Methods

        public override void OnInspectorGUI()
        {
            MainContainerBegin();

            SectionHeader("Behavior", GetIcon("icons/behavior"));
            SimpleProperty("template");
            SimpleProperty("container");

            if (Application.isPlaying)
            {
                SubHeader("Listening To");
                SerializedProperty list = serializedObject.FindProperty("channels");
                for (int i = 0; i < list.arraySize; i++)
                {
                    GUILayout.Label(list.GetArrayElementAtIndex(i).stringValue);
                }
            }
            else
            {
                SubHeader("Listen To");
                StringList("channels");
            }

            MainContainerEnd();
        }

        #endregion

    }
}