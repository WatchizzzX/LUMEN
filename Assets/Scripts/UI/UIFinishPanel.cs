using Enums;
using EventBusSystem;
using EventBusSystem.Signals.GameSignals;
using EventBusSystem.Signals.SceneSignals;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIFinishPanel : EventBehaviour
    {
        [SerializeField] private GameObject uiFinishPanel;
        [SerializeField] private GameObject starOne;
        [SerializeField] private GameObject starTwo;
        [SerializeField] private GameObject starThree;
        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private GameObject nextLevelButton;

        private int _nextSceneID;
        
        public void LoadNextLevel()
        {
            RaiseEvent(new OnSetScene(_nextSceneID, 0f));
        }
        
        [ListenTo(SignalEnum.OnFinish)]
        private void OnFinish(EventModel eventModel)
        {
            var payload = (OnFinish)eventModel.Payload;
            
            uiFinishPanel.SetActive(true);

            _nextSceneID = payload.NextSceneID;
            
            if(_nextSceneID == -1)
                nextLevelButton.SetActive(false);

            timeText.text = payload.ElapsedTime;

            switch (payload.StarsCount)
            {
                case 1:
                    starOne.SetActive(true);
                    break;
                case 2:
                    starOne.SetActive(true);
                    starTwo.SetActive(true);
                    break;
                case 3:
                    starOne.SetActive(true);
                    starTwo.SetActive(true);
                    starThree.SetActive(true);
                    break;
            }
        }
    }
}