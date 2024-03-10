using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    [SerializeField] DialogueEvent dialogueEvent;
    /*
    public Dialogue[] GetStory()
    {
        int endIndex = CSVDataManager.Instance.GetEndIndex(CSVDataManager.DataType.Dialogue);

        dialogueEvent.dialogues = CSVDataManager.Instance.GetDialogue(1, endIndex);

        return dialogueEvent.dialogues;
    }
    */
}
