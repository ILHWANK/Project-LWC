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

    [HideInInspector] public string contextName;
    [HideInInspector] public string choiceGroup;
    [HideInInspector] public string skipContext;

    [HideInInspector] public string[] contexts; 
    [HideInInspector] public string[] spriteNames;

    [HideInInspector] public CameraType[] cameraActions;

    [HideInInspector] public DialogueType dialogueType;
}

[System.Serializable]
public class DialogueEvent
{
    public string characterName;
    public string choiceGroup;

    public Vector2 line;
    public Dialogue[] dialogues;
}
