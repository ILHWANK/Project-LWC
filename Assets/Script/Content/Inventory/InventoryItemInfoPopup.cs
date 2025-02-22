using Script.Core.UI;
using Script.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemInfoPopup : UIPopup
{
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemDescription;

    [SerializeField] private Button _interactionButton;
    [SerializeField] private Button _closeButton;

    public struct ItemInfoPopupData
    {
        public string ItemId;
        public int ItemCount;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        
        if (popupData is ItemInfoPopupData itemInfo) // 받은 데이터를 구조체로 변환
        {
            var itemTable = CSVDialogueParser.LoadDialogueTable("Assets/Resources/DataTable/ItemTable.csv");
            var itemData = itemTable.GetByColumn("Item_Id", itemInfo.ItemId);

            itemName.text = itemData["Item_Name"] + $" (x{itemInfo.ItemCount})";
            itemDescription.text = itemData["Item_Desc"];
        }
        else
        {
            Debug.LogWarning("InventoryItemInfoPopup: 데이터가 전달되지 않았습니다.");
        }

        _interactionButton.onClick.AddListener(OnInteractionButtonClicked);
        _closeButton.onClick.AddListener(OnCloseButtonClicked);
    }

    #region Event

    private void OnInteractionButtonClicked()
    {
        UIManager.Instance.CloseLastPopup();
        UIManager.Instance.OpenPopup("ResultPopup"); 
    }

    private void OnCloseButtonClicked()
    {
        UIManager.Instance.CloseLastPopup();
    }

    #endregion
}