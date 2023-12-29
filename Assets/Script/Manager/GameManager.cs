using BackEnd;
using BackEnd.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WHDle.Controller;
using WHDle.Util.Define;

namespace WHDle.Util
{
    public class GameManager : Singleton<GameManager>
    {
        public TitleController TitleController;

        public LoginType loginType = LoginType.Null;

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
    }
}