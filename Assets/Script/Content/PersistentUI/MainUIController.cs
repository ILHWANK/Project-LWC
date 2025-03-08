using Script.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Content.PersistentUI
{
    public class MainUIController : MonoBehaviour
    {
        public static MainUIController Instance;

        [SerializeField] private GameObject mainUI;

        [SerializeField] private Button mainMenuButton;
        [SerializeField] private Button interactionButton;
        [SerializeField] private Button inventoryButton;

        [SerializeField] private TextMeshProUGUI dayText;

        private void Awake()
        {
            Instance = this;
            gameObject.SetActive(false);
        }

        private void Start()
        {
            AddListener();
        }

        private void OnEnable()
        {
            MessageSystem.Instance.Subscribe<int>("DayUpdate", UpdateDay);
        }

        private void OnDisable()
        {
            MessageSystem.Instance.Unsubscribe<int>("DayUpdate", UpdateDay);
        }

        private void AddListener()
        {
            mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
            interactionButton.onClick.AddListener(OnInteractionButtonCClicked);
            inventoryButton.onClick.AddListener(OnInventoryButtonClicked);
        }

        #region Message

        private void UpdateDay(int day)
        {
            dayText.text = $"Day-{day}";
        }

        #endregion

        #region Handle

        public void SetActive(bool isActive)
        {
            mainUI.SetActive(isActive);
        }
        
        #endregion
        
        #region Event

        private void OnMainMenuButtonClicked()
        {
            TitleUIController.Instance.ShowTitle();
            
            LoadingUIController.Instance.ClearLoadingItems();
        }

        private void OnInteractionButtonCClicked()
        {
            var currentDay = GameDataManager.Instance.GameData.CurrentDay;
            
            UIManager.Instance.OpenPanel("DayTransitionPanel", new DayTransitionPanel.Data
            {
                CurrentDay = currentDay
            });
        }

        private void OnInventoryButtonClicked()
        {
            SetActive(false);
            
            UIManager.Instance.OpenPopup("InventoryPopup");
        }

        #endregion
    }
}