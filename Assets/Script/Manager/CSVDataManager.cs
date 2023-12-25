using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVDataManager : MonoBehaviour
{
    public static CSVDataManager Instance;

    [SerializeField] string filePath;

    Dictionary<int, Story> storyDictionary = new Dictionary<int, Story>();

    public static bool isEnd = false;

    void Awake(){
        if(Instance == null){
            Instance = this;
            CSVParse csvParse = GetComponent<CSVParse>();
            Story[] storys = csvParse.Parse(filePath);

            for (int i = 0 ; i < storys.Length ; ++i){
                storyDictionary.Add(i + 1, storys[i]);
            }

            isEnd = true;
        }
    }

    public int GetEndIndex(){
        return storyDictionary.Count;
    }

    public Story[] GetStory(int pStartIndex, int pEndIndex){
        List<Story> storyList = new List<Story>();

        for (int i = 0 ; i <= pEndIndex - pStartIndex ; ++i){
            storyList.Add(storyDictionary[pStartIndex + i]);
        }

        return storyList.ToArray();
    }
}
