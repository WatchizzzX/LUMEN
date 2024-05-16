using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers.Settings
{
    [CreateAssetMenu(fileName = "SceneManagerSettings", menuName = "Settings/Create SceneManager settings", order = 0)]
    public class SceneManagerSettings : ScriptableObject
    {
        [Header("Level Settings")]
        [SerializeField, Scene] private string[] nonGameScenes;
        
        public IReadOnlyCollection<Scene> NonGameScenes
        {
            get
            {
                var scenes = new Scene[nonGameScenes.Length];
                for(var i = 0; i < nonGameScenes.Length; i++)
                {
                    scenes[i] = UnityEngine.SceneManagement.SceneManager.GetSceneByName(nonGameScenes[i]);
                }

                return Array.AsReadOnly(scenes);
            }
        }
    }
}