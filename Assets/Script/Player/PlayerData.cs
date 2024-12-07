using System.Collections.Generic;

public class PlayerData
{
    public int Day;
    public Dictionary<string, int> InventoryMap = new Dictionary<string, int>();
    public Dictionary<string, bool> RoutineMap = new Dictionary<string, bool>(); // 이름 변경: Map 중복 제거
    public string CurrentStoryGroup;
    public string NextStoryGroup;

    public PlayerData(int day, 
        Dictionary<string, bool> routineMap, 
        string currentStoryGroup, 
        string nextStoryGroup, 
        Dictionary<string, int> inventoryMap)
    {
        Day = day;
        RoutineMap = routineMap;
        CurrentStoryGroup = currentStoryGroup;
        NextStoryGroup = nextStoryGroup;
        InventoryMap = inventoryMap;
    }
}