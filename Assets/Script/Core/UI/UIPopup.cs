using UnityEngine;

namespace Script.Core.UI
{
    public class UIPopup : MonoBehaviour
    {
        protected object popupData; // 전달받은 데이터 저장

        public virtual void OnEnter()
        {
            
        }

        public virtual void OnEnter<T>(T data)
        {
            popupData = data;
            OnEnter();
        }

        protected virtual void OnExit() { }
    }
}