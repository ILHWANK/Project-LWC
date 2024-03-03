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
    public string characterName, choiceGroup, skipContext;

    [HideInInspector]
    public string[] contexts, spriteNames;

    [HideInInspector]
    public CameraType[] cameraActions;
}

[System.Serializable]
public class DialogueEvent
{
    public string characterName, choiceGroup;

    public Vector2 line;
    public Dialogue[] dialogues;
}
