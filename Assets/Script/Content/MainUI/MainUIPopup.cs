using Script.Core.UI;
using Script.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Content.MainUI
{
    public class MainUIPopup : UIPopup
    {
        [SerializeField] private Button mainMenuButton;
        [SerializeField] private Button interactionButton;
        [SerializeField] private Button inventoryButton;

        [SerializeField] private TextMeshProUGUI dayText;

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

        public override void OnEnter()
        {
            base.OnEnter();
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

        #region Event

        private void OnMainMenuButtonClicked()
        {
        }

        private void OnInteractionButtonCClicked()
        {
            UIManager.Instance.OpenPanel("DayTransitionPanel");
        }

        private void OnInventoryButtonClicked()
        {
            UIManager.Instance.OpenPopup("InventoryPopup");
        }

        #endregion
    }
}