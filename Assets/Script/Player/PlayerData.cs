using System.Collections.Generic;

public class PlayerData
{
    public int Day;

    public string TriggerType;
    public string CurrentStoryGroup;
    public string NextStoryGroup;

    public List<string> Inventorys;

    public PlayerData(int day, 
                      string triggerType, string currentStoryGroup, string nextStoryGroup, 
                      List<string> inventorys)
    {
        Day = day;
        TriggerType = triggerType;
        CurrentStoryGroup = currentStoryGroup;
        NextStoryGroup = nextStoryGroup;
        Inventorys = inventorys;
    }
}
