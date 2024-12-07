using script.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemInfoPopup : UIPopup
{
    public struct ItemData
    {
        public string ItemId;
    }

    private ItemData _itemData;
    
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemDescription;

    [SerializeField] private Button _interactionButton;
    [SerializeField] private Button _closeButton;

    private void Start()
    {
        _interactionButton.onClick.AddListener(OnInteractionButtonClicked);
        _closeButton.onClick.AddListener(OnCloseButtonClicked);
     
        //
        var itemTable = CSVDialogueParser.LoadDialogueTable("Assets/Resources/DataTable/ItemTable.csv");

        var itemData = itemTable.GetByColumn("Item_Id", _itemData.ItemId);

        if (itemData != null)
        {
            itemName.text = itemData["Item_Name"];
            itemDescription.text = itemData["Item_Desc"];
        }
    }

    #region Event

    private void OnInteractionButtonClicked()
    {
        Debug.Log("Item 사용");
        
        UIManager.Instance.ClosePopup("InventoryItemInfoPopup");
        UIManager.Instance.OpenPopup("ResultPopup"); 
    }

    private void OnCloseButtonClicked()
    {
        UIManager.Instance.ClosePopup("InventoryItemInfoPopup");
    }

    #endregion
}