using System.Reflection;
using Bootstrapper;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(BootstrapperSettings), true)]
    public class UniversalSoEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var scriptableObject = (BootstrapperSettings)target;

            var serializedObject = new SerializedObject(scriptableObject);
            var property = serializedObject.GetIterator();

            // Пропускаем "m_Script", так как это скрытое поле, которое содержит ссылку на скрипт
            property.NextVisible(true);

            while (property.NextVisible(false))
            {
                // Если это ScriptableObject, создаем развернутую панель
                if (property.propertyType == SerializedPropertyType.ObjectReference &&
                    property.objectReferenceValue is ScriptableObject subSo)
                {
                    var fieldInfo = scriptableObject.GetType().GetField(property.name,
                        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    if (fieldInfo != null)
                    {
                        var spaces = fieldInfo.GetCustomAttributes(typeof(SpaceAttribute), true);
                        if (spaces.Length > 0)
                        {
                            EditorGUILayout.Space(((SpaceAttribute)spaces[0]).height);
                        }

                        var headers = fieldInfo.GetCustomAttributes(typeof(HeaderAttribute), true);
                        if (headers.Length > 0)
                        {
                            EditorGUILayout.LabelField((headers[0] as HeaderAttribute)?.header, EditorStyles.boldLabel);
                        }
                    }

                    property.isExpanded = EditorGUILayout.Foldout(property.isExpanded, property.displayName);

                    if (!property.isExpanded) continue;
                    EditorGUI.indentLevel++;
                    EditorGUILayout.ObjectField(property);
                    if (subSo)
                    {
                        var subEditor = CreateEditor(subSo);
                        subEditor.OnInspectorGUI();
                    }

                    EditorGUI.indentLevel--;
                }
                else
                {
                    EditorGUILayout.PropertyField(property, true);
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}