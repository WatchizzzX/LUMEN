#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Editor
{
    [InitializeOnLoad]
    public static class DefaultSceneLoader
    {
        static DefaultSceneLoader(){
            EditorApplication.playModeStateChanged += LoadDefaultScene;
        }

        private static void LoadDefaultScene(PlayModeStateChange state)
        {
            if(SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0)) return;
            switch (state)
            {
                case PlayModeStateChange.ExitingEditMode:
                    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo ();
                    break;
                case PlayModeStateChange.EnteredPlayMode:
                    SceneManager.LoadScene (0);
                    break;
            }
        }
    }
}
#endif