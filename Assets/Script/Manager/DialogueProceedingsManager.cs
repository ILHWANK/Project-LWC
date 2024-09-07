using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueProceedingsManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro _proceedingText;

    private int _day;
    private Dictionary<string, bool> _routineMap = new();
    private Dictionary<string, int> _inventoryMap = new();
    private string _currentStoryGroup;
    private string _nextStoryGroup;
    
    public void UpdateProceeding(string currentStoryGroup)
    {
        var dialogueProceeding =  CSVDataManager.Instance.GetDialogueProceedingData(currentStoryGroup);

        var _day = 1;
        _routineMap = new Dictionary<string, bool>();
        _currentStoryGroup = dialogueProceeding.currentStoryGroup;
        _nextStoryGroup = dialogueProceeding.nextDialogue;
        
        var playerData = new PlayerData(_day, _routineMap, _currentStoryGroup, _nextStoryGroup, _inventoryMap);
        
        SaveDataManager.FileSave(playerData, "PlayerData");
    }

    private void UpdateProceeding()
    {
        var playerData = SaveDataManager.FileLoad("PlayerData");

        _proceedingText.text = playerData.CurrentStoryGroup;
    }
}
