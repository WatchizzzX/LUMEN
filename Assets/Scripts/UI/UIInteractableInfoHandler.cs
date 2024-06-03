using EPOOutline;
using Baracuda.Monitoring;
using DG.Tweening;
using EventBusSystem;
using EventBusSystem.Signals.DeveloperSignals;
using InteractionSystem;
using LogicalSystem;
using LogicalSystem.LogicalElements;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIInteractableInfoHandler : EventBehaviour
    {
        [SerializeField] private TextMeshProUGUI textName;
        [SerializeField] private TextMeshProUGUI textInfo;
        [SerializeField] private RectTransform targetPanel;
        [SerializeField] private float smoothTime;

        [Space(2f)] [SerializeField] private string andInfo;
        [SerializeField] private string nandInfo;
        [SerializeField] private string orInfo;
        [SerializeField] private string xorInfo;
        [SerializeField] private string norInfo;
        [SerializeField] private string xnorInfo;
        [SerializeField] private string notInfo;

        [Monitor] public bool IsVisible { get; private set; }

        private IInteractable _foundedInteractable;
        private GameObject _foundedInteractableGameObject;

        private LogicalComponent _logicalComponent;

        private Tweener _tweener;

        public void OnFoundInteractable(GameObject foundedInteractable)
        {
            var interactable = foundedInteractable.GetComponent<IInteractable>();

            if (interactable.GetInteractableType() != InteractableType.LogicComponent) return;

            _foundedInteractable = interactable;
            _foundedInteractableGameObject = foundedInteractable;
        }

        public void OnLossInteractable()
        {
            _foundedInteractable = null;
            _foundedInteractableGameObject = null;
        }

        private void LateUpdate()
        {
            if (!_foundedInteractableGameObject)
            {
                if (IsVisible)
                    ChangeVisibility(false);
                return;
            }
            
            _logicalComponent = _foundedInteractableGameObject.transform.parent.parent.GetComponent<LogicalComponent>();

            if (!IsVisible)
                ChangeVisibility(true);
            
            if (_logicalComponent.LogicalType == typeof(And))
            {
                textName.text = "And";
                textInfo.text = andInfo;
            }
            else if (_logicalComponent.LogicalType == typeof(Nand))
            {
                textName.text = "Nand";
                textInfo.text = nandInfo;
            }
            else if (_logicalComponent.LogicalType == typeof(Or))
            {
                textName.text = "Or";
                textInfo.text = orInfo;
            }
            else if (_logicalComponent.LogicalType == typeof(Xor))
            {
                textName.text = "Xor";
                textInfo.text = xorInfo;
            }
            else if (_logicalComponent.LogicalType == typeof(Xnor))
            {
                textName.text = "Xnor";
                textInfo.text = xnorInfo;
            }
            else if (_logicalComponent.LogicalType == typeof(Nor))
            {
                textName.text = "Nor";
                textInfo.text = norInfo;
            }
            else if (_logicalComponent.LogicalType == typeof(Not))
            {
                textName.text = "Not";
                textInfo.text = notInfo;
            }
        }

        private void ChangeVisibility(bool newState)
        {
            if (IsVisible == newState) return;

            _tweener?.Kill();

            IsVisible = newState;

            _tweener = DOVirtual.Float(!IsVisible ? 1520 : 1920, IsVisible ? 1520 : 1920, smoothTime,
                value => { targetPanel.anchoredPosition = new Vector3(value, -50, 0); });

            var inputs = _logicalComponent.Inputs;
            foreach (var input in inputs)
            {
                var outliner = input.GetComponentInChildren<Outlinable>();
                outliner.DrawingMode = IsVisible ? OutlinableDrawingMode.Normal : 0;
            }

            if (!IsVisible)
            {
                _logicalComponent = null;
            }
        }

        [ListenTo(SignalEnum.OnDevModeChanged)]
        private void OnDevModeChanged(EventModel eventModel)
        {
            var payload = (OnDevModeChanged)eventModel.Payload;
            if (payload.InDeveloperMode)
                this.StartMonitoring();
            else
                this.StopMonitoring();
        }
    }
}