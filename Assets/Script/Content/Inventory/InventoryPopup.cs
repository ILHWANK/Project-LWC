using System.Collections.Generic;
using Script.Core.UI;
using Script.Data;
using Script.Data.ScriptableObject;
using Script.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Content.Inventory
{
    public class InventoryPopup : UIPopup
    {
        // Data
        [SerializeField] private InventoryDataSO inventoryData;

        [SerializeField] private List<InventoryItemSlot> inventoryItemSlotList;
        [SerializeField] private Button closeButton;
    
        public override void OnEnter()
        {
            base.OnEnter();
            
            inventoryData.AddItem(new ItemData()
            {
                itemID = "Day1_Oriental",
                itemCount = 3
            });
            
            inventoryData.AddItem(new ItemData()
            {
                itemID = "Item1",
                itemCount = 1
            });
            
            closeButton.onClick.AddListener(OnCloseButtonClicked);
            UpdateUI();
        }

        private void OnEnable()
        {
            inventoryData.OnInventoryUpdated += UpdateUI;
        }

        private void OnDisable()
        {
            inventoryData.OnInventoryUpdated -= UpdateUI;
        }

        private void UpdateUI()
        {
            Init();

            var slotIndex = 0;
            
            foreach (var itemData in inventoryData.items)
            {
                inventoryItemSlotList[slotIndex].Set(itemData);

                slotIndex++;
                if (inventoryItemSlotList.Count <= slotIndex)
                {
                    break;
                }
            }
        }
        
        private void Init()
        {
            foreach (var inventoryItemSlot in inventoryItemSlotList)
            {
                inventoryItemSlot.Set(null);
            }
        }

        #region Event
    
        private void OnCloseButtonClicked()
        {
            UIManager.Instance.CloseLastPopup();
        }

        #endregion
    }
}
