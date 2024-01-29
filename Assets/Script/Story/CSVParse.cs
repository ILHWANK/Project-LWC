using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVParse : MonoBehaviour
{
    public Story[] Parse(string CSV_FileName){
        List<Story> storyList = new List<Story>();
        TextAsset csvData = Resources.Load<TextAsset>(CSV_FileName);

        if (csvData != null){
            string[] storyData = csvData.text.Remove(csvData.text.Length -1, 1).Split(new char[] {'\n'});

            for(int i = 1 ; i < storyData.Length;){
                string[] row = storyData[i].Split(new char[]{','});

                Story story = new Story();
                story.characterName = row[1];
                
                List<string> contextList = new List<string>();
                List<string> spriteList  = new List<string>();
                List<CameraType> cameraActionList = new List<CameraType>();

                do{
                    contextList.Add(row[2]); // column
                    spriteList.Add(row[3]);
                    cameraActionList.Add(GetCameraType(row[4]));

                    if (++i < storyData.Length){
                        row = storyData[i].Split(new char[]{','});
                    }
                    else {
                        break;
                    }
                }
                while(row[0].ToString() == "");

                story.contexts = contextList.ToArray();
                story.spriteNames = spriteList.ToArray();
                story.cameraActions = cameraActionList.ToArray();

                storyList.Add(story);
            }
        }

        return storyList.ToArray();
    }

    CameraType GetCameraType(string pCameraType)
    {
        CameraType cameraType;

        switch (pCameraType.ToString())
        {
            case "FadeIn":
                {
                    cameraType = CameraType.FadeIn;

                    Debug.Log("1");

                    break;
                }
            case "FadeOut":
                {
                    cameraType = CameraType.FadeOut;

                    Debug.Log("2");
                    break;
                }
            case "FlashIn":
                {
                    cameraType = CameraType.FlashIn;

                    Debug.Log("3");
                    break;
                }
            case "FlashOut":
                {
                    cameraType = CameraType.FlashOut;

                    Debug.Log("4");
                    break;
                }
            default:
                {
                    cameraType = CameraType.None;

                    Debug.Log("5");
                    break;
                }
        }

        return cameraType;
    }
}
