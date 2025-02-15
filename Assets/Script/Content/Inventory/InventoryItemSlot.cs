using Script.Common.UI;
using Script.Core.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WHDle.UI.Inventory
{
    public class InventoryItemSlot : MonoBehaviour
    {
        [SerializeField] private Button _click;

        [SerializeField] private UIPopup _inventoryResult;

        public GameObject SlotInner;
        public Image ItemImage;
        public TMP_Text AmountText;

        public void Start()
        {
            _click.onClick.AddListener(OnClicked);
        }

        public void DeleteItem()
        {
            ItemImage.sprite = null;
            AmountText.text = string.Empty;

            SlotInner.SetActive(false);
        }

        public void SlotClear()
        {
            ItemImage.sprite = null;
            AmountText.text = string.Empty;
        }

        #region Event

        private void OnClicked()
        {
            UIManager.Instance.OpenPopup("InventoryItemInfoPopup");
        }

        #endregion
    }
}