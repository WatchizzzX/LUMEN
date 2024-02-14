using UnityEditor;
using UnityEngine;

namespace NullSave.GDTK
{
    [CustomEditor(typeof(TargetingSystem))]
    [CanEditMultipleObjects]
    public class TargetingSystemEditor : GDTKEditor
    {

        #region Fields

        private TargetingSystem myTarget;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            if (target is TargetingSystem system)
            {
                myTarget = system;
            }
        }

        public override void OnInspectorGUI()
        {
            MainContainerBegin();

            if (Application.isPlaying)
            {
                SectionHeader("Targets");

                SubHeader("Locked: " + myTarget.lockedTargets.Count);
                foreach (LockOnTarget target in myTarget.lockedTargets)
                {
                    EditorGUILayout.ObjectField(target, typeof(TargetingSystem), true);
                }

                SubHeader("Available: " + myTarget.allTargets.Count);
                foreach (LockOnTarget target in myTarget.allTargets)
                {
                    EditorGUILayout.ObjectField(target, typeof(TargetingSystem), true);
                }
            }

            SectionHeader("Behavior");
            SimpleProperty("is2DMode");
            SimpleProperty("lockRadius");
            SimpleProperty("layerMask");
            SimpleProperty("tagMode");
            if (SimpleValue<bool>("tagMode"))
            {
                SimpleList("tagFilter", true);
            }

            SectionHeader("Lock-On");
            SimpleProperty("autoLockOn");
            if (!SimpleValue<bool>("autoLockOn"))
            {
                SimpleProperty("withButton", "Button Lock On");
                if (SimpleValue<bool>("withButton"))
                {
                    SimpleProperty("lockOnButton", "Button");
                }

                SimpleProperty("withKey", "Key Lock On");
                if (SimpleValue<bool>("withKey"))
                {
                    SimpleProperty("lockOnKey", "Key");
                }
            }

            SectionHeader("Remove Lock-On");
            SimpleProperty("removeWithButton", "Button Lock On");
            if (SimpleValue<bool>("removeWithButton"))
            {
                SimpleProperty("removeLockOnButton", "Button");
            }

            SimpleProperty("removeWithKey", "Key Lock On");
            if (SimpleValue<bool>("removeWithKey"))
            {
                SimpleProperty("removeLockOnKey", "Key");
            }

            SectionHeader("Line of Sight");
            SimpleProperty("requireLineOfSight", "Required");
            if (SimpleValue<bool>("requireLineOfSight"))
            {
                SimpleProperty("obstructionLayer");
                SimpleProperty("losOffset", "Offset");
            }

            SectionHeader("Indicators");
            bool org = SimpleValue<bool>("m_showIndicators");
            bool res = EditorGUILayout.Toggle("Show Indicators", org);
            if (res != org)
            {
                myTarget.showIndicators = res;
                EditorUtility.SetDirty(target);
            }
            SimpleProperty("availableTargetPrefab");
            SimpleProperty("lockedTargetPrefab");

            MainContainerEnd();
        }

        #endregion

    }
}