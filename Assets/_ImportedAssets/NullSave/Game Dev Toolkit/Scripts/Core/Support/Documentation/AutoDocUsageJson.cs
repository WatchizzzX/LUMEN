using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace NullSave.GDTK
{
    [AutoDocSuppress]
    public class AutoDocUsageJson
    {
        #region Constants

        private const string INSERT_USAGE = "!! INSERT USAGE HERE";

        #endregion

        #region Fields

        public List<AutoDocUsageMethod> methods;
        public List<AutoDocUsageProperty> properties;

        #endregion

        #region Constructor

        public AutoDocUsageJson()
        {
            methods = new List<AutoDocUsageMethod>();
            properties = new List<AutoDocUsageProperty>();
        }

        #endregion

        #region Public Methods

        public AutoDocUsageMethod GetOrCreateMethodUsage(string name, string signature, string typeName)
        {
            foreach (AutoDocUsageMethod method in methods)
            {
                if (method.name == name && method.signature == signature)
                {
                    return method;
                }
            }

            AutoDocUsageMethod adum = new AutoDocUsageMethod();
            adum.name = name;
            adum.signature = signature;
            adum.usage = INSERT_USAGE;
            methods.Add(adum);

            Debug.Log("New usage entry created for: " + name + " " + signature + " for type " + typeName);
            return adum;
        }

        public AutoDocUsageProperty GetOrCreatePropertyUsage(PropertyInfo prop, bool autoGen, Type t)
        {
            foreach (AutoDocUsageProperty p in properties)
            {
                if (p.name == prop.Name)
                {
                    if (autoGen && p.usage == INSERT_USAGE)
                    {
                        break;
                    }
                    else
                    {
                        return p;
                    }
                }
            }

            AutoDocUsageProperty adup = new AutoDocUsageProperty();
            adup.name = prop.Name;

            if (autoGen)
            {
                adup.usage = "```csharp title=\"Example\"\r\nusing " + t.Namespace + "\r\nusing UnityEngine;\r\n\r\npublic class Example : MonoBehaviour\r\n{\r\n\r\n\tpublic void ExampleMethod(" + t.Name + " target)\r\n\t{\r\n\t\t" +
                    GetFriendlyTypeName(prop.PropertyType, t.Namespace) + " result = target." + prop.Name + ";\r\n\t}\t\n}\r\n```";
            }
            else
            {
                adup.usage = INSERT_USAGE;
            }
            properties.Add(adup);
            Debug.Log("New usage entry created for: " + prop.Name + " on " + t.Name);
            return adup;
        }

        #endregion

        #region Private Methods

        private string GetFriendlyTypeName(Type t, string ns)
        {
            if (t == typeof(bool) || t == typeof(Boolean))
            {
                return "bool";
            }

            if (t == typeof(float))
            {
                return "float";
            }

            if (t == typeof(void))
            {
                return "void";
            }

            if (t == typeof(string))
            {
                return "string";
            }

            if (t.IsGenericType)
            {
                int paramCountIndex = t.Name.IndexOf('`');
                int paramCount = int.Parse(t.Name.Substring(paramCountIndex + 1));
                string result = t.Name.Substring(0, paramCountIndex) + "<";
                if (t.Namespace != "UnityEngine" && !ns.StartsWith(t.Namespace))
                {
                    result = t.Namespace + "." + result;
                }

                for (int i = 0; i < paramCount; i++)
                {
                    if (i > 0)
                    {
                        result += ", ";
                    }

                    result += GetFriendlyTypeName(t.GetGenericArguments()[i], ns);
                }

                result += ">";

                return result;
            }

            if (t.Namespace != "UnityEngine" && !ns.StartsWith(t.Namespace))
            {
                return t.Namespace + "." + t.Name;
            }

            return t.Name;
        }

        #endregion

    }
}