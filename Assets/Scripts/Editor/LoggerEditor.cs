using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;
using Logger = Utils.Logger;

namespace Editor
{
    public class LoggerEditor : EditorWindow
    {
        [MenuItem("Logging/Logger Window")]
        public static void ShowWindow()
        {
            GetWindow(typeof(LoggerEditor));
        }

        [FormerlySerializedAs("loggerChannels")] [SerializeField]
        private LoggerChannel loggerLoggerChannels = Logger.KAllChannels;

        private void OnGUI()
        {
            EditorGUI.BeginChangeCheck();
 
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Clear all"))
            {
                loggerLoggerChannels = 0;
            }
            if (GUILayout.Button("Select all"))
            {
                loggerLoggerChannels = Logger.KAllChannels;
            }

            EditorGUILayout.EndHorizontal();

            GUILayout.Label("Click to toggle logging channels", EditorStyles.boldLabel);
        
            foreach (LoggerChannel channel in System.Enum.GetValues(typeof(LoggerChannel)))
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Toggle((loggerLoggerChannels & channel) == channel, "", GUILayout.ExpandWidth(false));
                if (GUILayout.Button(channel.ToString()))
                {
                    loggerLoggerChannels ^= channel;
                }
                EditorGUILayout.EndHorizontal();
            }

            // If the game is playing then update it live when changes are made
            if (EditorApplication.isPlaying && EditorGUI.EndChangeCheck())
            {
                Logger.SetChannels(loggerLoggerChannels);
            }
        }
    
        // When the game starts update the logger instance with the users selections
        private void OnEnable()
        {
            Logger.SetChannels(loggerLoggerChannels);
        }
    }
}