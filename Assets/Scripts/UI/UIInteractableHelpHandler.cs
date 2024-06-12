using DG.Tweening;
using EventBusSystem;
using EventBusSystem.Signals.UISignals;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIInteractableHelpHandler : EventBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI infoText;
        [SerializeField] private RectTransform targetPanel;
        [SerializeField] private float smoothTime;
        
        public bool IsVisible { get; private set; }
        
        private Tweener _tweener;
        
        [ListenTo(SignalEnum.ShowUIHelp)]
        private void OnShowInteractableHelp(EventModel eventModel)
        {
            var payload = (ShowUIHelp)eventModel.Payload;
            nameText.text = payload.Name;
            infoText.text = payload.Info;
            ChangeVisibility(true);
        }
        
        [ListenTo(SignalEnum.HideUIHelp)]
        private void OnHideInteractableHelp(EventModel eventModel)
        {
            ChangeVisibility(false);
        }
        
        private void ChangeVisibility(bool newState)
        {
            if (IsVisible == newState) return;

            _tweener?.Kill();

            IsVisible = newState;

            _tweener = DOVirtual.Float(!IsVisible ? 1520 : 1920, IsVisible ? 1520 : 1920, smoothTime,
                value => { targetPanel.anchoredPosition = new Vector3(value, -50, 0); });
        }
    }
}