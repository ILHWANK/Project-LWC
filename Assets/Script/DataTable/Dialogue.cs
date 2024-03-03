using UnityEngine;

public enum CameraType
{
    None,
    FadeOut,
    FadeIn,
    FlashOut,
    FlashIn
}

public enum DialogueType
{
    None,
}

[System.Serializable]
public class Dialogue
{
    public CameraType cameraType;

    [HideInInspector]
    public string characterName;

    [HideInInspector]
    public string[] contexts, spriteNames, choiceGroups;

    [HideInInspector]
    public CameraType[] cameraActions;
}

[System.Serializable]
public class DialogueEvent
{
    public string characterName;

    public Vector2 line;
    public Dialogue[] dialogues;
}
