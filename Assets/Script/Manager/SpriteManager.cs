using System.Collections;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    [SerializeField] float fadeSpeed;

    bool CheckSameSprite(SpriteRenderer pSpriteRenderer, Sprite pSprite)
    {
        if (pSpriteRenderer.sprite == pSprite)
            return true;
        else
            return false;
    }

    public IEnumerator SpriteChangeCoroutine(Transform pTarget, string pSpriteName)
    {
        SpriteRenderer targetSpriteRenderer = pTarget.GetComponent<SpriteRenderer>();

        string resourceName = "Characters/Sprites/" + pSpriteName.ToString();

        Sprite targetSprite = Resources.Load<Sprite>(resourceName);


        if (!CheckSameSprite(targetSpriteRenderer, targetSprite))
        {
            Color targetColor = targetSpriteRenderer.color;
            targetColor.a = 100;//0;
            targetSpriteRenderer.color = targetColor;

            targetSpriteRenderer.sprite = targetSprite;

            yield return null;

            /*
            while (targetColor.a < 1)
            {
                targetColor.a += fadeSpeed;
                targetSpriteRenderer.color = targetColor;

                yield return null;
            }
            */
        }
    }
}
