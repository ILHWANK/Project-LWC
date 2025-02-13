using System;
using System.Collections.Generic;
using script.Common;
using UnityEngine;
using Yarn.Unity;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Transform _container;

    [SerializeField] private GameObject _dialogue;
    
    // Panel
    [SerializeField] private GameObject _loadingPanel;
    [SerializeField] private GameObject _miniGamePanel;
    [SerializeField] private GameObject _dayTransitionPanel;
    
    // Popup
    [SerializeField] private GameObject _inventoryPopup;
    [SerializeField] private GameObject _inventoryItemInfoPopup;
    [SerializeField] private GameObject _resultpopup;
    [SerializeField] private GameObject _dialoguePopup;

    private Dictionary<string, GameObject> prefabs;
    private Stack<UIPanel> openPanels = new();
    private Stack<UIPopup> openPopups = new();

    public static event Action<UIPanel> OnOpenPanel;
    public static event Action<UIPanel> OnClosePanel;
    public static event Action<UIPopup> OnPopupOpened;
    public static event Action<UIPopup> OnPopupClosed;

    private static UIManager _instance;
    public static UIManager Instance => _instance;

    private void Awake()
    {
        prefabs = new Dictionary<string, GameObject>
        {
            { "LoadingPanel", _loadingPanel},
            { "MiniGamePanel", _miniGamePanel },
            { "DayTransitionPanel", _dayTransitionPanel},
            { "InventoryPopup", _inventoryPopup },
            { "InventoryItemInfoPopup", _inventoryItemInfoPopup },
            { "ResultPopup", _resultpopup },
            { "DialoguePopup", _dialoguePopup}
        };

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
    
    public T GetPrefabComponent<T>(string prefabName) where T : Component
    {
        if (prefabs.TryGetValue(prefabName, out var prefab))
        {
            var instance = Instantiate(prefab, _container);
            var component = instance.GetComponent<T>();

            if (component == null)
            {
                Debug.LogError($"{prefabName} Prefab에 {typeof(T).Name} 컴포넌트가 없습니다.");
                Destroy(instance);
                return null;
            }

            instance.SetActive(true);
            return component;
        }

        Debug.LogError($"{prefabName} Prefab을 찾을 수 없습니다.");
        return null;
    }
    
    public void LoadSceneAsync(string sceneName, Action onComplete = null)
    {
        // var loadingPanel = _loadingPanel?.GetComponent<LoadingPanel>();
        
        // loadingPanel?.LoadSceneAsync(sceneName, onComplete);
    }

    #region UIPanel

    public void OpenPanel(string panelName)
    {
        if (prefabs.TryGetValue(panelName, out var prefab))
        {
            var panelInstance = Instantiate(prefab, _container).GetComponent<UIPanel>();
            if (panelInstance != null)
            {
                panelInstance.Open();
                openPanels.Push(panelInstance);
                OnOpenPanel?.Invoke(panelInstance);
            }
        }
        else
        {
            Debug.LogWarning($"Panel '{panelName}' not found in UIManager!");
        }
    }

    public void OpenPanel(UIPanel panel)
    {
        if (panel == null)
            return;

        var panelInstance = Instantiate(panel, _container);
        panelInstance.Open();

        openPanels.Push(panelInstance);
        OnOpenPanel?.Invoke(panelInstance);
    }

    public void ClosePanel(string panelName)
    {
        if (openPanels.Count <= 0) return;

        var panelToClose = openPanels.Peek();
        if (panelToClose != null && panelToClose.name.Contains(panelName))
        {
            ClosePanel(panelToClose);
        }
    }

    public void ClosePanel(UIPanel panel)
    {
        if (openPanels.Count <= 0 || openPanels.Peek() != panel)
            return;

        var currentPanel = openPanels.Pop();
        currentPanel.OnCloseAnimationFinished();

        Destroy(currentPanel.gameObject);
        OnClosePanel?.Invoke(currentPanel);
    }

    public void CloseAllPanels()
    {
        while (openPanels.Count > 0)
        {
            var panel = openPanels.Pop();
            panel.OnCloseAnimationFinished();

            Destroy(panel.gameObject);
            OnClosePanel?.Invoke(panel);
        }
    }

    #endregion

    #region UIPopup

    public void OpenPopup(string popupName)
    {
        if (prefabs.TryGetValue(popupName, out var prefab))
        {
            var popupInstance = Instantiate(prefab, _container).GetComponent<UIPopup>();
            if (popupInstance != null)
            {
                popupInstance.gameObject.SetActive(true);
                openPopups.Push(popupInstance);
                OnPopupOpened?.Invoke(popupInstance);
            }
        }
        else
        {
            Debug.LogWarning($"Popup '{popupName}' not found in UIManager!");
        }
    }

    public void OpenPopup(UIPopup popup)
    {
        if (popup == null)
        {
            Debug.LogError("Popup 프리팹이 설정되지 않았습니다.");
            return;
        }

        var popupInstance = Instantiate(popup, _container);
        openPopups.Push(popupInstance);
        popupInstance.gameObject.SetActive(true);

        OnPopupOpened?.Invoke(popupInstance);
    }

    public void ClosePopup(string popupName)
    {
        if (openPopups.Count <= 0) return;

        var popupToClose = openPopups.Peek();
        if (popupToClose != null && popupToClose.name.Contains(popupName))
        {
            ClosePopup(popupToClose);
        }
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

    #region Dialogue

    public void SetDialogue(bool isActive)
    {
        _dialogue.SetActive(isActive);
    }

    #endregion
}
