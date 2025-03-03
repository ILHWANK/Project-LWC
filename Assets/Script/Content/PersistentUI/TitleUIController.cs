using UnityEngine;
using UnityEngine.UI;

namespace Script.Content.PersistentUI
{
    public class TitleUIController : MonoBehaviour
    {
        public static TitleUIController Instance;

        [SerializeField] private GameObject titleUI;

        [SerializeField] private Button newGameButton;

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
        }

        #region Handler

        public void ShowTitle()
        {
            titleUI.SetActive(true);
        }

        #endregion

        #region Event

        private void OnNewGameButtonClicked()
        {
            titleUI.SetActive(false);
        }

        #endregion
    }
}