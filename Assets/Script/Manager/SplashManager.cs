using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SplashManager : MonoBehaviour
{
    [SerializeField]
    Image image;

    [SerializeField]
    Color whiteColor, blackColor;

    [SerializeField]
    float fadeSpeed, fadeSlowSpeed;

    public static bool isFinish = false;

    public IEnumerator FadeOut(bool pIsWhite, bool pIsSlow)
    {
        Color targetColor = pIsWhite ? whiteColor : blackColor;
        targetColor.a = 0;

        while (targetColor.a < 1)
        {
            targetColor.a += pIsSlow ? fadeSlowSpeed : fadeSpeed;
            image.color = targetColor;

            yield return null;
        }

        isFinish = true;
    }

    public IEnumerator FadeIn(bool pIsWhite, bool pIsSlow)
    {
        Color targetColor = pIsWhite ? whiteColor : blackColor;
        targetColor.a = 1;

        while (targetColor.a > 0)
        {
            targetColor.a -= pIsSlow ? fadeSlowSpeed : fadeSpeed;
            image.color = targetColor;

            yield return null;
        }

        isFinish = true;
    }
}
