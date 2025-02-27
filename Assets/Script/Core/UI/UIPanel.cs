using UnityEngine;

namespace Script.Core.UI
{
    public class UIPanel : MonoBehaviour
    {
        protected object PanelData;

        public virtual void OnEnter()
        {
        }

        public void OnEnter<T>(T data)
        {
            PanelData = data;
            OnEnter();
        }

        protected virtual void OnExit()
        {
        }
    }
}