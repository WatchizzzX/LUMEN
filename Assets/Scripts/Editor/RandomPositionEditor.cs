using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Utils;

namespace Editor
{
    [CustomEditor(typeof(RandomPosition))]
    [CanEditMultipleObjects]
    public class RandomPositionEditor : UnityEditor.Editor
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
                    RandomPosition((RandomPosition)targetObject);
                    RandomRotation((RandomPosition)targetObject);
                }
            }
            else
            {
                RandomPosition((RandomPosition)serializedObject.targetObject);
                RandomRotation((RandomPosition)serializedObject.targetObject);
            }
        }

        private int RandomSign()
        {
            return (int)Mathf.Sign(Random.Range(-1, 1));
        }

        private void RandomPosition(RandomPosition targetObject)
        {
            var transform = targetObject.transform;

            var newXPosition = transform.position.x +
                               Random.Range(targetObject.xPositionRange.x, targetObject.xPositionRange.y) *
                               RandomSign();
                
            var newYPosition = transform.position.y +
                               Random.Range(targetObject.yPositionRange.x, targetObject.yPositionRange.y) *
                               RandomSign();
                
            var newZPosition = transform.position.z +
                               Random.Range(targetObject.zPositionRange.x, targetObject.zPositionRange.y) *
                               RandomSign();
                
            transform.position = new Vector3(newXPosition, newYPosition, newZPosition);
        }

        private void RandomRotation(RandomPosition targetObject)
        {
            var transform = targetObject.transform;

            var newXRotation = transform.eulerAngles.x +
                               Random.Range(targetObject.xRotationRange.x, targetObject.xRotationRange.y) *
                               RandomSign();
                
            var newYRotation = transform.eulerAngles.y +
                               Random.Range(targetObject.yRotationRange.x, targetObject.yRotationRange.y) *
                               RandomSign();
                
            var newZRotation = transform.eulerAngles.z +
                               Random.Range(targetObject.zRotationRange.x, targetObject.zRotationRange.y) *
                               RandomSign();
                
            transform.eulerAngles = new Vector3(newXRotation, newYRotation, newZRotation);
        }
    }
}