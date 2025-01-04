using System.Collections.Generic;
using UnityEngine;

public static class PlayerDataManager
{
    private static PlayerData _cachedData;

    public static PlayerData CachedData
    {
        get
        {
            if (_cachedData == null)
            {
                Debug.LogError("PlayerData가 로드되지 않았습니다.");
            }

            return _cachedData;
        }
    }

    public static void LoadData(string saveFileName)
    {
        var data = PlayerDataFileHandler.FileLoad(saveFileName);
        if (data != null)
        {
            _cachedData = data;
        }
        else
        {
            Debug.LogError($"저장 파일 {saveFileName}을 찾을 수 없습니다.");
        }
    }

    public static void SaveData(string saveFileName)
    {
        if (_cachedData != null)
        {
            PlayerDataFileHandler.FileSave(_cachedData, saveFileName);
        }
        else
        {
            Debug.LogError("저장할 PlayerData가 없습니다.");
        }
    }

    public static void UpdateInventory(string itemKey, int itemCount)
    {
        if (_cachedData == null)
        {
            Debug.LogError("PlayerData가 로드되지 않았습니다.");
            return;
        }

        if (string.IsNullOrEmpty(itemKey))
        {
            Debug.LogError("itemKey가 null이거나 비어 있습니다.");
            return;
        }

        if (_cachedData.InventoryMap == null)
        {
            Debug.LogWarning("InventoryMap이 초기화되지 않았습니다. 새로 생성합니다.");
            _cachedData.InventoryMap = new Dictionary<string, int>();
        }

        if (0 < itemCount)
        {
            _cachedData.InventoryMap[itemKey] = itemCount;            
        }
        else
        {
            _cachedData.InventoryMap.Remove(itemKey);
        }
    }
    
    public static void UpdateDay(int newDay)
    {
        if (_cachedData == null)
        {
            Debug.LogError("PlayerData가 로드되지 않았습니다.");
            return;
        }

        _cachedData.Day = newDay;
    }

    public static void UpdateRoutine(string routineKey, bool isComplete)
    {
        if (_cachedData == null)
        {
            Debug.LogError("PlayerData가 로드되지 않았습니다.");
            return;
        }

        _cachedData.RoutineMap[routineKey] = isComplete;
    }

    public static int GetInventoryItemCount(string itemKey)
    {
        if (_cachedData == null || !_cachedData.InventoryMap.ContainsKey(itemKey))
        {
            return 0;
        }

        return _cachedData.InventoryMap[itemKey];
    }
    
    public static Dictionary<string, int> GetAllInventoryItems()
    {
        if (_cachedData == null)
        {
            Debug.LogError("PlayerData가 로드되지 않았습니다.");
            return new Dictionary<string, int>(); // 빈 Dictionary 반환
        }

        return new Dictionary<string, int>(_cachedData.InventoryMap);
    }

    public static bool GetRoutineStatus(string routineKey)
    {
        if (_cachedData == null || !_cachedData.RoutineMap.ContainsKey(routineKey))
        {
            return false;
        }

        return _cachedData.RoutineMap[routineKey];
    }

    public static int GetDay()
    {
        if (_cachedData == null)
        {
            return 0;
        }

        return _cachedData.Day;
    }
}