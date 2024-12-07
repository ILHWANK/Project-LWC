using System;
using UnityEngine;

namespace script.Common
{
    public class UIPopup : MonoBehaviour
    {
        [SerializeField] private GameObject popupCanvas;
        [SerializeField] private Animator popupAnimator;
        [SerializeField] private CanvasGroup canvasGroup;

        public Action PopupClose;
        
        public virtual void Open()
        {
            if (popupCanvas == null) 
                return;

            UIUtilities.SetUIActive(popupCanvas, true);

            if (popupAnimator != null)
            {
                popupAnimator.SetTrigger("Open");
            }

            if (canvasGroup != null)
            {
                UIUtilities.FadeIn(canvasGroup, 0.5f);
            }
        }

        public virtual void Close()
        {
            PopupClose?.Invoke();
            
            gameObject.SetActive(false);
        
            Destroy(gameObject);
        }

        public void OnCloseAnimationFinished()
        {
            UIUtilities.SetUIActive(popupCanvas, false);
        }

        public void OnClickCloseButton()
        {
            Close();
        }
    }
}