using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class SpriteManager : MonoBehaviour
{
    [SerializeField] float fadeSpeed;

    bool CheckSameSprite(Image pSpriteRenderer, Sprite pSprite)
    {
        return pSpriteRenderer.sprite == pSprite;
    }

    public IEnumerator SpriteChangeCoroutine(Transform pTarget, string pSpriteName)
    {
        var targetSpriteRenderer = pTarget.GetComponent<Image>();
        if (targetSpriteRenderer == null || pSpriteName == "")
            yield break;

        var addressablePath = $"Assets/Addressables/Sprites/Characters/Dialogue/{pSpriteName}.png";

        var handle = Addressables.LoadAssetAsync<Sprite>(addressablePath);
        yield return handle;

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            SetSpriteAlpha(targetSpriteRenderer, 0);
            yield break;
        }

        var targetSprite = handle.Result;

        if (CheckSameSprite(targetSpriteRenderer, targetSprite))
            yield break;

        SetSpriteAlpha(targetSpriteRenderer, 1);
        targetSpriteRenderer.sprite = targetSprite;
    }

    private void SetSpriteAlpha(Image spriteRenderer, float alpha)
    {
        var color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }

}
