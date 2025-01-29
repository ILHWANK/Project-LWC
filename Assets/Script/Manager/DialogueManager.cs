using System;
using System.Collections.Generic;
using Script.Content.Dialogue;
using UnityEngine;
using Yarn;
using Yarn.Unity;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; set; }

    [SerializeField]private DialogueRunner dialogueRunner;

    private UIDialoguePopup dialoguePopup;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        //dialoguePopup = UIManager.Instance.GetPrefabComponent<UIDialoguePopup>("DialoguePopup");
    }

    public void StartDialogue(string dialogueNode)
    {
        UIManager.Instance.OpenPopup("DialoguePopup");
        
        dialogueRunner.StartDialogue(dialogueNode);
    }
}