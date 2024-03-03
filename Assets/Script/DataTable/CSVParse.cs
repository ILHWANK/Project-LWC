using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVParse : MonoBehaviour
{
    public Dialogue[] DialogueParse(string dialouge_File)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();
        TextAsset dialogueCsvData = Resources.Load<TextAsset>(dialouge_File);

        //TextAsset dialogueData = Resources.Load<TextAsset>(dialouge_File);

        if (dialogueCsvData != null)
        {
            string[] dialogueData = dialogueCsvData.text.Remove(dialogueCsvData.text.Length - 1, 1).Split(new char[] { '\n' });

            for (int i = 1; i < dialogueData.Length;)
            {
                string[] row = dialogueData[i].Split(new char[] { ',' });

                Debug.Log("row 확인 : " + row[1]);

                if (row[1] != "Day1_Western_Option1")
                    continue;

                Dialogue story = new Dialogue();
                story.characterName = row[3];

                List<string> contextList = new List<string>();
                List<string> spriteList = new List<string>();
                List<CameraType> cameraActionList = new List<CameraType>();
                List<string> choiceList = new List<string>();
                List<DialogueType> dialogueTypeList = new List<DialogueType>();

                do
                {
                    contextList.Add(row[4]); // column
                    spriteList.Add(row[5]);
                    cameraActionList.Add(GetCameraType(row[6]));
                    choiceList.Add(row[7]);
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
                while (row[2].ToString() == "");

                story.contexts = contextList.ToArray();
                story.spriteNames = spriteList.ToArray();
                story.cameraActions = cameraActionList.ToArray();

                dialogueList.Add(story);
            }
        }

        return dialogueList.ToArray();
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
