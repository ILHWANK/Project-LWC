using System.Collections.Generic;
using System.Linq;
using script.Common;
using UnityEngine;
using WHDle.UI.Inventory;

namespace Script.Content.Inventory
{
    public class InventoryPopup : UIPopup
    {
        [SerializeField] private List<InventoryItemSlot> _inventoryItemList;
    
        private void Start()
        {
            Open();
        }
    
        public override void Open()
        {
            base.Open();

            // PlayerData 로드
            PlayerDataManager.LoadData("PlayerData");
        
            // 임시 Item 추가
            PlayerDataManager.UpdateInventory("Animal_Gift", 1);
            PlayerDataManager.UpdateInventory("Item1", 1);
            PlayerDataManager.UpdateInventory("Day3_Future", 1);
            PlayerDataManager.UpdateInventory("Day3_Animal", 1);
        
            PlayerDataManager.SaveData("PlayerData");
        
            Init();
        }
    
        public override void Close()
        {
            base.Close();
            
            //
        }

        private void Init()
        {       
            Refresh();
        }

        private void Refresh()
        {
            // 모든 아이템 가져오기
            Dictionary<string, int> inventoryItemMap = PlayerDataManager.GetAllInventoryItems();

            if (inventoryItemMap.Count == 0)
            {
                Debug.Log("인벤토리가 비어 있습니다.");
            }
            else
            {
                foreach (var item in inventoryItemMap)
                {
                    Debug.Log($"아이템: {item.Key}, 개수: {item.Value}");
                }
            }

            var inventoryindex = 0;
        
            var inventoryItemIdList = inventoryItemMap.Keys.ToList();

            foreach (var inventoryItemSlot in _inventoryItemList)
            {
                var itemId = "";

                if (inventoryindex < inventoryItemMap.Count)
                {
                    itemId = inventoryItemIdList[inventoryindex];
                }
                
                inventoryItemSlot.SetData(new InventoryItemSlot.Data
                {
                    ItemId = itemId,
                    Interaction = OnInventoryRefresh
                });

                inventoryindex++;
            }
        
            /*foreach (var inventoryItem in inventoryItemMap)
        {
            _inventoryItemList[inventoryindex].SetData(new InventoryItemSlot.Data
            {
                ItemId = inventoryItem.Key,
                Interaction = OnInventoryRefresh
            });

            inventoryindex++;
            
            if ( 10 <= inventoryindex)
                break;
        }*/
        }

        #region Handle

        private void OnInventoryRefresh()
        {
            Refresh();
        }

        #endregion
    
        #region Events

        public void OnButtonCloseClicked()
        {
            UIManager.Instance.ClosePopup(this);
        }

        #endregion
    }
}
