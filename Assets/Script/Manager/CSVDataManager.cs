using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVDataManager : MonoBehaviour
{
    public enum DataType
    {
        Dialogue,
        Letter,
        Choice,
        None
    }

    public static CSVDataManager Instance;

    [SerializeField]
    public string dialoguefilePath, letterfilePath, choicefilePath;

    Dictionary<int, Dialogue> dialogueDictionary = new Dictionary<int, Dialogue>();
    Dictionary<int, Choice> choiceDictionary = new Dictionary<int, Choice>();
    Dictionary<int, Letter> letterDictionary = new Dictionary<int, Letter>();

    CSVParse csvParse = new CSVParse();

    public static bool isEnd = false;

    void Awake(){
        if(Instance == null){
            Instance = this;

            csvParse = GetComponent<CSVParse>();

            SetDialogueData(dialoguefilePath);
        }
    }

    // Dialogue
    public void SetDialogueData(string pDialougeGroup)
    {
        dialogueDictionary.Clear();

        Dialogue[] dialogues = csvParse.DialogueParse(dialoguefilePath, pDialougeGroup);

        for (int i = 0; i < dialogues.Length; ++i)
        {
            dialogueDictionary.Add(i + 1, dialogues[i]);
        }

        isEnd = true;
    }

    public Dialogue[] GetDialogue(int pStartIndex, int pEndIndex){
        List<Dialogue> dialogueList = new List<Dialogue>();

        for (int i = 0 ; i <= pEndIndex - pStartIndex ; ++i){
            dialogueList.Add(dialogueDictionary[pStartIndex + i]);
        }

        return dialogueList.ToArray();
    }

    // Choice
    public void SetChoiceData(string pChoiceGroup)
    {
        choiceDictionary.Clear();

        Choice[] choices = csvParse.ChoiceParse(choicefilePath, pChoiceGroup);

        for (int i = 0; i < choices.Length; ++i)
        {
            choiceDictionary.Add(i + 1, choices[i]);
        }
    }

    public Choice[] GetChoice(int pStartIndex, int pEndIndex)
    {
        List<Choice> choiceList = new List<Choice>();

        for (int i = 0; i <= pEndIndex - pStartIndex; ++i)
        {
            choiceList.Add(choiceDictionary[pStartIndex + i]);
        }

        return choiceList.ToArray();
    }


    //
    public int GetStartIndex()
    {
        return 0;
    }

    public int GetEndIndex(DataType pDataType)
    {
        int endIndex;

        switch (pDataType)
        {
            case DataType.Dialogue:
                {
                    endIndex = dialogueDictionary.Count;
                    break;
                }
            case DataType.Letter:
                {
                    endIndex = 0;
                    break;
                }
            case DataType.Choice:
                {
                    endIndex = choiceDictionary.Count;
                    break;
                }
            default:
                {
                    endIndex = 0;
                    break;
                }
        }

        return endIndex;
    }
}
