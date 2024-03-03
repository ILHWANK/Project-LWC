using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVParse : MonoBehaviour
{
    [SerializeField]
    string tempDialogueGroup;
    
    public Dialogue[] DialogueParse(string dialouge_File)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();
        TextAsset dialogueCsvData = Resources.Load<TextAsset>(dialouge_File);

        if (dialogueCsvData != null)
        {
            string[] dialogueData = dialogueCsvData.text.Remove(dialogueCsvData.text.Length - 1, 1).Split(new char[] { '\n' });

            for (int i = 1; i < dialogueData.Length;)
            {
                string[] row = dialogueData[i].Split(new char[] { ',' });

                if (row[1].ToString() == tempDialogueGroup)
                {
                    Dialogue dialogue = new Dialogue();
                    dialogue.characterName = row[3];

                    List<string> contextList = new List<string>();
                    List<CameraType> cameraActionList = new List<CameraType>();
                    List<string> spriteList = new List<string>();
                    List<DialogueType> dialogueTypeList = new List<DialogueType>();

                    do
                    {
                        contextList.Add(row[4]); // column
                        spriteList.Add(row[5]);
                        cameraActionList.Add(GetCameraType(row[6]));

                        if (row[7] != "")
                            dialogue.choiceGroup = row[7].ToString();

                        dialogueTypeList.Add(GetDialogueType(row[8]));

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

    public void LetterParse(string letter_File)
    {

    }

    public void ChoiceParse(string choice_File)
    {

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

        string cameraTypeString = pDialogueType.ToString();

        switch (cameraTypeString)
        {
            case "None":
                {
                    dialogueType = DialogueType.None;

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
