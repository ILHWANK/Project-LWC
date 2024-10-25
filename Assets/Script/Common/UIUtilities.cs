using UnityEngine;

namespace script.Common
{
    public static class UIUtilities
    {
        public static void SetUIActive(GameObject uiObject, bool isActive)
        {
            if (uiObject)
            {
                uiObject.SetActive(isActive);
            }
        }

        public static void FadeIn(CanvasGroup canvasGroup, float duration)
        {
            if (canvasGroup == null)
                return;

            //canvasGroup.alpha = 0;
            //canvasGroup.LeanAlpha(1, duration); // LeanTween이나 DOTween과 같은 Tween 라이브러리 사용
        }

        public static void FadeOut(CanvasGroup canvasGroup, float duration)
        {
            if (canvasGroup == null)
                return;

            //canvasGroup.LeanAlpha(0, duration).setOnComplete(() => canvasGroup.gameObject.SetActive(false));
        }
    }
}