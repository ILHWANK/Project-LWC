using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVParse : MonoBehaviour
{
    [SerializeField] private string tempDialogueGroup;

    public DayRoutine[] DayRoutineParse(string dayRoutine_File, string dayGroup)
    {
        var dayRoutineList = new List<DayRoutine>();
        var dayRoutineCsvData = Resources.Load<TextAsset>(dayRoutine_File);

        if (dayRoutineCsvData == null) 
            return dayRoutineList.ToArray();
        
        var dialogueData = dayRoutineCsvData.text.Remove(dayRoutineCsvData.text.Length - 1, 1).Split(new char[] { '\n' });

        for (int i = 1; i < dialogueData.Length;)
        {
            var row = dialogueData[i].Split(new char[] { ',' });

            if (row[1] == dayGroup)
            {
                var dayRoutine = new DayRoutine();
                var routine1List = new List<string>();
                var routine2List = new List<string>();
                var routine3List = new List<string>();
                var routine4List = new List<string>();
                var routine5List = new List<string>();
                var routine6List = new List<string>();
                var routine7List = new List<string>();
                var routine8List = new List<string>();
                var routine9List = new List<string>();
                var routine10List = new List<string>();
                
                dayRoutineList.Add(dayRoutine);
            }
            else
            {
                do
                {
                    if (++i < dialogueData.Length)
                    {
                        row = dialogueData[i].Split(new char[] { ',' });
                    }
                    else
                    {
                        break;
                    }
                }
                while (row[2] == "");
            }
        }

        return dayRoutineList.ToArray();
    }
    
    public DialogueProceeding[] DialogueProceedingsParse(string dialougeProceeding_File, string currentStoryGroup)
    {
        List<DialogueProceeding> dialogueProceedingsList = new List<DialogueProceeding>();
        TextAsset dialogueProceedingCsvData = Resources.Load<TextAsset>(dialougeProceeding_File);

        if (dialogueProceedingCsvData != null)
        {
            string[] dialogueData = dialogueProceedingCsvData.text.Remove(dialogueProceedingCsvData.text.Length - 1, 1).Split(new char[] { '\n' });

            for (int i = 1; i < dialogueData.Length; ++i)
            {
                var row = dialogueData[i].Split(new[] { ',' });

                DialogueProceeding dialogueProceeding = new DialogueProceeding
                {
                    currentStoryGroup = row[1],
                    dialogueContext = row[2],
                    nextDialogue = row[3],
                    witchSpot = row[4],
                    ramSpot = row[5],
                    trigger = row[6]
                };

                dialogueProceedingsList.Add(dialogueProceeding);
            }
        }

        return dialogueProceedingsList.ToArray();
    }
    
    public Dialogue[] DialogueParse(string dialouge_File, string pDialogueGorup)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();
        TextAsset dialogueCsvData = Resources.Load<TextAsset>(dialouge_File);

        if (dialogueCsvData != null)
        {
            string[] dialogueData = dialogueCsvData.text.Remove(dialogueCsvData.text.Length - 1, 1).Split(new char[] { '\n' });

            for (int i = 1; i < dialogueData.Length;)
            {
                string[] row = dialogueData[i].Split(new char[] { ',' });

                if (row[1].ToString() == pDialogueGorup)
                {
                    Dialogue dialogue = new Dialogue();
                    dialogue.characterName = row[3];
                    dialogue.dialogueType = GetDialogueType(row[8]);
                    dialogue.skipContext = row[9].Replace("\\n", "\n");

                    List<string> contextList = new List<string>();
                    List<CameraType> cameraActionList = new List<CameraType>();
                    List<string> spriteList = new List<string>();

                    do
                    {
                        contextList.Add(row[4].Replace("\\n", "\n")); // column
                        spriteList.Add(row[5]);
                        cameraActionList.Add(GetCameraType(row[6]));

                        if (row[7] != "")
                            dialogue.choiceGroup = row[7].ToString();

                        //dialogueTypeList.Add(GetDialogueType(row[8]));

                        if (++i < dialogueData.Length)
                        {
                            row = dialogueData[i].Split(new char[] { ',' });
                        }
                        else
                        {
                            break;
                        }
                    }
                    while (row[2] == "");

                    dialogue.contexts = contextList.ToArray();
                    dialogue.spriteNames = spriteList.ToArray();
                    dialogue.cameraActions = cameraActionList.ToArray();

                    dialogueList.Add(dialogue);
                }
                else
                {
                    do
                    {
                        if (++i < dialogueData.Length)
                        {
                            row = dialogueData[i].Split(new char[] { ',' });
                        }
                        else
                        {
                            break;
                        }
                    }
                    while (row[2] == "");
                }
            }
        }

        return dialogueList.ToArray();
    }

    public Letter[] LetterParse(string letter_File, string pLetterGroup)
    {
        List<Letter> letterList = new List<Letter>();
        TextAsset letterData = Resources.Load<TextAsset>(letter_File);

        if (letterData == null) 
            return letterList.ToArray();
        
        var dialogueData = letterData.text.Remove(letterData.text.Length - 1, 1).Split(new char[] { '\n' });

        for (var i = 1; i < dialogueData.Length;)
        {
            var row = dialogueData[i].Split(new char[] { ',' });

            if (row[1].ToString() == tempDialogueGroup)
            {

            }
            else
            {
                do
                {
                    if (++i < dialogueData.Length)
                    {
                        row = dialogueData[i].Split(new char[] { ',' });
                    }
                    else
                    {
                        break;
                    }
                }
                while (row[2] == "");
            }
        }

        return letterList.ToArray();
    }

    public Choice[] ChoiceParse(string choice_File, string pChoiceGroup)
    {
        List<Choice> choiceList = new List<Choice>();
        TextAsset choiceData = Resources.Load<TextAsset>(choice_File);

        if (choiceData != null)
        {
            string[] dialogueData = choiceData.text.Remove(choiceData.text.Length - 1, 1).Split(new char[] { '\n' });

            for (int i = 1; i < dialogueData.Length; ++i)
            {
                string[] row = dialogueData[i].Split(new char[] { ',' });

                if (row[1].ToString() == pChoiceGroup)
                {
                    Choice choice = new Choice();

                    choice.context = row[3];
                    choice.item = row[4];
                    choice.dialogueGroup = row[5];
                    choice.likeabilityWorld = row[6];
                    choice.likeabilityValue = row[7];

                    choiceList.Add(choice);
                }
            }
        }

        return choiceList.ToArray();
    }

    CameraType GetCameraType(string pCameraType)
    {
        CameraType cameraType;

        string cameraTypeString = pCameraType.ToString();

        switch (cameraTypeString)
        {
            case "FadeIn":
                {
                    cameraType = CameraType.FadeIn;

                    break;
                }
            case "FadeOut":
                {
                    cameraType = CameraType.FadeOut;

                    break;
                }
            case "FlashIn":
                {
                    cameraType = CameraType.FlashIn;

                    break;
                }
            case "FlashOut":
                {
                    cameraType = CameraType.FlashOut;

                    break;
                }
            default:
                {
                    cameraType = CameraType.None;

                    break;
                }
        }

        return cameraType;
    }

    DialogueType GetDialogueType(string pDialogueType)
    {
        DialogueType dialogueType;

        string dialogueTypeString = pDialogueType.ToString();

        switch (dialogueTypeString)
        {
            case "ContextUp":
                {
                    dialogueType = DialogueType.ContextUp;

                    break;
                }
            case "ContextDown":
                {
                    dialogueType = DialogueType.ContextDown;

                    break;
                }
            case "Narration":
                {
                    dialogueType = DialogueType.Narration;

                    break;
                }

            case "Letter" :
                {
                    dialogueType = DialogueType.Letter;

                    break;
                }

            default:
                {
                    dialogueType = DialogueType.None;

                    break;
                }
        }

        return dialogueType;
    }
}
