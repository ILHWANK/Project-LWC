using UnityEngine;

namespace script.Common
{
    public class UIPanel : MonoBehaviour
    {
        [SerializeField] private GameObject panelCanvas;
        [SerializeField] private Animator panelAnimator;
        [SerializeField] private CanvasGroup canvasGroup;

        public virtual void Open()
        {
            if (panelCanvas == null)
                return;

            UIUtilities.SetUIActive(panelCanvas, true);

            if (panelAnimator != null)
            {
                panelAnimator.SetTrigger("Open");
            }

            if (canvasGroup != null)
            {
                UIUtilities.FadeIn(canvasGroup, 0.5f);
            }
        }

        public virtual void Close()
        {
            UIManager.Instance.ClosePanel(this);
        }

        public void OnCloseAnimationFinished()
        {
            UIUtilities.SetUIActive(panelCanvas, false);
        }
    }
}