using UnityEditor;

namespace NullSave.GDTK
{
    [CustomEditor(typeof(InteractableRPGObject))]
    [CanEditMultipleObjects]
    public class InteractableRPGObjectEditor : GDTKEditor
    {

        #region Unity Methods

        public override void OnInspectorGUI()
        {
            MainContainerBegin();

            SectionHeader("Behavior", GetIcon("icons/behavior"));
            SimpleProperty("interactable");
            SimpleProperty("customUI");

            MainContainerEnd();
        }

        #endregion

    }
}