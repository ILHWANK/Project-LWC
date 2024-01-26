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

                do{
                    contextList.Add(row[2]); // row ดย column
                    spriteList.Add(row[3]);

                    if (++i < storyData.Length){
                        row = storyData[i].Split(new char[]{','});
                    }
                    else {
                        break;
                    }
                }
                while(row[0].ToString() == "");

                story.contexts = contextList.ToArray();
                story.spriteName = spriteList.ToArray();

                storyList.Add(story);
            }
        }

        return storyList.ToArray();
    }
}
