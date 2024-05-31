using UnityEditor;
using UnityEngine;
using Utils.EditorHelpers;

namespace Editor
{
    [CustomEditor(typeof(GroupSpawner))]
    public class GroupSpawnerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.LabelField("This action will be delete all children of this object",
                new GUIStyle(GUI.skin.label)
                {
                    alignment = TextAnchor.MiddleCenter,
                    fontStyle = FontStyle.Italic
                });

            if (GUILayout.Button("Spawn Objects"))
            {
                var script = (GroupSpawner)serializedObject.targetObject;
                var transform = script.transform;

                Undo.RegisterChildrenOrderUndo(transform, "Spawn new group objects");

                if (transform.childCount > 0)
                {
                    DeleteAllChildren(transform);
                }

                if (!script.targetObject) return;

                for (var x = 0; x < script.xCount; x++)
                {
                    for (var y = 0; y < script.yCount; y++)
                    {
                        for (var z = 0; z < script.zCount; z++)
                        {
                            var newObject = (GameObject)PrefabUtility.InstantiatePrefab(script.targetObject, transform);

                            var newPosition = new Vector3(x * script.xGap, y * script.yGap, z * script.zGap);

                            newObject.transform.localPosition = newPosition;
                        }
                    }
                }
            }
        }

        private void DeleteAllChildren(Transform parent)
        {
            while (parent.childCount > 0)
            {
                DestroyImmediate(parent.GetChild(0).gameObject);
            }
        }
    }
}