using System.Collections.Generic;
using UnityEngine;
using System;
using WHDle.Util;

namespace script.Common
{
    public class UIManager : MonoBehaviour
    {
        private Stack<UIPopup> openPopups = new Stack<UIPopup>();
        private PriorityQueue<UIPopup> pendingPopups = new PriorityQueue<UIPopup>();
        
        public static event Action<UIPopup> OnPopupOpened;
        public static event Action<UIPopup> OnPopupClosed;

        private static UIManager _instance;

        public static UIManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("UIManager가 씬에 존재하지 않습니다.");
                }

                return _instance;
            }
        }

        private void Awake()
        {
            GameManager.Instance.RegisterManager(this); // GameManager에 자신을 등록
            
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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseLastOpenedPopup();
            }
        }

        public void OpenPopup(UIPopup popup)
        {
            if (!popup) 
                return;
            
            UIUtilities.SetUIActive(popup.gameObject, true);
            openPopups.Push(popup);
            
            OnPopupOpened?.Invoke(popup);
        }

        public void ClosePopup(UIPopup popup)
        {
            if (popup != null && openPopups.Contains(popup))
            {
                UIUtilities.SetUIActive(popup.gameObject, false);
                openPopups.Pop();

                OnPopupClosed?.Invoke(popup);
                
                if (pendingPopups.Count > 0)
                {
                    OpenPopup(pendingPopups.Dequeue());
                }
            }
        }

        public void CloseLastOpenedPopup()
        {
            if (openPopups.Count > 0)
            {
                ClosePopup(openPopups.Peek());
            }
        }

        public void CloseAllOpenPopups()
        {
            while (openPopups.Count > 0)
            {
                ClosePopup(openPopups.Peek());
            }
        }

        public void ReservePopup(UIPopup popup, int priority = 0)
        {
            if (popup != null)
            {
                pendingPopups.Enqueue(popup, priority); // 우선순위
            }
        }
    }
}
