using System;
using UnityEngine;
using System.Collections.Generic;
using script.Common;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Transform popupContainer;
    
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

    public UIPopup CreatePopup(GameObject popupPrefab)
    {
        if (!popupPrefab)
        {
            Debug.LogError("Popup 프리팹이 설정되지 않았습니다.");
            return null;
        }

        UIPopup popupInstance = Instantiate(popupPrefab, popupContainer).GetComponent<UIPopup>();
        
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
}