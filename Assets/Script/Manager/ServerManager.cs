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
        // ó�� �α��� �ϴ��� Ȯ���ϴ� ����
        public bool isFirstLogin = true;

        // Server���� �޾ƿ� User �����Դϴ�.
        // public DtoUserInfo userInfo;

        protected override void Awake()
        {
            base.Awake();

            // Send Queue ����� ����ϱ� ���� ����Դϴ�.
            // �񵿱� �Լ� ȣ�� �� �ٷ� ȣ������ �ʰ� ���������� �Լ��� ȣ���ϱ� ���� ����մϴ�.
            if (gameObject != null)
                SendQueue.StartSendQueue(true);

            if (transform.parent == null)
                DontDestroyOnLoad(this);
        }

        public void Update()
        {   
            // �ڳ��� �ʱ�ȭ�� �����Ͽ����� Ȯ���մϴ�.
            if (Backend.IsInitialized)
            {
                // �񵿱� ȣ���� ����ϱ� ���� AysncPoll�� ȣ���մϴ�.
                Backend.AsyncPoll();


                // SendQueue�� �ʱ�ȭ�� �Ϸ�Ǿ����� Ȯ���մϴ�.
                if (SendQueue.IsInitialize)
                {
                    // ��� �ݹ� �Լ��� ȣ���ŵ�ϴ�/
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
                    // ���� ������ ������ ���� �۾��� �ϵ��� �����մϴ�.
                    GameManager.Instance.TitleController.LoadComplete = true;
                else
                    // ������ �������� �� UI�� �߰� ����� �ϳ���?
                    Debug.Log($"Server Initialize Faild");
            });
        }

        public void CheckAppVersion()
        {
// ��ó���⸦ ����� ���� ���¿� �´� �ڵ常 ������ �մϴ�.
// UNITY_EDITOR�� ����Ƽ ������ �� ���� ����Ǵ� �ڵ��Դϴ�.
#if UNITY_EDITOR
            GameManager.Instance.TitleController.LoadComplete = true;
#else
            // ���� ���� ������ �� �� �����ϰڽ��ϴ�.
            GameManager.Instance.TitleController.LoadComplete = true;
#endif
        }
    }
}