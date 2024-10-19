using System.Collections;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    [SerializeField] float fadeSpeed;

    bool CheckSameSprite(SpriteRenderer pSpriteRenderer, Sprite pSprite)
    {
        return pSpriteRenderer.sprite == pSprite;
    }

    public IEnumerator SpriteChangeCoroutine(Transform pTarget, string pSpriteName)
    {
        var targetSpriteRenderer = pTarget.GetComponent<SpriteRenderer>();

        var resourceName = "Characters/Sprites/" + pSpriteName;

        var targetSprite = Resources.Load<Sprite>(resourceName);
        
        var targetColor = targetSpriteRenderer.color;
        
        if (!targetSprite)
        {
            targetColor.a = 0;
            
            targetSpriteRenderer.color = targetColor;
        }
        else
        {
            if (CheckSameSprite(targetSpriteRenderer, targetSprite)) 
                yield break;
        
            targetColor.a = 100;
        
            targetSpriteRenderer.color = targetColor;
            targetSpriteRenderer.sprite = targetSprite;

            yield return null;
        }
    }
}
