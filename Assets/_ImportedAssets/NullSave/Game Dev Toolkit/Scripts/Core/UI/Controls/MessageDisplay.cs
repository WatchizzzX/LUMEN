using UnityEngine;

namespace NullSave.GDTK
{
    [AutoDocLocation("ui")]
    [AutoDoc("This component provides a UI for displaying messages")]
    public class MessageDisplay : MonoBehaviour
    {

        #region Fields

        [Tooltip("Label used to display message")] public Label messageLabel;

        #endregion

    }
}