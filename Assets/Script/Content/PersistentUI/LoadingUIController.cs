using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace Script.Content.PersistentUI
{
    public class LoadingUIController : MonoBehaviour
    {
        public static LoadingUIController Instance; // 싱글톤 인스턴스

        [Serializable]
        public class LoadingItem
        {
            public string Key;          // Addressables에서 사용할 Key
            public string Description;  // 로딩할 때 표시할 설명
            public Type AssetType;      // ✅ 로드할 리소스의 타입

            public LoadingItem(string key, string description, Type assetType)
            {
                Key = key;
                Description = description;
                AssetType = assetType;
            }
        }

        [SerializeField] private Slider progressSlider; // 로딩 진행 표시 바
        [SerializeField] private TMP_Text loadingText;  // 현재 로딩 중인 항목 표시

        private readonly List<LoadingItem> _loadingItems = new(); // 로딩할 아이템 목록
        private readonly Dictionary<string, AsyncOperationHandle> _loadedAssets = new(); // 로드된 Addressables 저장
        private bool _isLoading; // 로딩 중인지 여부 체크

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogWarning("⚠️ 중복된 LoadingUIController가 존재! 기존 인스턴스를 유지합니다.");
                Destroy(gameObject);
                return;
            }

            gameObject.SetActive(false);
        }

        /// <summary>
        /// UI 활성화/비활성화
        /// </summary>
        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
        
        public void AddLoadingItem(string key, string description, Type assetType)
        {
            _loadingItems.Add(new LoadingItem(key, description, assetType));
        }
        
        public async UniTask AddLoadingItemsByLabel(string label, string description, Type assetType)
        {
            if (_loadingItems.Count > 0)
            {
                ClearLoadingItems(); // 기존 리스트 초기화
            }

            var locationsHandle = Addressables.LoadResourceLocationsAsync(label);
            await locationsHandle.Task;

            if (locationsHandle.Status == AsyncOperationStatus.Succeeded)
            {
                foreach (var location in locationsHandle.Result)
                {
                    _loadingItems.Add(new LoadingItem(location.PrimaryKey, description, assetType));
                }
            }
            else
            {
                Debug.LogError($"❌ Addressables 로드 실패: Label [{label}]");
            }

            Addressables.Release(locationsHandle);
        }
        
        public void ClearLoadingItems()
        {
            if (_loadingItems.Count > 0)
            {
                _loadingItems.Clear();
            }
        }
        
        public async UniTask ShowAndLoadAsync()
        {
            if (_isLoading) return;

            _isLoading = true;
            gameObject.SetActive(true);
            await LoadAllItems();
            gameObject.SetActive(false);

            _isLoading = false;
        }
        
        private async UniTask LoadAllItems()
        {
            var totalCount = _loadingItems.Count;
            var currentIndex = 0;

            foreach (var item in _loadingItems)
            {
                loadingText.text = $"{item.Description} ({currentIndex + 1}/{totalCount})";

                if (!_loadedAssets.ContainsKey(item.Key))
                {
                    AsyncOperationHandle handle;

                    // ✅ AssetType에 따라 적절한 타입으로 로드
                    if (item.AssetType == typeof(GameObject))
                        handle = Addressables.LoadAssetAsync<GameObject>(item.Key);
                    else if (item.AssetType == typeof(AnimationClip))
                        handle = Addressables.LoadAssetAsync<AnimationClip>(item.Key);
                    else if (item.AssetType == typeof(AudioClip))
                        handle = Addressables.LoadAssetAsync<AudioClip>(item.Key);                   
                    else if (item.AssetType == typeof(Sprite))
                        handle = Addressables.LoadAssetAsync<Sprite>(item.Key);
                    else
                    {
                        Debug.LogError($"❌ 지원하지 않는 타입: {item.AssetType}");
                        continue;
                    }

                    await handle.Task;

                    if (handle.Status == AsyncOperationStatus.Succeeded)
                    {
                        Debug.Log($"✅ {item.Key} ({item.AssetType.Name}) 로드 완료!");
                        _loadedAssets[item.Key] = handle;
                    }
                    else
                    {
                        Debug.LogError($"❌ {item.Key} 로드 실패!");
                    }
                }

                currentIndex++;
                progressSlider.value = (float)currentIndex / totalCount;
            }
        }
        
        public void ReleaseAllLoadedAssets()
        {
            if (_loadedAssets.Count == 0) return;

            foreach (var kvp in _loadedAssets)
            {
                Debug.Log($"🔻 {kvp.Key} 메모리에서 해제");
                Addressables.Release(kvp.Value);
            }

            _loadedAssets.Clear();
        }

        private void OnDestroy()
        {
            ReleaseAllLoadedAssets();
        }
    }
}
