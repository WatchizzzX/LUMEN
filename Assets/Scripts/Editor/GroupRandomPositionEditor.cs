using UnityEditor;
using UnityEngine;
using Utils.EditorHelpers;

namespace Editor
{
    [CustomEditor(typeof(GroupRandomPosition))]
    [CanEditMultipleObjects]
    public class GroupRandomPositionEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!GUILayout.Button("Random position")) return;

            var selectedObjects = serializedObject.targetObjects;
            if (selectedObjects.Length > 1)
            {
                foreach (var targetObject in selectedObjects)
                {
                    RandomPosition((GroupRandomPosition)targetObject);
                    RandomRotation((GroupRandomPosition)targetObject);
                }
            }
            else
            {
                RandomPosition((GroupRandomPosition)serializedObject.targetObject);
                RandomRotation((GroupRandomPosition)serializedObject.targetObject);
            }
        }

        private int RandomSign()
        {
            return (int)Mathf.Sign(Random.Range(-1, 1));
        }

        private void RandomPosition(GroupRandomPosition targetObject)
        {
            var childTransforms = targetObject.gameObject.GetComponentsInChildren<Transform>();
            Undo.RegisterChildrenOrderUndo(targetObject, "Random group position");

            foreach (var childTransform in childTransforms)
            {
                if(childTransform == targetObject.transform) continue;
                
                var newXPosition = childTransform.position.x +
                                   Random.Range(targetObject.xPositionRange.x, targetObject.xPositionRange.y) *
                                   RandomSign();

                var newYPosition = childTransform.position.y +
                                   Random.Range(targetObject.yPositionRange.x, targetObject.yPositionRange.y) *
                                   RandomSign();

                var newZPosition = childTransform.position.z +
                                   Random.Range(targetObject.zPositionRange.x, targetObject.zPositionRange.y) *
                                   RandomSign();

                childTransform.position = new Vector3(newXPosition, newYPosition, newZPosition);
            }
        }

        private void RandomRotation(GroupRandomPosition targetObject)
        {
            var childTransforms = targetObject.gameObject.GetComponentsInChildren<Transform>();
            Undo.RegisterChildrenOrderUndo(targetObject, "Random group rotation");

            foreach (var childTransform in childTransforms)
            {
                if(childTransform == targetObject.transform) continue;
                
                var newXRotation = childTransform.eulerAngles.x +
                                   Random.Range(targetObject.xRotationRange.x, targetObject.xRotationRange.y) *
                                   RandomSign();

                newXRotation = Mathf.Clamp(newXRotation, targetObject.xRotationLimit.x, targetObject.xRotationLimit.y);

                var newYRotation = childTransform.eulerAngles.y +
                                   Random.Range(targetObject.yRotationRange.x, targetObject.yRotationRange.y) *
                                   RandomSign();
                
                newYRotation = Mathf.Clamp(newYRotation, targetObject.yRotationLimit.x, targetObject.yRotationLimit.y);

                var newZRotation = childTransform.eulerAngles.z +
                                   Random.Range(targetObject.zRotationRange.x, targetObject.zRotationRange.y) *
                                   RandomSign();
                
                newZRotation = Mathf.Clamp(newZRotation, targetObject.zRotationLimit.x, targetObject.zRotationLimit.y);

                childTransform.eulerAngles = new Vector3(newXRotation, newYRotation, newZRotation);
            }
        }
    }
}