using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueProceedingsManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro _proceedingText;

    private int _day;
    private string _triggerType;
    private string _currentStoryGroup;
    private string _nextStoryGroup;
    private List<string> _inventoryList = new List<string>();
    
    public void UpdateProceeding(string currentStoryGroup)
    {
        var dialogueProceeding =  CSVDataManager.Instance.GetDialogueProceedingData(currentStoryGroup);
        
        var playerData = new PlayerData(_day, _triggerType, _currentStoryGroup, _nextStoryGroup, _inventoryList);
        
        SaveDataManager.FileSave(playerData, "PlayerData");
    }

    private void UpdateProceeding()
    {
        var playerData = SaveDataManager.FileLoad("PlayerData");

        _proceedingText.text = playerData.CurrentStoryGroup;
    }
}
