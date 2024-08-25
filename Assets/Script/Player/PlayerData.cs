using System.Collections.Generic;

public class PlayerData
{
    public int Day;
    
    public List<string> Inventorys;
    public Dictionary<string, bool> RoutineMapMap; // 추후 Gorup 변경 될 가능성 있음

    public string CurrentStoryGroup;
    public string NextStoryGroup;

    public PlayerData(int day, 
                      Dictionary<string, bool> routineMap, string currentStoryGroup, string nextStoryGroup, 
                      List<string> inventoryList)
    {
        Day = day;
        RoutineMapMap = routineMap;
        CurrentStoryGroup = currentStoryGroup;
        NextStoryGroup = nextStoryGroup;
        Inventorys = inventoryList;
    }
}
