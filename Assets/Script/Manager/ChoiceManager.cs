using System.Collections.Generic;
using Script.Manager;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    [SerializeField] ChoiceEvent choiceEvent;

    [SerializeField] private GameObject choiceObject;
    [SerializeField] private GameObject choiceObjectOne;
    [SerializeField] private GameObject choiceObjectTwo;
    [SerializeField] private GameObject choiceObjectThree;

    [SerializeField] private Button choiceButtonOne;
    [SerializeField] private Button choiceButtonTwo; 
    [SerializeField] private Button choiceButtonThree;

    [SerializeField] private Text choiceTextOne;
    [SerializeField] private Text choiceTextTwo;
    [SerializeField] private Text choiceTextThree;

    PlayerAction playerAction;

    DialogueManagerTemp _dialogueManagerTemp;

    List <string> dialogurGroups = new List<string>();

    void Start()
    {
        playerAction = FindObjectOfType<PlayerAction>();

        _dialogueManagerTemp = FindObjectOfType<DialogueManagerTemp>();
    }

    void Update()
    {
        
    }

    public void SetChoiceData(string pChoiceGroup)
    {
        CSVDataManager.Instance.SetChoiceData(pChoiceGroup);

        var endIndex = CSVDataManager.Instance.GetEndIndex(CSVDataManager.DataType.Choice);

        choiceEvent.choices = CSVDataManager.Instance.GetChoice(1, endIndex);

        dialogurGroups.Clear();
        TempResetObject(false);

        if (choiceEvent.choices.Length > 0)
        {
            string tempText;

            choiceObject.SetActive(true);

            for (int i = 0; i < choiceEvent.choices.Length; ++i)
            {
                tempText = choiceEvent.choices[i].context;
                dialogurGroups.Add(choiceEvent.choices[i].dialogueGroup);

                if (i == 0)
                {
                    choiceObjectOne.SetActive(true);
                    choiceTextOne.text = tempText;
                }
                else if (i == 1)
                {
                    choiceObjectTwo.SetActive(true);
                    choiceTextTwo.text = tempText;
                }
                else if (i == 2)
                {
                    choiceObjectThree.SetActive(true);
                    choiceTextThree.text = tempText;
                }
            }
        }
        else
        {
            choiceObject.SetActive(false);
        }
    }

    public void OnClick_ChoiceButtonOne()
    {
        playerAction.currentDialogueGroup = dialogurGroups[0];

        TempResetObject(false);

        _dialogueManagerTemp.EndStory();
        _dialogueManagerTemp.TempPlayStory();
    }

    public void OnClick_ChoiceButtonTwo()
    {
        playerAction.currentDialogueGroup = dialogurGroups[1];

        TempResetObject(false);

        _dialogueManagerTemp.EndStory();
        _dialogueManagerTemp.TempPlayStory();
    }

    public void OnClick_ChoiceButtonThree()
    {
        playerAction.currentDialogueGroup = dialogurGroups[2];

        TempResetObject(false);

        _dialogueManagerTemp.EndStory();
        _dialogueManagerTemp.TempPlayStory();
    }

    private void TempResetObject(bool isActive)
    {
        choiceObject.SetActive(isActive);

        // Destroy
        choiceObjectOne.SetActive(isActive);
        choiceObjectTwo.SetActive(isActive);
        choiceObjectThree.SetActive(isActive);
    }
}
