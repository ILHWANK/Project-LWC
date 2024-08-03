using UnityEngine;

[System.Serializable]
public class DialogueProceeding
{
    [HideInInspector] public string currentStoryGroup;
    [HideInInspector] public string dialogueContext;
    [HideInInspector] public string nextDialogue;
    [HideInInspector] public string witchSpot;
    [HideInInspector] public string ramSpot;
    [HideInInspector] public string trigger;
}

[System.Serializable]
public class DialogueProceedingsEvent
{
    
}
