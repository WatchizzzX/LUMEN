using UnityEditor;

namespace NullSave.GDTK
{
    [CustomEditor(typeof(MessageDisplay))]
    public class MessageDisplayEditor : GDTKEditor
    {

        #region Unity Methods

        public override void OnInspectorGUI()
        {
            MainContainerBegin();
            SimpleProperty("messageLabel");
            MainContainerEnd();
        }

        #endregion

    }
}