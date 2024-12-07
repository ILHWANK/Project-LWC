using System.Collections.Generic;
using script.Common;
using UnityEngine;
using WHDle.UI.Inventory;

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

        foreach (var inventoryItemSlot in _inventoryItemList)
        {
            inventoryItemSlot.Reset();
        }

        // 1. PlayerData 로드
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
        
        Debug.Log("Close 확인");
    }

    private void Init()
    {       
        // 2. 모든 아이템 가져오기
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
        
        foreach (var inventoryItem in inventoryItemMap)
        {
            _inventoryItemList[inventoryindex].SetData(new InventoryItemSlot.Data
            {
                ItemId = inventoryItem.Key
            });

            inventoryindex++;
            
            if ( 10 <= inventoryindex)
                break;
        }
    }
    
    public void OnButtonCloseClicked()
    {
        UIManager.Instance.ClosePopup(this);
    }
}
