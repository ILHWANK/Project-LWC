using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class UIDialogueCharacterSlot : MonoBehaviour
{
    [SerializeField] private RawImage characterImageSpot;

    public AsyncOperationHandle<Texture2D> ChangeImage(string spriteName)
    {
        var key = $"Assets/Addressables/Sprites/Characters/Dialogue/{spriteName}.png";
        var handle = Addressables.LoadAssetAsync<Texture2D>(key);

        handle.Completed += OnImageLoaded;
        return handle; // ✅ 로딩이 끝날 때까지 기다릴 수 있도록 반환
    }

    private void OnImageLoaded(AsyncOperationHandle<Texture2D> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            characterImageSpot.texture = obj.Result;
        }
        else
        {
            Debug.LogError($"이미지를 불러오는 데 실패했습니다: {obj.DebugName}");
        }
    }
}