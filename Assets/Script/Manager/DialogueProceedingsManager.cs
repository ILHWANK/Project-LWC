using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueProceedingsManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro _proceedingText;

    private int _day;
    private List<string> _routineList = new();
    private List<string> _inventoryList = new();
    private string _currentStoryGroup;
    private string _nextStoryGroup;
    
    public void UpdateProceeding(string currentStoryGroup)
    {
        var dialogueProceeding =  CSVDataManager.Instance.GetDialogueProceedingData(currentStoryGroup);
        
        var playerData = new PlayerData(_day, _routineList, _currentStoryGroup, _nextStoryGroup, _inventoryList);
        
        SaveDataManager.FileSave(playerData, "PlayerData");
    }

    private void UpdateProceeding()
    {
        var playerData = SaveDataManager.FileLoad("PlayerData");

        _proceedingText.text = playerData.CurrentStoryGroup;
    }
}
