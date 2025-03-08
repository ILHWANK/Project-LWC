using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Content.PersistentUI
{
    public class TitleUIController : MonoBehaviour
    {
        public static TitleUIController Instance;

        [SerializeField] private GameObject titleUI;

        [SerializeField] private Button newGameButton;
        [SerializeField] private Button loadGameButton;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            AddListener();
        }

        private void AddListener()
        {
            newGameButton.onClick.AddListener(OnNewGameButtonClicked);
            loadGameButton.onClick.AddListener(OnLoadGameButtonClicked);
        }

        #region Handler

        public void ShowTitle()
        {
            titleUI.SetActive(true);
        }

        private async UniTask Loading()
        {
            titleUI.SetActive(false);
            
            LoadingUIController.Instance.SetActive(true);
            
            await LoadingUIController.Instance.AddLoadingItemsByLabel("DialogueResource", "이미지 를 불러 오는 중입니다...", typeof(Sprite));
            await LoadingUIController.Instance.ShowAndLoadAsync();
            
            MainUIController.Instance.SetActive(true);
        }
        
        #endregion

        #region Event

        private void OnNewGameButtonClicked()
        {
            LoadingUIController.Instance.ReleaseAllLoadedAssets();
            
            Loading().Forget();
        }

        private void OnLoadGameButtonClicked()
        {
            Loading().Forget();
        }

        #endregion
    }
}