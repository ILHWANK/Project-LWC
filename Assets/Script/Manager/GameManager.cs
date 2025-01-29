using BackEnd;
using System;
using System.Collections;
using Script.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;
using WHDle.Controller;
using WHDle.Util.Define;

namespace WHDle.Util
{
    using SceneType = Define.SceneType;

    public class GameManager : Singleton<GameManager>
    {
        // 필요한 매니저들을 등록
        public SoundManager SoundManager { get; private set; }
        public FadeManager FadeManager { get; private set; }
        public DialogueManagerTemp DialogueManagerTemp { get; private set; }
        public UIManager UIManager { get; private set; }
        
        // 
        public LoginType loginType = LoginType.Guest;
        
        public TitleController TitleController;

        [SerializeField]
        // private StaticDataModule sd = new();
        // public static StaticDataModule SD => Instance?.sd;

        private float loadProgress = 0;

        protected override void Awake()
        {
            base.Awake();

            if (transform.parent == null)
                DontDestroyOnLoad(gameObject);

            SendQueue.StartSendQueue(true);
        }

        private void Start()
        {   
            // TitleController 초기화
            // TitleController?.Initialize();
        }
        
        // 매니저 등록 메서드
        public void RegisterManager(object manager)
        {
            switch (manager)
            {
                case SoundManager soundManager:
                    SoundManager = soundManager;
                    break;
                case FadeManager fadeManager:
                    FadeManager = fadeManager;
                    break;
                case DialogueManagerTemp dialogueManager:
                    DialogueManagerTemp = dialogueManager;
                    break;
                case UIManager uiManager:
                    UIManager = uiManager;
                    break;
                case TitleController titleController:
                    TitleController = titleController;
                    break;
                default:
                    Debug.LogError("알 수 없는 매니저 타입입니다.");
                    break;
            }
        }

        // 애플리케이션 설정
        public void OnAplicationSetting()
        {
            QualitySettings.vSyncCount = 1;
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        // 씬 로드 기능
        public void LoadScene(SceneType sceneType, IEnumerator loadCoroutine = null, Action loadComplete = null)
        {
            StartCoroutine(WaitForLoad());

            IEnumerator WaitForLoad()
            {
                loadProgress = 0;
                yield return SceneManager.LoadSceneAsync(SceneType.Loading.ToString());

                var asyncOpen = SceneManager.LoadSceneAsync(sceneType.ToString(), LoadSceneMode.Additive);
                asyncOpen.allowSceneActivation = false;

                if (loadCoroutine != null)
                    yield return StartCoroutine(loadCoroutine);

                while (!asyncOpen.isDone)
                {
                    if (loadProgress >= .9f)
                    {
                        loadProgress = 1;
                        asyncOpen.allowSceneActivation = true;
                    }
                    else
                    {
                        loadProgress = asyncOpen.progress;
                    }
                    yield return null;
                }

                yield return SceneManager.UnloadSceneAsync(SceneType.Loading.ToString());
                loadComplete?.Invoke();
            }
        }

        // 로그 메서드
        public static void Log(string str) => Debug.Log($"{str}");
        public static void ErrorLog(string str) => Debug.LogError($"{str}");
    }
}
