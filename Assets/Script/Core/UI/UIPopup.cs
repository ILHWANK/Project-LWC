using UnityEngine;

namespace Script.Core.UI
{
    public class UIPopup : MonoBehaviour
    {
        protected object PopupData;

        public virtual void OnEnter() { }

        public void OnEnter<T>(T data)
        {
            PopupData = data;
            OnEnter();
        }

        protected virtual void OnExit() { }
    }
}