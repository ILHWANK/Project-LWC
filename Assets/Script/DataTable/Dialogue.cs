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
    ContextUp,
    ContextDown,
    Narration,
    Letter,
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

    [HideInInspector]
    public DialogueType dialogueType;
}

[System.Serializable]
public class DialogueEvent
{
    public string characterName, choiceGroup;

    public Vector2 line;
    public Dialogue[] dialogues;
}
