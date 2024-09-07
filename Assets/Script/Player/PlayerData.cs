using System.Collections.Generic;

public class PlayerData
{
    public int Day;
    
    public Dictionary<string, int> InventoryMap;
    public Dictionary<string, bool> RoutineMapMap; // 추후 Group 변경 될 가능성 있음

    public string CurrentStoryGroup;
    public string NextStoryGroup;

    public PlayerData(int day, 
                      Dictionary<string, bool> routineMap, string currentStoryGroup, string nextStoryGroup, 
                      Dictionary<string, int> inventoryMap)
    {
        Day = day;
        RoutineMapMap = routineMap;
        CurrentStoryGroup = currentStoryGroup;
        NextStoryGroup = nextStoryGroup;
        InventoryMap = inventoryMap;
    }
}
