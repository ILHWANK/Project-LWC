using System.Collections.Generic;
using UnityEngine;

namespace script.Common
{
    public class UIManager : MonoBehaviour
    {
        private Stack<UIPopup> openPopups = new Stack<UIPopup>();
        private Queue<UIPopup> pendingPopups = new Queue<UIPopup>();
        
        private static UIManager _instance;

        public static UIManager Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                _instance = FindObjectOfType<UIManager>();

                if (_instance == null)
                {
                    Debug.LogError("UIManager가 씬에 존재하지 않습니다.");
                }

                return _instance;
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
        }

        public void ClosePopup(UIPopup popup)
        {
            if (popup != null && openPopups.Contains(popup))
            {
                UIUtilities.SetUIActive(popup.gameObject, false);
                openPopups.Pop();

                // 예약된 팝업이 있은 경우
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

        // 예약된 팝업을 추가
        public void ReservePopup(UIPopup popup)
        {
            if (popup != null)
            {
                pendingPopups.Enqueue(popup);
            }
        }
    }
}