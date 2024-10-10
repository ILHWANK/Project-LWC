using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public DialogueEnum.CameraActionType cameraType;

    [HideInInspector] public string contextName;
    [HideInInspector] public string choiceGroup;
    [HideInInspector] public string skipContext;

    [HideInInspector] public string[] contexts; 
    [HideInInspector] public string[] spriteNames;

    [HideInInspector] public DialogueEnum.CameraActionType[] cameraActions;

    [HideInInspector] public DialogueEnum.DialogueType dialogueType;
}

[System.Serializable]
public class DialogueEvent
{
    public string characterName;
    public string choiceGroup;

    public Vector2 line;
    public Dialogue[] dialogues;
}