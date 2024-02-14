using UnityEditor;
using UnityEngine;

namespace NullSave.GDTK
{
    [CustomEditor(typeof(RPGTrappedAction))]
    [CanEditMultipleObjects]
    public class RPGTrappedActionEditor : GDTKEditor
    {

        #region Unity Methods

        public override void OnInspectorGUI()
        {
            MainContainerBegin();

            SectionHeader("Behavior", GetIcon("icons/behavior"));
            SimpleProperty("m_trapped");
            SimpleProperty("disarmText");
            SimpleProperty("successChance");

            GUILayout.Space(10);
            SectionHeader("Audio", GetIcon("icons/audio"));
            SimpleProperty("audioPoolChannel");
            SimpleProperty("successSound");
            SimpleProperty("failureSound");

            GUILayout.Space(10);
            SectionHeader("Events", GetIcon("icons/event"));
            SimpleProperty("onSuccess");
            SimpleProperty("onFail");
            SimpleProperty("onTriggered");

            MainContainerEnd();
        }

        #endregion

    }
}
