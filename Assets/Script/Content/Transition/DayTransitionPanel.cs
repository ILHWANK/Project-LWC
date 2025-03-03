using Script.Core.UI;
using Script.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayTransitionPanel : UIPanel
{
    [SerializeField] private TextMeshProUGUI _dayChangeText;
    [SerializeField] private TextMeshProUGUI _contextText;

    [SerializeField] private Button _closeButton;

    public struct Data
    {
        public int CurrentDay;
    }

    private Data _data;

    public void Start()
    {
        AddListener();
    }

    public override void OnEnter()
    {
        base.OnEnter();

        if (PanelData is Data data) // 받은 데이터를 구조체로 변환
        {
            _data = data;

            var currentDay = _data.CurrentDay;
            var dayChangeText = $"{currentDay} Day => {currentDay + 1} Day";

            _dayChangeText.text = dayChangeText;
        }
        else
        {
            _dayChangeText.text = "Data 가 없습니다.";
        }
    }

    private void AddListener()
    {
        _closeButton.onClick.AddListener(OnCloseButtonClicked);
    }

    private void OnExit()
    {
        GameDataManager.Instance.GameData.CurrentDay = _data.CurrentDay + 1;

        MessageSystem.Instance.Publish("DayUpdate", GameDataManager.Instance.GameData.CurrentDay);

        UIManager.Instance.ClosePanel(this);
    }

    #region Event

    private void OnCloseButtonClicked()
    {
        OnExit();
    }

    #endregion
}