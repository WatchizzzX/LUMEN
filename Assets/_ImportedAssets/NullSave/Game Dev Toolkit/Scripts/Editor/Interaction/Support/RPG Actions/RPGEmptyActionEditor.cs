using UnityEditor;
using UnityEngine;

namespace NullSave.GDTK
{
    [CustomEditor(typeof(RPGEmptyAction))]
    [CanEditMultipleObjects]
    public class RPGEmptyActionEditor : GDTKEditor
    {

        #region Unity Methods

        public override void OnInspectorGUI()
        {
            MainContainerBegin();

            GUILayout.BeginVertical("box");
            GUILayout.Label("This action simply takes up a slot. It can be used to help arrange other actions in the order you wish them to appear.");
            GUILayout.EndVertical();

            MainContainerEnd();
        }

        #endregion

    }
}
