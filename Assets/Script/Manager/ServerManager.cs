using WHDle.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using GooglePlayGames.BasicApi;
using GooglePlayGames;

namespace WHDle.Server
{
    public class ServerManager : Singleton<ServerManager>
    {
        // 처음 로그인 하는지 확인하는 변수
        public bool isFirstLogin = true;

        // Server에서 받아온 User 정보입니다.
        // public DtoUserInfo userInfo;

        protected override void Awake()
        {
            base.Awake();

            // Send Queue 방식을 사용하기 위한 방식입니다.
            // 비동기 함수 호출 시 바로 호출하지 않고 순차적으로 함수를 호출하기 위해 사용합니다.
            if (gameObject != null)
                SendQueue.StartSendQueue(true);

            if (transform.parent == null)
                DontDestroyOnLoad(this);
        }

        public void Update()
        {   
            // 뒤끝의 초기화가 성공하였는지 확인합니다.
            if (Backend.IsInitialized)
            {
                // 비동기 호출을 사용하기 위해 AysncPoll를 호출합니다.
                Backend.AsyncPoll();


                // SendQueue의 초기화가 완료되었는지 확인합니다.
                if (SendQueue.IsInitialize)
                {
                    // 모든 콜백 함수를 호출시킵니다/
                    SendQueue.Poll();
                }
            }
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause) SendQueue.PauseSendQueue();
            else SendQueue.ResumeSendQueue();
        }

        private void OnApplicationQuit()
        {
            SendQueue.StopSendQueue();
        }

        public void Initialize()
        {
            Backend.InitializeAsync(true, true, callback =>
            {
                if (callback.IsSuccess())
                    // 서버 설정에 성공해 다음 작업을 하도록 설정합니다.
                    GameManager.Instance.TitleController.LoadComplete = true;
                else
                    // 서버가 실패했을 때 UI를 추가 해줘야 하나요?
                    Debug.Log($"Server Initialize Faild");
            });
        }

        public void CheckAppVersion()
        {
// 전처리기를 사용해 현재 상태에 맞는 코드만 컴파일 합니다.
// UNITY_EDITOR는 유니티 에디터 일 때만 실행되는 코드입니다.
#if UNITY_EDITOR
            GameManager.Instance.TitleController.LoadComplete = true;
#else
            // 추후 버전 관리가 들어갈 때 제작하겠습니다.
            GameManager.Instance.TitleController.LoadComplete = true;
#endif
        }
    }
}