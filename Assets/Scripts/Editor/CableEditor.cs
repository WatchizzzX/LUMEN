using _ImportedAssets.Real_Time_Procedural_Cable_Simple.Scripts;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(BaseCableRenderer), true)]
    public class CableEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!GUILayout.Button("Apply")) return;

            var targetObject = (BaseCableRenderer)serializedObject.targetObject;
            targetObject.RenderCable();
        }
    }
}