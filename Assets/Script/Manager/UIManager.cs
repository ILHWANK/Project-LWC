using System;
using System.Collections.Generic;
using System.Linq;
using Script.Core.UI;
using UnityEngine;

namespace Script.Manager
{
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

        #region UIPanel

        public void OpenPanel(string panelName)
        {
            if (prefabs.TryGetValue(panelName, out var prefab))
            {
                var panelInstance = Instantiate(prefab, _container).GetComponent<UIPanel>();
                if (panelInstance != null)
                {
                    panelInstance.OnEnter();
                    openPanels.Push(panelInstance);
                    OnOpenPanel?.Invoke(panelInstance);
                }
            }
            else
            {
                Debug.LogWarning($"Panel '{panelName}' not found in UIManager!");
            }
        }
        
        public void OpenPanel<T>(string panelName, T data)
        {
            if (prefabs.TryGetValue(panelName, out var prefab))
            {
                var panelInstance = Instantiate(prefab, _container).GetComponent<UIPanel>();
                if (panelInstance != null)
                {
                    panelInstance.OnEnter(data);
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
            panelInstance.OnEnter();

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

            Destroy(currentPanel.gameObject);
            OnClosePanel?.Invoke(currentPanel);
        }

        public void CloseAllPanels()
        {
            while (openPanels.Count > 0)
            {
                var panel = openPanels.Pop();

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
                    popupInstance.OnEnter();
                    popupInstance.gameObject.SetActive(true);
                    openPopups.Push(popupInstance);
                }
            }
            else
            {
                Debug.LogWarning($"Popup '{popupName}' not found in UIManager!");
            }
        }
        
        public void OpenPopup<T>(string popupName, T data)
        {
            if (prefabs.TryGetValue(popupName, out var popupPrefab))
            {
                var popupInstance = Instantiate(popupPrefab, _container).GetComponent<UIPopup>();
                popupInstance.OnEnter(data);
                popupInstance.gameObject.SetActive(true);
                openPopups.Push(popupInstance);
            }
            else
            {
                Debug.LogWarning($"Popup '{popupName}' not found in UIManager!");
            }
        }

        public void ClosePopup(string popupName)// 이름 대신 다른 방식으로 찾아서 지우는 방법 으로 수정 필요
        {
            if (openPopups.Count == 0) return;

            var popupToClose = openPopups.FirstOrDefault(popup => popup.name == popupName);
            if (popupToClose == null) return;

            openPopups = new Stack<UIPopup>(openPopups.Where(popup => popup != popupToClose));

            popupToClose.gameObject.SetActive(false);
            OnPopupClosed?.Invoke(popupToClose);
            Destroy(popupToClose.gameObject);
        }

        public void CloseLastPopup()
        {
            if (openPopups.Count == 0) return;

            var popupToClose = openPopups.Pop();
            popupToClose.gameObject.SetActive(false);
            OnPopupClosed?.Invoke(popupToClose);
            Destroy(popupToClose.gameObject);
        }
        
        #endregion

        #region Dialogue

        public void SetDialogue(bool isActive)
        {
            _dialogue.SetActive(isActive);
        }

        #endregion
    }
}
