using System;
using LogicalSystem;
using UnityEditor;
using UnityEngine;
using Utils;
using Logger = Utils.Logger;

namespace Editor
{
    [CustomEditor(typeof(LogicalComponent))]
    public class LogicalComponentEditor : UnityEditor.Editor
    {
        #region Private Const

        /// <summary>
        /// Path to Logical Node prefabs
        /// </summary>
        private const string LogicalPrefabs = "LogicalPrefabs";

        #endregion
        
        #region Private Variables

        /// <summary>
        /// Cached LogicalComponent
        /// </summary>
        private LogicalComponent _logicalComponent;
        
        /// <summary>
        /// Cached Type
        /// </summary>
        private Type _type;

        #endregion

        #region Editor Methods

        public override void OnInspectorGUI()
        {
            if (Application.isPlaying) return;
            _logicalComponent = (LogicalComponent)target;

            DrawDefaultInspector();

            _type = _logicalComponent.LogicalType.Type;
            
            if(!CheckSpawnedNode()) return;

            if (_type == null) return;

            var prefab = Resources.Load<GameObject>($"{LogicalPrefabs}/{_type.Name}Node");

            if (prefab == null)
            {
                Logger.Log(LoggerChannel.LogicalSystem, Priority.Error,
                    $"Can't find prefab with path: {LogicalPrefabs}/{_type.Name}Node");
                return;
            }
            
            PrefabUtility.InstantiatePrefab(prefab, _logicalComponent.transform);
            Logger.Log(LoggerChannel.LogicalSystem, Priority.Info,
                $"Successfully spawned {_type}Node");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Check if node prefab are correctly spawned
        /// </summary>
        /// <returns>True if need to spawn prefab, and else if everything is ok</returns>
        private bool CheckSpawnedNode()
        {
            switch (_logicalComponent.transform.childCount)
            {
                case 1:
                {
                    var child = _logicalComponent.transform.GetChild(0);
                    if (_type.Name + "Node" == child.name) return false;
                
                    Logger.Log(LoggerChannel.LogicalSystem, Priority.Info, $"{_logicalComponent.name} has wrong child. Type: {_type.Name} - Child: {child.name} It will be replaced");
                    DestroyImmediate(child.gameObject);
                    return true;
                }
                case > 1:
                {
                    Logger.Log(LoggerChannel.LogicalSystem, Priority.Warning,
                        $"{_logicalComponent.name} has a multiple childs. They will be deleted");
                    for (var i = 0; i < _logicalComponent.transform.childCount; i++)
                    {
                        DestroyImmediate(_logicalComponent.transform.GetChild(i).gameObject);
                    }

                    return true;
                }
                default:
                    return true;
            }
        }

        #endregion
    }
}