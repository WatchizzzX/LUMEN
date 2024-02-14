using UnityEditor;
using UnityEngine;

namespace NullSave.GDTK
{
    [CustomEditor(typeof(RPGLockUnlockAction))]
    [CanEditMultipleObjects]
    public class RPGLockUnlockActionEditor : GDTKEditor
    {

        #region Unity Methods

        public override void OnInspectorGUI()
        {
            MainContainerBegin();

            SectionHeader("Behavior", GetIcon("icons/behavior"));
            SimpleProperty("m_locked");
            SimpleProperty("canLock");
            SimpleProperty("unlockText");
            SimpleProperty("lockText");

            GUILayout.Space(10);
            SectionHeader("Audio", GetIcon("icons/audio"));
            SimpleProperty("audioPoolChannel");
            SimpleProperty("lockSound");

            GUILayout.Space(10);
            SectionHeader("Forbid Actions", GetIcon("icons/prevent"));
            SimpleProperty("forbidWhenLocked");
            SimpleProperty("forbidWhenUnlocked");

            GUILayout.Space(10);
            SectionHeader("Events", GetIcon("icons/event"));
            SimpleProperty("onLocked");
            SimpleProperty("onUnlocked");

            MainContainerEnd();
        }

        #endregion

    }
}