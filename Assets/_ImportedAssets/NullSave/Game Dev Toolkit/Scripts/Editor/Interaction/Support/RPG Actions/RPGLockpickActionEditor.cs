using UnityEditor;
using UnityEngine;

namespace NullSave.GDTK
{
    [CustomEditor(typeof(RPGLockpickAction))]
    [CanEditMultipleObjects]
    public class RPGLockpickActionEditor : GDTKEditor
    {

        #region Unity Methods

        public override void OnInspectorGUI()
        {
            MainContainerBegin();

            SectionHeader("Behavior", GetIcon("icons/behavior"));
            SimpleProperty("canLockPick");
            SimpleProperty("lockPickText");
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

            MainContainerEnd();
        }

        #endregion

    }
}