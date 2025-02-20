using Script.Common.UI;
using Script.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Script.Content.Inventory
{
    public class InventoryItemSlot : MonoBehaviour
    {
        [SerializeField] private Button click;
        [SerializeField] private GameObject slotInner;
        
        public Image ItemImage;
        public TMP_Text AmountText;

        public void Start()
        {
            click.onClick.AddListener(OnClicked);
        }
        
        public void Set(ItemData itemData)
        {
            Init();

            if (itemData == null) 
                return;
            
            var itemTable = CSVDialogueParser.LoadDialogueTable("Assets/Resources/DataTable/ItemTable.csv");
            var itemTableData = itemTable.GetByColumn("Item_Id", itemData.itemID);

            Debug.Log($"Item Image 위치 : {itemTableData["Item_Path"]}");
            
            ItemImage.sprite = null;
            AmountText.text = itemData.itemCount.ToString();
            
            slotInner.SetActive(true);   
        }

        private void Init()
        {
            ItemImage.sprite = null;
            AmountText.text = string.Empty;

            slotInner.SetActive(false);    
        }

        #region Event

        private void OnClicked()
        {
            UIManager.Instance.OpenPopup("InventoryItemInfoPopup");
        }

        #endregion
    }
}