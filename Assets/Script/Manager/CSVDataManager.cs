using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[RequireComponent(typeof(CSVParse))]
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
    [SerializeField] private string dialogueProceedingsPath;
    [SerializeField] private string dayRoutinePath;
    
    private Dictionary<int, Dialogue> _dialogueDictionaryMap = new Dictionary<int, Dialogue>();
    private Dictionary<int, Choice> _choiceDictionaryMap = new Dictionary<int, Choice>();
    private Dictionary<int, Letter> _letterDictionaryMap = new Dictionary<int, Letter>();
    private Dictionary<int, DialogueProceeding> _dialogueProceedingsMap = new Dictionary<int, DialogueProceeding>();
    private Dictionary<int, DayRoutine> _dayRoutineMap = new Dictionary<int, DayRoutine>();
    
    CSVParse _csvParse;

    public static bool isEnd = false;

    void Awake(){
        if(Instance == null){
            Instance = this;

            _csvParse = GetComponent<CSVParse>();

            SetDialogueData(dialoguefilePath);
        }
    }
    
    // DayRoutine
    public void SetDayRoutine(string routineGroup)
    {
        _dayRoutineMap.Clear();

        var dayRoutineList = _csvParse.DayRoutineParse(dayRoutinePath, routineGroup);

        for (var i = 0; i < dayRoutineList.Length; ++i)
        {
            _dayRoutineMap.Add(i + 1, dayRoutineList[i]);
        }
    }

    public DayRoutine GetDayRoutine(string dayRoutineGroup)
    {
        var dayRoutineList = _csvParse.DayRoutineParse(dayRoutinePath, dayRoutineGroup);

        var dayRoutine = dayRoutineList[0];

        return dayRoutine;
    }
    
    // DialogueProceedings
    public void SetDialogueProceedingData(string pChoiceGroup)
    {
        _dialogueProceedingsMap.Clear();

        var dialogueProceedings 
            = _csvParse.DialogueProceedingsParse(choicefilePath, pChoiceGroup);

        for (int i = 0; i < dialogueProceedings.Length; ++i)
        {
            _dialogueProceedingsMap.Add(i + 1, dialogueProceedings[i]);
        }
    }

    public DialogueProceeding GetDialogueProceedingData(string currentStoryGroup)
    {
        var dialogueProceedingList 
            = _csvParse.DialogueProceedingsParse(dialogueProceedingsPath, currentStoryGroup);
        
        var dialogueProceeding = dialogueProceedingList[0];
        
        return dialogueProceeding;
    }
    
    // Dialogue
    public void SetDialogueData(string pDialougeGroup)
    {
        _dialogueDictionaryMap.Clear();

        Dialogue[] dialogues = _csvParse.DialogueParse(dialoguefilePath, pDialougeGroup);

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

        Choice[] choices = _csvParse.ChoiceParse(choicefilePath, pChoiceGroup);

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
