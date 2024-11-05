using System;
using System.Collections.Generic;
using script.Common;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Transform _container;

    private Stack<UIPanel> openPanels = new Stack<UIPanel>();
    private Stack<UIPopup> openPopups = new Stack<UIPopup>();

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

    public UIPanel CreatePanel(UIPanel panelPrefab)
    {
        if (!panelPrefab) return null;

        UIPanel newPanel = Instantiate(panelPrefab, _container);
        newPanel.Open();
        
        openPanels.Push(newPanel);

        return newPanel;
    }

    public void ClosePanel(UIPanel panel)
    {
        if (openPanels.Count > 0 && openPanels.Peek() == panel)
        {
            var currentPanel = openPanels.Pop();
            currentPanel.OnCloseAnimationFinished();
            Destroy(currentPanel.gameObject);
        }
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

    public UIPopup CreatePopup(UIPopup popupPrefab)
    {
        if (!popupPrefab)
        {
            Debug.LogError("Popup 프리팹이 설정되지 않았습니다.");
            return null;
        }

        // UIPopup popupInstance = Instantiate(popupPrefab, _container).GetComponent<UIPopup>();
        UIPopup popupInstance = Instantiate(popupPrefab, _container);

        OpenPopup(popupInstance);

        return popupInstance;
    }

    public void OpenPopup(UIPopup popup)
    {
        if (popup == null)
            return;

        openPopups.Push(popup);
        popup.gameObject.SetActive(true);

        OnPopupOpened?.Invoke(popup);
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