using System;
using Codice.CM.SEIDInfo;
using EventBusSystem;
using EventBusSystem.SerializedSignals;
using UnityEditor;
using UnityEngine;

namespace Editor.EventBus
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
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var eventRaiserSingle = target as EventRaiserSingle;

            var targetSerializedType =
                SignalDictionary.TypeToSerializedType[SignalDictionary.EnumToType[eventRaiserSingle.eventName]];

            if (eventRaiserSingle.signal == null ||
                !eventRaiserSingle.signal.GetType().IsAssignableFrom(targetSerializedType))
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