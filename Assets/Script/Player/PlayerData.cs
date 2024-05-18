using System.Collections.Generic;

public class PlayerData
{
    public int _day;

    public string _triggerType;

    public List<string> _inventorys;

    public PlayerData(int day, string triggerType, List<string> inventorys)
    {
        _day = day;
        _triggerType = triggerType;
        _inventorys = inventorys;
    }
}
