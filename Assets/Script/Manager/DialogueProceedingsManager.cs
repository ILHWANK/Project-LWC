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
        var dialogueProceeding =  CSVDataManager.Instance.GetDialogueProceedingData(currentStoryGroup); ;
    }

    private void UpdateProceeding()
    {
        
    }
}
