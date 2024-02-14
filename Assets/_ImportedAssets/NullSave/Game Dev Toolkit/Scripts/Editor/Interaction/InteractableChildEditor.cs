using UnityEditor;

namespace NullSave.GDTK
{
    [CustomEditor(typeof(InteractableChild))]
    [CanEditMultipleObjects]
    public class InteractableChildEditor : GDTKEditor
    {

        #region Unity Methods

        public override void OnInspectorGUI()
        {
            MainContainerBegin();

            SectionHeader("Behavior");
            SimpleProperty("parentInteractable");

            MainContainerEnd();
        }

        #endregion

    }
}