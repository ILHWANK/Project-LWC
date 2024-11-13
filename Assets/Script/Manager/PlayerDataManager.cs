using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerDataManager
{
    private static PlayerData _cachedData;

    public static PlayerData CachedData => _cachedData;

    public static void UpdateCache(PlayerData newData)
    {
        _cachedData = newData;
    }

    public static bool IsDataLoaded => _cachedData != null;

    public static void UpdateInventory(string itemKey, int itemCount)
    {
        if (_cachedData != null && _cachedData.InventoryMap.ContainsKey(itemKey))
        {
            _cachedData.InventoryMap[itemKey] = itemCount;
        }
        else if (_cachedData != null)
        {
            _cachedData.InventoryMap.Add(itemKey, itemCount);
        }
    }

    public static void UpdateDay(int newDay)
    {
        if (_cachedData != null)
        {
            _cachedData.Day = newDay;
        }
    }

    public static void SaveData(string saveFileName)
    {
        if (_cachedData != null)
        {
            PlayerDataFileHandler.FileSave(_cachedData, saveFileName);
        }
    }
}
