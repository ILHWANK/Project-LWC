using Script.Core.UI;
using Script.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : UIPanel
{
    [SerializeField] private TextMeshProUGUI _dayChangeText;
    [SerializeField] private TextMeshProUGUI _contextText;
    
    [SerializeField] private Button _closeButton;

    public void Start()
    {
        AddListener();
    }

    private void AddListener()
    {
        _closeButton.onClick.AddListener(OnCloseButtonClicked);
    }

    private void OnExit()
    {
        UIManager.Instance.ClosePanel(this);
        Debug.Log(this);
    }

    #region Event

    private void OnCloseButtonClicked()
    {
        OnExit();
    }    

    #endregion
}