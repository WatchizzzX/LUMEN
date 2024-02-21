using System;
using UnityEditor;
using UnityEngine;
using Utils;
using Logger = Utils.Logger;

namespace LogicalSystem.Utils
{
    [CustomEditor(typeof(LogicalComponent))]
    public class LogicalComponentEditor : Editor
    {
        private const string PrefabPath = "LogicalPrefabs";

        private LogicalComponent _logicalComponent;
        private Type _type;

        public override void OnInspectorGUI()
        {
            _logicalComponent = (LogicalComponent)target;

            DrawDefaultInspector();

            _type = _logicalComponent.LogicalType.Type;
            
            if(!CheckSpawnedNode()) return;

            var prefab = Resources.Load<GameObject>($"{PrefabPath}/{_type.Name}Node");

            if (prefab == null)
            {
                Logger.Log(LoggerChannel.LogicalSystem, Priority.Error,
                    $"Can't find prefab with path: {PrefabPath}/{_type.Name}Node");
                return;
            }
            
            PrefabUtility.InstantiatePrefab(prefab, _logicalComponent.transform);
            Logger.Log(LoggerChannel.LogicalSystem, Priority.Info,
                $"Successfully spawned {_type}Node");
        }

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
                        $"{_logicalComponent.name} has a multiple childs. The will be deleted");
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
    }
}