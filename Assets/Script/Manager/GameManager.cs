using BackEnd;
using BackEnd.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using WHDle.Controller;
using WHDle.Database.SD;
using WHDle.Database.Vo;
using WHDle.Util.Define;

namespace WHDle.Util
{
    using SceneType = Define.SceneType;

    public class GameManager : Singleton<GameManager>
    {
        public TitleController TitleController;

        public LoginType loginType = LoginType.Null;

        [SerializeField]
        private VOUser boUser;
        public static VOUser User => Instance?.boUser;

        [SerializeField]
        private StaticDataModule sd = new();
        public static StaticDataModule SD => Instance?.sd;

        protected override void Awake()
        {
            base.Awake();

            if(transform.parent == null)
                DontDestroyOnLoad(gameObject);

            if (gameObject != null)
                SendQueue.StartSendQueue(true);
        }

        public void Start()
        {
            TitleController?.Initialize();
        }

        public void OnAplicationSetting()
        {
            QualitySettings.vSyncCount = 1;
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        private float loadProgress = 0;

        public void LoadScene(SceneType sceneType, IEnumerator loadCoroutine = null, Action loadComplete = null)
        {
            StartCoroutine(WaitForLoad());

            IEnumerator WaitForLoad()
            {
                loadProgress = 0;

                yield return SceneManager.LoadSceneAsync(SceneType.Loading.ToString());

                var asyncOper = SceneManager.LoadSceneAsync(sceneType.ToString(), LoadSceneMode.Additive);

                asyncOper.allowSceneActivation = false;

                if(loadCoroutine != null) { yield return StartCoroutine(loadCoroutine); }

                while (!asyncOper.isDone)
                {
                    if (loadProgress >= .9f)
                    {
                        loadProgress = 1;
                        asyncOper.allowSceneActivation = true;
                    }
                    else
                    {
                        loadProgress = asyncOper.progress;
                    }

                    yield return null;
                }

                yield return SceneManager.UnloadSceneAsync(SceneType.Loading.ToString());
                loadComplete?.Invoke();
            }
        }

        public static void Log(string str)
            => Debug.Log($"{str}");

        public static void ErrorLog(string str)
            => Debug.LogError($"{str}");
    }
}