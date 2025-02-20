using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Data.ScriptableObject
{
    [CreateAssetMenu(menuName = "UI/InventoryData")]
    public class InventoryDataSO : UnityEngine.ScriptableObject
    {
        public List<ItemData> items;

        public event Action OnInventoryUpdated;

        public void AddItem(ItemData itemData)
        {
            if (items.Exists(x => x.itemID == itemData.itemID))
            {
                var item = items.Find(x => x.itemID == itemData.itemID);

                item.itemCount += itemData.itemCount;

                if (item.itemCount <= 0)
                {
                    RemoveItem(item);
                }
            }
            else
            {
                items.Add(itemData);
                OnInventoryUpdated?.Invoke();
            }
        }

        public void RemoveItem(ItemData itemData)
        {
            items.Remove(itemData);
            OnInventoryUpdated?.Invoke();
        }
    }
}