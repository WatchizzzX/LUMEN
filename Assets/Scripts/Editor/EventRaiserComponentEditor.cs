using System;
using System.Linq;
using System.Reflection;
using EventBusSystem;
using EventBusSystem.SerializedSignals;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils.Extensions;

namespace Editor
{
    public abstract class EventRaiserEditor<T> : UnityEditor.Editor where T : EventBehaviour
    {
        protected T Raiser;

        private void OnEnable()
        {
            Raiser = serializedObject.targetObject as T;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            EditorGUI.BeginDisabledGroup(Application.isPlaying == false);
            {
                if (GUILayout.Button("Raise Events"))
                {
                    OnButtonPressed();
                }
            }
            if (Application.isPlaying == false)
            {
                EditorGUILayout.LabelField("This button will be active during play mode", new GUIStyle(GUI.skin.label)
                {
                    alignment = TextAnchor.MiddleCenter,
                    fontStyle = FontStyle.Italic
                });
            }

            EditorGUI.EndDisabledGroup();
        }

        protected abstract void OnButtonPressed();
    }

    [CustomEditor(typeof(EventRaiserSingle))]
    public class EventRaiserSingleEditor : EventRaiserEditor<EventRaiserSingle>
    {
        private Type[] _implementations;
        private int _implementationTypeIndex;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var eventRaiserSingle = target as EventRaiserSingle;

            if (GUILayout.Button("Create instance"))
            {
                eventRaiserSingle.signal = (SerializedSignal)Activator.CreateInstance(
                    SignalDictionary.TypeToSerializedType[SignalDictionary.EnumToType[Raiser.eventName]]);
            }
        }

        protected override void OnButtonPressed()
        {
            Raiser.Raise();
        }
    }
}