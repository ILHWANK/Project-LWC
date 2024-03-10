using UnityEngine;

[System.Serializable]
public class Choice
{
    [HideInInspector]
    public string context, item;

    [HideInInspector]
    public string dialogueGroup;

    [HideInInspector]
    public string likeabilityWorld, likeabilityValue;
}


[System.Serializable]
public class ChoiceEvent
{
    public Choice[] choices;
}
