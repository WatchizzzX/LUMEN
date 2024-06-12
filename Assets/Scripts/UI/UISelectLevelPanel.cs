using NaughtyAttributes;
using SaveLoadSystem;
using ServiceLocatorSystem;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UISelectLevelPanel : MonoBehaviour
    {
        [SerializeField,Scene] private int sceneID;
        [SerializeField] private GameObject firstStar;
        [SerializeField] private GameObject secondStar;
        [SerializeField] private GameObject thirdStar;
        [SerializeField] private TextMeshProUGUI timeText;
        
        private void Awake()
        {
            var saveManager = ServiceLocator.Get<SaveLoadManager>();

            saveManager.LoadLevelProgress(sceneID, out var loadedStarsCount, out var loadedTime);

            if (loadedStarsCount == -1)
            {
                timeText.text = "--:--:---";
                return;
            }
            
            timeText.text = loadedTime;
            
            switch (loadedStarsCount)
            {
                case 1:
                    firstStar.SetActive(true);
                    break;
                case 2:
                    firstStar.SetActive(true);
                    secondStar.SetActive(true);
                    break;
                case 3:
                    firstStar.SetActive(true);
                    secondStar.SetActive(true);
                    thirdStar.SetActive(true);
                    break;
            }
        }
    }
}