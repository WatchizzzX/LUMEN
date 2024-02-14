using System;
using UnityEngine;

namespace NullSave.GDTK
{
    [Serializable]
    [AutoDocSuppress]
    public class InteractorLabel
    {
        #region Enumerations

        public enum TextSource
        {
            InteractionText = 0,
            Args = 1,
        }

        #endregion

        #region Fields

        public Label label;
        [TextArea(2, 5)] public string format = "{0}";
        public TextSource textSource;
        public int argIndex;

        #endregion

    }
}