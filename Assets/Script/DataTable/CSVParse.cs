using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        
        var routine1List = new List<string>();
        if (routine1List == null) throw new ArgumentNullException(nameof(routine1List));
            
        var routine2List = new List<string>();
        if (routine2List == null) throw new ArgumentNullException(nameof(routine2List));
            
        var routine3List = new List<string>();
        if (routine3List == null) throw new ArgumentNullException(nameof(routine3List));
            
        var routine4List = new List<string>();
        if (routine4List == null) throw new ArgumentNullException(nameof(routine4List));
            
        var routine5List = new List<string>();
        if (routine5List == null) throw new ArgumentNullException(nameof(routine5List));
            
        var routine6List = new List<string>();
        if (routine6List == null) throw new ArgumentNullException(nameof(routine6List));
            
        var routine7List = new List<string>();
        if (routine7List == null) throw new ArgumentNullException(nameof(routine7List));
            
        var routine8List = new List<string>();
        if (routine8List == null) throw new ArgumentNullException(nameof(routine8List));
            
        var routine9List = new List<string>();
        if (routine9List == null) throw new ArgumentNullException(nameof(routine9List));
            
        var routine10List = new List<string>();
        if (routine10List == null) throw new ArgumentNullException(nameof(routine10List));
        
        for (var i = 1; i < dialogueData.Length;)
        {
            var row = dialogueData[i].Split(new [] { ',' });

            do
            {
                if (row[2] != "") routine1List.Add(row[2]);
                if (row[3] != "") routine2List.Add(row[3]);
                if (row[4] != "") routine3List.Add(row[4]);
                if (row[5] != "") routine4List.Add(row[5]);
                if (row[6] != "") routine5List.Add(row[6]);
                if (row[7] != "") routine6List.Add(row[7]);
                if (row[8] != "") routine7List.Add(row[8]);
                if (row[9] != "") routine8List.Add(row[9]);
                if (row[10] != "") routine9List.Add(row[10]);
                if (row[11] != "") routine10List.Add(row[11]);
            }
            while (row[1] == dayGroup);
        }

        var dayRoutine = new DayRoutine
        {
            routine1List = routine1List,
            routine2List = routine2List,
            routine3List = routine3List,
            routine4List = routine4List,
            routine5List = routine5List,
            routine6List = routine6List,
            routine7List = routine7List,
            routine8List = routine8List,
            routine9List = routine9List,
            routine10List = routine10List
        };
        
        dayRoutineList.Add(dayRoutine);

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
        var dialogueList = new List<Dialogue>();
        var dialogueCsvData = Resources.Load<TextAsset>(dialouge_File);

        if (dialogueCsvData == null) return dialogueList.ToArray();
        var dialogueData = dialogueCsvData.text.Remove(dialogueCsvData.text.Length - 1, 1).Split(new char[] { '\n' });

        for (int i = 1; i < dialogueData.Length;)
        {
            var row = dialogueData[i].Split(new [] { ',' });

            if (row[1] == pDialogueGorup)
            {
                var dialogue = new Dialogue();
                dialogue.contextName = row[3];
                dialogue.dialogueType = GetDialogueType(row[8]);
                dialogue.skipContext = row[9].Replace("\\n", "\n");

                var contextList = new List<string>();
                var cameraActionList = new List<CameraType>();
                var spriteList = new List<string>();

                do
                {
                    contextList.Add(row[4].Replace("\\n", "\n")); // column
                    spriteList.Add(row[5]);
                    cameraActionList.Add(GetCameraType(row[6]));

                    if (row[7] != "")
                        dialogue.choiceGroup = row[7];

                    //dialogueTypeList.Add(GetDialogueType(row[8]));

                    if (++i < dialogueData.Length)
                    {
                        row = dialogueData[i].Split(new[] { ',' });
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
                        row = dialogueData[i].Split(new [] { ',' });
                    }
                    else
                    {
                        break;
                    }
                }
                while (row[2] == "");
            }
        }

        return dialogueList.ToArray();
    }

    public Letter[] LetterParse(string letter_File, string pLetterGroup)
    {
        var letterList = new List<Letter>();
        var letterData = Resources.Load<TextAsset>(letter_File);

        if (letterData == null) 
            return letterList.ToArray();
        
        var dialogueData = letterData.text.Remove(letterData.text.Length - 1, 1).Split(new [] { '\n' });

        for (var i = 1; i < dialogueData.Length;)
        {
            var row = dialogueData[i].Split(new [] { ',' });

            if (row[1] == tempDialogueGroup)
            {

            }
            else
            {
                do
                {
                    if (++i < dialogueData.Length)
                    {
                        row = dialogueData[i].Split(new [] { ',' });
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
        var choiceList = new List<Choice>();
        var choiceData = Resources.Load<TextAsset>(choice_File);

        if (!choiceData) 
            return choiceList.ToArray();
        
        var dialogueData = choiceData.text.Remove(choiceData.text.Length - 1, 1).Split(new [] { '\n' });

        for (var i = 1; i < dialogueData.Length; ++i)
        {
            var row = dialogueData[i].Split(new [] { ',' });

            if (row[1] != pChoiceGroup) continue;
            var choice = new Choice
            {
                context = row[3],
                item = row[4],
                dialogueGroup = row[5],
                likeabilityWorld = row[6],
                likeabilityValue = row[7]
            };

            choiceList.Add(choice);
        }

        return choiceList.ToArray();
    }

    CameraType GetCameraType(string pCameraType)
    {
        var cameraType = pCameraType switch
        {
            "FadeIn" => CameraType.FadeIn,
            "FadeOut" => CameraType.FadeOut,
            "FlashIn" => CameraType.FlashIn,
            "FlashOut" => CameraType.FlashOut,
            _ => CameraType.None
        };

        return cameraType;
    }

    DialogueType GetDialogueType(string pDialogueType)
    {
        var dialogueType = pDialogueType switch
        {
            "ContextUp" => DialogueType.ContextUp,
            "ContextDown" => DialogueType.ContextDown,
            "Narration" => DialogueType.Narration,
            "Letter" => DialogueType.Letter,
            _ => DialogueType.None
        };

        return dialogueType;
    }
}
