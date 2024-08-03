using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CSVDataManager : MonoBehaviour
{
    public enum DataType
    {
        Dialogue,
        Letter,
        Choice,
        DialogueProceeding,
        None
    }

    public static CSVDataManager Instance;

    [SerializeField] private string dialoguefilePath;
    [SerializeField] private string letterfilePath;
    [SerializeField] private string choicefilePath;
    [SerializeField] private string dialogueProceedings;

    private Dictionary<int, Dialogue> _dialogueDictionaryMap;
    private Dictionary<int, Choice> _choiceDictionaryMap;
    private Dictionary<int, Letter> _letterDictionaryMap;
    private Dictionary<int, DialogueProceeding> _dialogueProceedingsMap;

    CSVParse csvParse = new CSVParse();

    public static bool isEnd = false;

    void Awake(){
        if(Instance == null){
            Instance = this;

            csvParse = GetComponent<CSVParse>();

            SetDialogueData(dialoguefilePath);
        }
    }

    // DialogueProceedings
    // Choice
    public void SetDialogueProceedingData(string pChoiceGroup)
    {
        _choiceDictionaryMap.Clear();

        Choice[] choices = csvParse.ChoiceParse(choicefilePath, pChoiceGroup);

        for (int i = 0; i < choices.Length; ++i)
        {
            _choiceDictionaryMap.Add(i + 1, choices[i]);
        }
    }

    public DialogueProceeding GetDialogueProceedingData(string currentStoryGroup)
    {
        var dialogueProceeding = new DialogueProceeding();
        
        
        
        return dialogueProceeding;
    }
    
    // Dialogue
    public void SetDialogueData(string pDialougeGroup)
    {
        _dialogueDictionaryMap.Clear();

        Dialogue[] dialogues = csvParse.DialogueParse(dialoguefilePath, pDialougeGroup);

        for (int i = 0; i < dialogues.Length; ++i)
        {
            _dialogueDictionaryMap.Add(i + 1, dialogues[i]);
        }

        isEnd = true;
    }

    public Dialogue[] GetDialogue(int pStartIndex, int pEndIndex){
        List<Dialogue> dialogueList = new List<Dialogue>();

        for (int i = 0 ; i <= pEndIndex - pStartIndex ; ++i){
            dialogueList.Add(_dialogueDictionaryMap[pStartIndex + i]);
        }

        return dialogueList.ToArray();
    }

    // Choice
    public void SetChoiceData(string pChoiceGroup)
    {
        _choiceDictionaryMap.Clear();

        Choice[] choices = csvParse.ChoiceParse(choicefilePath, pChoiceGroup);

        for (int i = 0; i < choices.Length; ++i)
        {
            _choiceDictionaryMap.Add(i + 1, choices[i]);
        }
    }

    public Choice[] GetChoice(int pStartIndex, int pEndIndex)
    {
        List<Choice> choiceList = new List<Choice>();

        for (int i = 0; i <= pEndIndex - pStartIndex; ++i)
        {
            choiceList.Add(_choiceDictionaryMap[pStartIndex + i]);
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
                    endIndex = _dialogueDictionaryMap.Count;
                }
                break;
            case DataType.Letter:
                {
                    endIndex = 0;
                }
                break;
            case DataType.Choice:
                {
                    endIndex = _choiceDictionaryMap.Count;
                }
                break;
            case DataType.DialogueProceeding:
                {
                    endIndex = 0;
                }
                break;
            default:
                {
                    endIndex = 0;
                    break;
                }
        }

        return endIndex;
    }
}
