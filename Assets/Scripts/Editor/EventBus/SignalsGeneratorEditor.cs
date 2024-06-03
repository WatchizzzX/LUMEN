using System;
using System.IO;
using System.Linq;
using System.Text;
using EventBusSystem.Interfaces;
using UnityEditor;
using UnityEngine.SceneManagement;
using Utils.Extra;
using Logger = Utils.Extra.Logger;

namespace Editor.EventBus
{
    public class SignalsGeneratorEditor : UnityEditor.Editor
    {
        [MenuItem("Tools/EventBus/Regenerate files")]
        public static void GenerateSignalEnum()
        {
            Logger.Log(LoggerChannel.EditorTools, Priority.Info, "Starting generate signal enum & dictionary...");

            try
            {
                var signalTypes = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(type => typeof(ISignal).IsAssignableFrom(type) && type.IsClass)
                    .ToList();

                var enumCode = new StringBuilder();
                enumCode.AppendLine("////////////////////////////////////////////////////////////////////////////////");
                enumCode.AppendLine("// This code is auto-generated. Please don't change this code to avoid errors //");
                enumCode.AppendLine("////////////////////////////////////////////////////////////////////////////////");
                enumCode.AppendLine();
                enumCode.AppendLine("namespace EventBusSystem");
                enumCode.AppendLine("{");
                enumCode.AppendLine("   public enum SignalEnum");
                enumCode.AppendLine("   {");
                foreach (var signalType in signalTypes)
                {
                    enumCode.AppendLine($"      {signalType.Name},");
                }

                enumCode.AppendLine("   }");
                enumCode.AppendLine("}");

                File.WriteAllText("Assets/Scripts/EventBusSystem/SignalEnum.cs", enumCode.ToString());

                Logger.Log(LoggerChannel.EditorTools, Priority.Info, "Enum successfully generated!");

                var dictionaryCode = new StringBuilder();
                dictionaryCode.AppendLine(
                    "////////////////////////////////////////////////////////////////////////////////");
                dictionaryCode.AppendLine(
                    "// This code is auto-generated. Please don't change this code to avoid errors //");
                dictionaryCode.AppendLine(
                    "////////////////////////////////////////////////////////////////////////////////");
                dictionaryCode.AppendLine();
                dictionaryCode.AppendLine("using System;");
                dictionaryCode.AppendLine("using System.Collections.Generic;");
                dictionaryCode.AppendLine();
                dictionaryCode.AppendLine("namespace EventBusSystem");
                dictionaryCode.AppendLine("{");
                dictionaryCode.AppendLine("    public static class SignalDictionary");
                dictionaryCode.AppendLine("    {");
                dictionaryCode.AppendLine(
                    "        public static readonly Dictionary<Type, SignalEnum> TypeToEnum = new()");
                dictionaryCode.AppendLine("        {");
                foreach (var signalType in signalTypes)
                {
                    dictionaryCode.AppendLine($"            {{typeof({signalType}), SignalEnum.{signalType.Name}}},");
                }

                dictionaryCode.AppendLine("        };");
                dictionaryCode.AppendLine();
                dictionaryCode.AppendLine(
                    "        public static readonly Dictionary<SignalEnum, Type> EnumToType = new()");
                dictionaryCode.AppendLine("        {");
                foreach (var signalType in signalTypes)
                {
                    dictionaryCode.AppendLine($"            {{SignalEnum.{signalType.Name}, typeof({signalType})}},");
                }

                dictionaryCode.AppendLine("        };");
                dictionaryCode.AppendLine();
                dictionaryCode.AppendLine(
                    "        public static readonly Dictionary<Type, Type> TypeToSerializedType = new()");
                dictionaryCode.AppendLine("        {");
                foreach (var signalType in signalTypes)
                {
                    dictionaryCode.AppendLine($"            {{typeof({signalType}), typeof(EventBusSystem.SerializedSignals.Serialized{signalType.Name})}},");
                }

                dictionaryCode.AppendLine("        };");
                dictionaryCode.AppendLine();
                dictionaryCode.AppendLine(
                    "        public static readonly Dictionary<Type, Type> SerializedTypeToType = new()");
                dictionaryCode.AppendLine("        {");
                foreach (var signalType in signalTypes)
                {
                    dictionaryCode.AppendLine($"            {{typeof(EventBusSystem.SerializedSignals.Serialized{signalType.Name}), typeof({signalType})}},");
                }

                dictionaryCode.AppendLine("        };");
                dictionaryCode.AppendLine("    }");
                dictionaryCode.AppendLine("}");

                File.WriteAllText("Assets/Scripts/EventBusSystem/SignalDictionary.cs", dictionaryCode.ToString());

                Logger.Log(LoggerChannel.EditorTools, Priority.Info, "Dictionary successfully generated!");

                var serializedSignals = new StringBuilder();
                serializedSignals.AppendLine(
                    "////////////////////////////////////////////////////////////////////////////////");
                serializedSignals.AppendLine(
                    "// This code is auto-generated. Please don't change this code to avoid errors //");
                serializedSignals.AppendLine(
                    "////////////////////////////////////////////////////////////////////////////////");

                serializedSignals.AppendLine("using System;");
                serializedSignals.AppendLine("using EasyTransition;");
                serializedSignals.AppendLine("using Enums;");
                serializedSignals.AppendLine("using Unity.Cinemachine;");
                serializedSignals.AppendLine("using NaughtyAttributes;");
                serializedSignals.AppendLine("using UnityEngine;");
                serializedSignals.AppendLine();
                serializedSignals.AppendLine("namespace EventBusSystem.SerializedSignals");
                serializedSignals.AppendLine("{");
                serializedSignals.AppendLine("");

                foreach (var signalType in signalTypes)
                {
                    serializedSignals.AppendLine("  [Serializable]");
                    serializedSignals.AppendLine($"  public class Serialized{signalType.Name} : SerializedSignal");
                    serializedSignals.AppendLine("  {");

                    var fields = signalType.GetFields();
                    foreach (var field in fields)
                    {
                        if (field.FieldType == typeof(Scene))
                            serializedSignals.AppendLine(
                                $"      [SerializeField][Scene] public int {field.Name};");
                        else
                            serializedSignals.AppendLine(
                                $"      [SerializeField] public {field.FieldType.Name} {field.Name};");
                    }

                    serializedSignals.AppendLine("  }");
                    serializedSignals.AppendLine();
                }

                serializedSignals.AppendLine("}");

                File.WriteAllText("Assets/Scripts/EventBusSystem/SerializedSignals/SerializedSignals.cs",
                    serializedSignals.ToString());

                Logger.Log(LoggerChannel.EditorTools, Priority.Info, "Start refresh AssetDatabase...");

                AssetDatabase.Refresh();

                Logger.Log(LoggerChannel.EditorTools, Priority.Info, "Code successfully generated. End of operation");
            }
            catch (Exception ex)
            {
                Logger.Log(LoggerChannel.EditorTools, Priority.Error,
                    $"Error on generating code. Message: {ex.Message}");
            }
        }
    }
}