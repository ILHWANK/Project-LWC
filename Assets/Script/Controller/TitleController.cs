using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using WHDle.Database;
using WHDle.Server;
using WHDle.Stage;
using WHDle.Util;
using WHDle.Util.Define;

namespace WHDle.Controller {

    /// <summary>
    /// Title Scene ��Ʈ�ѷ�
    /// 1. �ε��� �ʿ��� ����, ������, ���� ���� �ε�
    /// 2. ���൵ ǥ�� �ε��� ����
    /// </summary>
    public class TitleController : MonoBehaviour
    {
        // �ε� �Ϸῡ ���� ����
        private bool loadComplete;

        // ��� ������ �ε��� �Ϸ�Ǿ��°��� ���� ����.
        private bool allLoaded = false;

        // ������Ƽ -> loadComplete ���� 
        public bool LoadComplete
        {
            get => loadComplete;
            set
            {
                loadComplete = true;

                /* ���� ��� �۾��� ������ �ʾ�����,
                 * ���� Phase�� ���� */
                
                if (!allLoaded)
                {
                    nextPhase();
                }
            }
        }

        [SerializeField]
        private TitleDlg title;

        private Coroutine loadGagueUpdateCoroutine;

        public void SetLoadStateGagueAfterLogin(IntroPhase phase)
        {

        }

        // ���� �ε��ϰ� �ִ� �������� �˷��ִ� ����
        // IntroPhase => Define.cs
        [SerializeField]
        private IntroPhase introPhase = IntroPhase.Start;

        // �ʱ�ȭ
        public void Initialize() => onPhase(introPhase);

        // introPhase������ �´� �ʱ�ȭ �ε�
        private void onPhase(IntroPhase phase)
        {
            if (phase > IntroPhase.AfterLogin)
                SetLoadStateGagueAfterLogin(phase);

            switch (phase)
            {
                case IntroPhase.Start:
                    LoadComplete = true;
                    break;
                case IntroPhase.ApplicationSetting:
                    GameManager.Instance.OnAplicationSetting();
                    LoadComplete = true;
                    break;
                case IntroPhase.ServerInit:
                    ServerManager.Instance.Initialize();
                    break;
                case IntroPhase.VersionCheck:
                    ServerManager.Instance.CheckAppVersion();
                    break;
                case IntroPhase.BeforeLogin:
                    title.BeforeLogin();
                    break;
                case IntroPhase.Register:
                    title.EnableRegisterPanel();
                    break;
                case IntroPhase.AfterLogin:
                    title.AfterLogin();
                    break;
                case IntroPhase.Save_Load:
                    ServerManager.Instance.CheckIsFirstLogin();

                    title.SaveLoadPanelEnable();
                    break;
                case IntroPhase.StaticData:
                    GameManager.SD.Initialize();
                    break;
                case IntroPhase.UserData:
                    DatabaseManager.Instance.LoaduserData(() => LoadComplete = true);
                    break;
                case IntroPhase.Complete:
                    var stageManager = StageManager.Instance;
                    GameManager.Instance.LoadScene(SceneType.GamePlay, stageManager.ChangeStage(), stageManager.OnChangeStageComplete);
                    allLoaded = true;
                    LoadComplete = true;
                    break;
                default:
                    break;
            }
        }

        private void nextPhase()
        {
            StartCoroutine(WaitForSeconds());

            IEnumerator WaitForSeconds()
            {
                yield return new WaitForSeconds(.5f);

                loadComplete = true;
                onPhase(++introPhase);
            }
        }

        public void SkipRegister()
            => introPhase = IntroPhase.Register;
    }
}