//
// Game Developers Toolkit © 2023 by Thomas W Holtquist is licensed under CC BY-SA 4.0 
// https://www.nullsave.com
//

using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NullSave.GDTK
{
    [AutoDocLocation("tool-registry/components")]
    [AutoDoc("This component allows you to easily add existing object to the Tool Registry with a key")]
    [DefaultExecutionOrder(-200)]
    public class ToolRegistryKeyedHelper : MonoBehaviour
    {

        #region Structures

        [Serializable]
        public struct KeyedEntry
        {
            public string key;
            public Object value;
        }

        #endregion

        #region Fields

        [Tooltip("List of entries to register")] public List<KeyedEntry> entries;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            foreach (var obj in entries)
            {
                ToolRegistry.RegisterComponent(obj.value, obj.key);
            }
        }

        #endregion

    }
}