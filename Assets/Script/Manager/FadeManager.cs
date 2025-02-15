using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Manager
{
    public class FadeManager : MonoBehaviour
    {
        public static FadeManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public UniTask FadeInImage(Image image, float duration, Action onComplete = null)
        {
            return FadeImage(image, 1f, duration, onComplete);
        }

        public UniTask FadeOutImage(Image image, float duration, Action onComplete = null)
        {
            return FadeImage(image, 0f, duration, onComplete);
        }
    
        public UniTask FadeInCanvas(CanvasGroup canvasGroup, float duration, Action onComplete = null)
        {
            return FadeCanvas(canvasGroup, 1f, duration, onComplete);
        }

        public UniTask FadeOutCanvas(CanvasGroup canvasGroup, float duration, Action onComplete = null)
        {
            return FadeCanvas(canvasGroup, 0f, duration, onComplete);
        }
        
        private async UniTask FadeImage(Image image, float targetAlpha, float duration, Action onComplete = null)
        {
            if (image == null) return;

            Color color = image.color;
            float startAlpha = color.a;
            float time = 0f;

            while (time < duration)
            {
                time += Time.deltaTime;
                color.a = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
                image.color = color;
                await UniTask.Yield();
            }

            color.a = targetAlpha;
            image.color = color;

            onComplete?.Invoke();
        }

        private async UniTask FadeCanvas(CanvasGroup canvasGroup, float targetAlpha, float duration, Action onComplete = null)
        {
            if (canvasGroup == null) return;

            float startAlpha = canvasGroup.alpha;
            float time = 0f;

            while (time < duration)
            {
                time += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
                await UniTask.Yield();
            }

            canvasGroup.alpha = targetAlpha;

            onComplete?.Invoke();
        }
    }
}
