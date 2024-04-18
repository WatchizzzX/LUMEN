using UnityEditor;
using UnityEngine;

namespace Editor.DefaultSceneLoader
{
    public class DefaultSceneWindow : EditorWindow
    {
        private bool isActive;
        private bool autoSaveOnPlay;
        private int defaultSceneIndex;

        [MenuItem("Utils/Default Scene Loader Settings")]
        public static void ShowWindow()
        {
            EditorWindow window = GetWindow<DefaultSceneWindow>("Default Scene Loader Settings");
            window.minSize = new Vector2(200, 100);
        }

        private void OnLostFocus()
        {
            EditorPrefs.SetBool(DefaultScenePrefs.AutoSave, autoSaveOnPlay);
            EditorPrefs.SetBool(DefaultScenePrefs.IsActive, isActive);
            EditorPrefs.SetInt(DefaultScenePrefs.DefaultSceneIndex, defaultSceneIndex);
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Default Scene Loader Settings", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUILayout.BeginVertical("HelpBox");
            isActive = EditorGUILayout.Toggle("Scene Loader Active", isActive);
            autoSaveOnPlay = EditorGUILayout.Toggle("Auto Save On Play", autoSaveOnPlay);
            defaultSceneIndex = EditorGUILayout.IntField("Default Scene Index", defaultSceneIndex);
            EditorGUILayout.EndVertical();
        }
    }
}