using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    [SerializeField]
    ChoiceEvent choiceEvent;

    // Temp ObjectPool 적용 예정
    [SerializeField]
    GameObject choiceObject, choiceObjectOne, choiceObjectTwo, choiceObjectThree, choiceObjectFour;

    [SerializeField]
    Button choiceButtonOne, choiceButtonTwo, choiceButtonThree, choiceButtonFour;

    [SerializeField]
    Text choiceTextOne, choiceTextTwo, choiceTextThree, choiceTextFour;

    PlayerAction playerAction;

    DialogueManager dialogueManager;

    List <string> dialogurGroups = new List<string>();

    void Start()
    {
        playerAction = FindObjectOfType<PlayerAction>();

        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    void Update()
    {
        
    }

    public void SetChoiceData(string pChoiceGroup)
    {
        CSVDataManager.Instance.SetChoiceData(pChoiceGroup);

        int endIndex = CSVDataManager.Instance.GetEndIndex(CSVDataManager.DataType.Choice);

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

        dialogueManager.EndStory();
        dialogueManager.TempPlayStory();
    }

    public void OnClick_ChoiceButtonTwo()
    {
        playerAction.currentDialogueGroup = dialogurGroups[1];

        TempResetObject(false);

        dialogueManager.EndStory();
        dialogueManager.TempPlayStory();
    }

    public void OnClick_ChoiceButtonThree()
    {
        playerAction.currentDialogueGroup = dialogurGroups[2];

        TempResetObject(false);

        dialogueManager.EndStory();
        dialogueManager.TempPlayStory();
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
