using System;
using script.Common;
using UnityEngine;
using UnityEngine.UI;

public class ResultPopup : UIPopup
{
    [SerializeField] private Button _closeButton;

    private void Start()
    {
        _closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    #region Evenet

    private void OnCloseButtonClick()
    {
        UIManager.Instance.ClosePopup("ResultPopup");
    }

    #endregion

}
