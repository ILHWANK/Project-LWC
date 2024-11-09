using System;
using System.Collections.Generic;
using script.Common;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Transform _container;

    private Stack<UIPanel> openPanels = new Stack<UIPanel>();
    private Stack<UIPopup> openPopups = new Stack<UIPopup>();

    public static event Action<UIPanel> OnOpenPanel;
    public static event Action<UIPanel> OnClosePanel;
    public static event Action<UIPopup> OnPopupOpened;
    public static event Action<UIPopup> OnPopupClosed;

    private static UIManager _instance;
    public static UIManager Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    #region UIPanel

    public void  OpenPanel(UIPanel panel)
    {
        if (!panel) 
            return;

        var panelInstance = Instantiate(panel, _container);
        panelInstance.Open();
        
        openPanels.Push(panelInstance);
    }

    public void ClosePanel(UIPanel panel)
    {
        if (openPanels.Count <= 0 || openPanels.Peek() != panel) 
            return;
        
        var currentPanel = openPanels.Pop();
        currentPanel.OnCloseAnimationFinished();
        
        Destroy(currentPanel.gameObject);
    }

    public void CloseAllPanels()
    {
        while (openPanels.Count > 0)
        {
            var panel = openPanels.Pop();
            panel.OnCloseAnimationFinished();
            
            Destroy(panel.gameObject);
        }
    }

    #endregion

    #region UIPopup
    public void OpenPopup(UIPopup popup)
    {
        if (!popup)
        {
            Debug.LogError("Popup 프리팹이 설정되지 않았습니다.");
            return;
        }

        var popupInstance = Instantiate(popup, _container);

        openPopups.Push(popupInstance);
        popupInstance.gameObject.SetActive(true);

        OnPopupOpened?.Invoke(popupInstance);
    }

    public void ClosePopup(UIPopup popup)
    {
        if (popup == null || !openPopups.Contains(popup))
            return;

        openPopups.Pop();
        popup.gameObject.SetActive(false);

        OnPopupClosed?.Invoke(popup);

        Destroy(popup.gameObject);
    }

    public void CloseLastOpenedPopup()
    {
        if (openPopups.Count > 0)
        {
            ClosePopup(openPopups.Peek());
        }
    }

    #endregion
}