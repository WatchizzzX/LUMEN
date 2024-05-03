using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityToolbarExtender;

namespace Editor
{
    [InitializeOnLoad]
    public class LevelSelectorEditor
    {
        private static List<SceneInfo> _scenes;
        private static SceneInfo _sceneOpened;
        private static int _selectedIndex;
        private static string[] _displayedOptions;

        static LevelSelectorEditor()
        {
            LoadFromPlayerPrefs();
            ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
            EditorSceneManager.sceneOpened += HandleSceneOpened;
        }

        private static void OnToolbarGUI()
        {
            GUILayout.FlexibleSpace();

            _selectedIndex = EditorGUILayout.Popup(_selectedIndex, _displayedOptions);

            GUI.enabled = _selectedIndex == 0;
            if (GUILayout.Button("+"))
            {
                AddScene(_sceneOpened);
            }

            GUI.enabled = _selectedIndex > 0;
            if (GUILayout.Button("-"))
                RemoveScene(_sceneOpened);

            GUI.enabled = true;
            if (GUI.changed && _selectedIndex > 0 && _scenes.Count > _selectedIndex - 1)
                EditorSceneManager.OpenScene(_scenes[_selectedIndex - 1].path);
        }

        private static void RefreshDisplayedOptions()
        {
            _displayedOptions = new string[_scenes.Count + 1];
            _displayedOptions[0] = "Click on '+' to add current scene";

            for (var i = 0; i < _scenes.Count; i++)
                _displayedOptions[i + 1] = _scenes[i].name;
        }

        private static void HandleSceneOpened(Scene scene, OpenSceneMode mode) => SetOpenedScene(scene);

        private static void SetOpenedScene(SceneInfo scene)
        {
            if (scene == null || string.IsNullOrEmpty(scene.path))
                return;

            for (var i = 0; i < _scenes.Count; i++)
            {
                if (_scenes[i].path != scene.path) continue;

                _sceneOpened = _scenes[i];
                _selectedIndex = i + 1;
                SaveToPlayerPrefs(true);
                return;
            }

            _sceneOpened = scene;
            _selectedIndex = 0;
            SaveToPlayerPrefs(true);
        }

        private static void SetOpenedScene(Scene scene) => SetOpenedScene(new SceneInfo(scene));

        private static void AddScene(SceneInfo scene)
        {
            if (scene == null)
                return;

            if (_scenes.Any(s => s.path == scene.path))
                RemoveScene(scene);

            _scenes.Add(scene);
            _selectedIndex = _scenes.Count;
            SetOpenedScene(scene);
            RefreshDisplayedOptions();
            SaveToPlayerPrefs();
        }

        private static void RemoveScene(SceneInfo scene)
        {
            _scenes.Remove(scene);
            _selectedIndex = 0;
            RefreshDisplayedOptions();
            SaveToPlayerPrefs();
        }

        private static void SaveToPlayerPrefs(bool onlyLatestOpenedScene = false)
        {
            if (!onlyLatestOpenedScene)
            {
                var serialized = string.Join(";",
                    _scenes.Where(s => !string.IsNullOrEmpty(s.path)).Select(s => s.path));
                SetPref("SceneSelectionToolbar.Scenes", serialized);
            }

            if (_sceneOpened != null)
                SetPref("SceneSelectionToolbar.LatestOpenedScene", _sceneOpened.path);
        }

        private static void LoadFromPlayerPrefs()
        {
            var serialized = GetPref("SceneSelectionToolbar.Scenes");

            _scenes = serialized.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => new SceneInfo(s)).ToList();

            serialized = GetPref("SceneSelectionToolbar.LatestOpenedScene");

            if (!string.IsNullOrEmpty(serialized))
                SetOpenedScene(new SceneInfo(serialized));

            RefreshDisplayedOptions();
        }

        private static void SetPref(string name, string value) =>
            EditorPrefs.SetString($"{Application.productName}_{name}", value);

        private static string GetPref(string name) => EditorPrefs.GetString($"{Application.productName}_{name}");

        [Serializable]
        private class SceneInfo
        {
            public SceneInfo()
            {
            }

            public SceneInfo(Scene scene)
            {
                name = scene.name;
                path = scene.path;
            }

            public SceneInfo(string path)
            {
                name = System.IO.Path.GetFileNameWithoutExtension(path);
                this.path = path;
            }

            public string name;
            public string path;
        }
    }
}