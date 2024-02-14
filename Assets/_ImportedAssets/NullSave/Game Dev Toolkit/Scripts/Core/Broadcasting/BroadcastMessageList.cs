using System.Collections.Generic;
using UnityEngine;

namespace NullSave.GDTK
{
    [AutoDocLocation("broadcast-system/components")]
    [AutoDoc("This UI control dislays a list of messages for multiple channels.")]
    public class BroadcastMessageList : MonoBehaviour, IBroadcastReceiver
    {

        #region Fields

        [Tooltip("List of channels to display messages for")] public List<string> channels;
        [Tooltip("Label to use as a template for displaying messages")] public Label template;
        [Tooltip("Transform used to contain messages")] public Transform container;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            if (template != null && template.gameObject.LoadedOrNonPrefab())
            {
                template.gameObject.SetActive(false);
            }

            foreach (string channel in channels)
            {
                Broadcaster.SubscribeToChannel(this, channel);
            }
        }

        private void Reset()
        {
            container = transform;
        }

        #endregion

        #region Public Methods

        [AutoDocSuppress]
        public void BroadcastReceived(object sender, string channel, string message, object[] args)
        {
            Label l = Instantiate(template, container);
            l.text = message;
            l.gameObject.SetActive(true);
        }

        [AutoDocSuppress]
        public void PublicBroadcastReceived(object sender, string message) { }

    }

    #endregion

}