using Script.Core.UI;
using Script.Manager;
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
        UIManager.Instance.CloseLastPopup();
    }

    #endregion
}