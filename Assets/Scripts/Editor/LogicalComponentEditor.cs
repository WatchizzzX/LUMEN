using System;
using System.Linq;
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
        private const string LogicalPrefabs = "LogicalPrefabs";
        private const string CablePrefab = "Cable/Cable";
        private static readonly string[] AllowedChilds = { "Cable", "CableNodes" };

        private LogicalComponent _logicalComponent;
        private Type _type;

        public override void OnInspectorGUI()
        {
            if (Application.isPlaying) return;
            _logicalComponent = (LogicalComponent)target;

            DrawDefaultInspector();

            _type = _logicalComponent.LogicalType.Type;
            
            if(!CheckSpawnedNode()) return;

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

        /*private void GenerateCables()
        {
            if (_logicalComponent.transform.Find("Cable")) return;
            
            foreach (var input in _logicalComponent.Inputs)
            {
                var prefab = Resources.Load<GameObject>(CablePrefab);
            
                var spawnedCable = (GameObject)PrefabUtility.InstantiatePrefab(prefab, _logicalComponent.transform);
                spawnedCable.name = "Cable";

                var splineComputer = spawnedCable.GetComponent<SplineComputer>();
                splineComputer.SetPoint(0, new SplinePoint(_logicalComponent.transform.position));
                splineComputer.SetPoint(1, new SplinePoint(input.transform.position));

                var tubeGenerator = spawnedCable.GetComponent<TubeGenerator>();
                tubeGenerator.Bake(true, true);

                var cableController = spawnedCable.GetComponent<CableController>();
                cableController.input = input;
            }
        }*/

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
                    var nodeCount = 0;
                    for (var i = 0; i < _logicalComponent.transform.childCount; i++)
                    {
                        if (!AllowedChilds.Contains(_logicalComponent.transform.GetChild(i).name))
                        {
                            nodeCount++;
                        }
                    }

                    if (nodeCount == 1)
                    {
                        var child = _logicalComponent.transform.GetChild(0);
                        if (_type.Name + "Node" == child.name) return false;
                
                        Logger.Log(LoggerChannel.LogicalSystem, Priority.Info, $"{_logicalComponent.name} has wrong child. Type: {_type.Name} - Child: {child.name} It will be replaced");
                        DestroyImmediate(child.gameObject);
                        return true;
                    }
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
    }
}