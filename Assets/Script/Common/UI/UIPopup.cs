using UnityEngine;

namespace script.Common
{
    public class UIPopup : MonoBehaviour
    {
        [SerializeField] private GameObject popupCanvas;
        [SerializeField] private Animator popupAnimator;
        [SerializeField] private CanvasGroup canvasGroup;

        public void Open()
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

        public void Close()
        {
            UIManager.Instance.ClosePopup(this);
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