using System.Collections.Generic;

public class PlayerData
{
    public int Day;
    
    public List<string> Inventorys;
    public List<string> Routine; // 추후 Gorup 변경 될 가능성 있음

    public string CurrentStoryGroup;
    public string NextStoryGroup;

    public PlayerData(int day, 
                      List<string> routine, string currentStoryGroup, string nextStoryGroup, 
                      List<string> inventoryList)
    {
        Day = day;
        Routine = routine;
        CurrentStoryGroup = currentStoryGroup;
        NextStoryGroup = nextStoryGroup;
        Inventorys = inventoryList;
    }
}
