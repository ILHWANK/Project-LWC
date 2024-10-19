using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public IEnumerator FadeIn(Image image, float fadeSpeed)
    {
        var color = image.color;
        color.a = 1f;
        image.color = color;

        while (color.a > 0f)
        {
            color.a -= fadeSpeed * Time.deltaTime;
            image.color = color;
            yield return null;
        }

        color.a = 0f;
        image.color = color;
    }

    public IEnumerator FadeOut(Image image, float fadeSpeed)
    {
        var color = image.color;
        color.a = 0f;
        image.color = color;

        while (color.a < 1f)
        {
            color.a += fadeSpeed * Time.deltaTime;
            image.color = color;
            yield return null;
        }

        color.a = 1f;
        image.color = color;
    }
}