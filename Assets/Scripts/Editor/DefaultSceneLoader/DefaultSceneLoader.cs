using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Editor.DefaultSceneLoader
{
    [InitializeOnLoad]
    public static class DefaultSceneLoader
    {
        static DefaultSceneLoader()
        {
            EditorApplication.playModeStateChanged += ToggleScenes;
        }

        private static void ToggleScenes(PlayModeStateChange state)
        {
            if (!EditorPrefs.GetBool(DefaultScenePrefs.IsActive)) return;
            var defaultScene =
                SceneUtility.GetScenePathByBuildIndex(EditorPrefs.GetInt(DefaultScenePrefs.DefaultSceneIndex));

            var currentScenePath = SceneManager.GetActiveScene().path;

            switch (state)
            {
                case PlayModeStateChange.ExitingEditMode:
                {
                    EditorPrefs.SetString(DefaultScenePrefs.ReturnPath, currentScenePath);

                    if (currentScenePath != defaultScene)
                    {
                        if (EditorPrefs.GetBool(DefaultScenePrefs.AutoSave))
                            EditorSceneManager.SaveOpenScenes();
                        else
                            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

                        EditorSceneManager.OpenScene(defaultScene);
                    }

                    break;
                }
                case PlayModeStateChange.EnteredEditMode:
                {
                    var returnPath = EditorPrefs.GetString(DefaultScenePrefs.ReturnPath);

                    if (currentScenePath != returnPath)
                        EditorSceneManager.OpenScene(returnPath);
                    break;
                }
            }
        }
    }
}