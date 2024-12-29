using System.Collections.Generic;
using script.Common;
using Script.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemInfoPopup : UIPopup
{
    public struct State
    {
        public string ItemId;
    }

    private State _state;
    
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemDescription;

    [SerializeField] private Button _interactionButton;
    [SerializeField] private Button _closeButton;

    private Dictionary<string, string> _itemData = new Dictionary<string, string>();

    // 추후 Manager 일괄 관리 방식 으로 변경 예정
    DialogueManager dialogueManager;
    
    private void Start()
    {
        _interactionButton.onClick.AddListener(OnInteractionButtonClicked);
        _closeButton.onClick.AddListener(OnCloseButtonClicked);
        
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    public void SetData(State state)
    {
        _state = state;

        ReFresh();
    }

    private void ReFresh()
    {
        var itemTable = CSVDialogueParser.LoadDialogueTable("Assets/Resources/DataTable/ItemTable.csv");

        _itemData = itemTable.GetByColumn("Item_Id", _state.ItemId);

        Debug.Log(_state.ItemId);
        
        if (_itemData != null)
        {
            itemName.text = _itemData["Item_Name"];
            itemDescription.text = _itemData["Item_Desc"];
        }
    }

    #region Event

    private void OnInteractionButtonClicked()
    {
        var itemType = _itemData["Item_Type"];

        if (itemType == "Letter")
        {
            UIManager.Instance.CloseAllPopups();
            
            dialogueManager.TempPlayStory("Day3_Animal"); //_itemData["Item_Id"]
        }
        else
        {
            UIManager.Instance.ClosePopup("InventoryItemInfoPopup");
            UIManager.Instance.OpenPopup("ResultPopup"); 
        }
        

    }

    private void OnCloseButtonClicked()
    {
        UIManager.Instance.ClosePopup("InventoryItemInfoPopup");
    }

    #endregion
}