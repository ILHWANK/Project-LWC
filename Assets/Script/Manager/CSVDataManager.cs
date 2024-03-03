using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVDataManager : MonoBehaviour
{
    public static CSVDataManager Instance;

    [SerializeField]
    string dialoguefilePath, letterfilePath, choicefilePath;

    Dictionary<int, Dialogue> dialogueDictionary = new Dictionary<int, Dialogue>();

    public static bool isEnd = false;

    void Awake(){
        if(Instance == null){
            Instance = this;
            CSVParse csvParse = GetComponent<CSVParse>();
            Dialogue[] dialogues = csvParse.DialogueParse(dialoguefilePath);

            for (int i = 0 ; i < dialogues.Length ; ++i){
                dialogueDictionary.Add(i + 1, dialogues[i]);
            }

            isEnd = true;
        }
    }

    public int GetStartIndex()
    {
        return 0;
    }

    public int GetEndIndex(){
        return dialogueDictionary.Count;
    }

    public Dialogue[] GetDialogue(int pStartIndex, int pEndIndex){
        List<Dialogue> dialogueList = new List<Dialogue>();

        for (int i = 0 ; i <= pEndIndex - pStartIndex ; ++i){
            dialogueList.Add(dialogueDictionary[pStartIndex + i]);
        }

        return dialogueList.ToArray();
    }
}
