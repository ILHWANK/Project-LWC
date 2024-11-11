using System;
using System.Collections;
using System.Collections.Generic;
using script.Common;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemInfoPopup : UIPopup
{
    [SerializeField] private Button _interactionButton;
    [SerializeField] private Button _closeButton; 
    
    private void Start()
    {
        _interactionButton.onClick.AddListener(OnInteractionButtonClicked);
        _closeButton.onClick.AddListener(OnCloseButtonClicked);
    }

    #region Event

    private void OnInteractionButtonClicked()
    {
        //
        Debug.Log("Item 사용");
        UIManager.Instance.ClosePopup("InventoryItemInfoPopup");
    }
    
    private void OnCloseButtonClicked()
    {
        UIManager.Instance.ClosePopup("InventoryItemInfoPopup");
    }

    #endregion
}
