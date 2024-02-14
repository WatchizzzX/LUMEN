﻿using UnityEditor;
using UnityEngine;

namespace NullSave.GDTK
{
    [CustomEditor(typeof(DisableAfterAudioPlayed))]
    public class DisablefterAudioPlayedEditor : GDTKEditor
    {

        #region Unity Methods

        public override void OnInspectorGUI()
        {
            MainContainerBegin();

            GUILayout.BeginVertical("box");
            GUILayout.Label("This component will disable the Game Object after the attached Audio Source finishes playing. If the Audio Source does not auto-play the component will also wait for play to start first.", Styles.WrappedTextStyle);
            GUILayout.EndVertical();

            MainContainerEnd();
        }

        #endregion

    }
}