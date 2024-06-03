using Baracuda.Monitoring;
using DG.Tweening;
using EventBusSystem;
using EventBusSystem.Signals.DeveloperSignals;
using InteractionSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [MTag("UI Interactable Handler")]
    [MGroupName("UI Interactable Handler")]
    public class UIInteractableHandler : EventBehaviour
    {
        [SerializeField] private Image pointerImage;
        [SerializeField] private TextMeshProUGUI pointerText;
        [SerializeField] private float smoothTime;

        private IInteractable _foundedInteractable;
        private GameObject _foundedInteractableGameObject;

        private Camera _camera;

        private Tweener _tweener;

        private Vector3 _calculatedVelocity;

        [Monitor] public bool IsVisible { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            _camera = Camera.main;
            pointerImage.color = new Color(1f, 1f, 1f, 0f);
            pointerText.color = new Color(1f, 1f, 1f, 0f);
        }

        public void OnFoundInteractable(GameObject foundedInteractable)
        {
            _foundedInteractable = foundedInteractable.GetComponent<IInteractable>();
            _foundedInteractableGameObject = foundedInteractable;
        }

        public void OnLossInteractable()
        {
            _foundedInteractable = null;
            ChangeVisibility(false);
        }

        private void LateUpdate()
        {
            if (_foundedInteractable == null) return;

            var minX = pointerImage.GetPixelAdjustedRect().width / 2;
            var maxX = Screen.width - minX;
            var minY = pointerImage.GetPixelAdjustedRect().height / 2;
            var maxY = Screen.height - minY;
            var realPos = _camera.WorldToScreenPoint(_foundedInteractableGameObject.transform.position);
            var pos = realPos;
            
            // Limit the X and Y positions
            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY) - 50f;

            pointerImage.transform.position = Vector3.SmoothDamp(pointerImage.transform.position, pos,
                ref _calculatedVelocity, smoothTime);
            
            if (IsVisible) return;
            ChangeVisibility(true);
        }

        private void ChangeVisibility(bool newState)
        {
            if (IsVisible == newState) return;

            _tweener?.Kill();

            IsVisible = newState;

            if (IsVisible)
                pointerText.text = _foundedInteractable.GetMessage();

            _tweener = DOVirtual.Float(pointerImage.color.a, IsVisible ? 1f : 0f, smoothTime, value =>
            {
                var tempColor = new Color(1, 1, 1, value);
                pointerImage.color = tempColor;
                pointerText.color = tempColor;
            });
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