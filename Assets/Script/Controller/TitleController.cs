using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Unity.Collections;
using UnityEngine;
using WHDle.Database;
using WHDle.Server;
using WHDle.Stage;
using WHDle.Util;
using WHDle.Util.Define;

namespace WHDle.Controller {

    /// <summary>
    /// Title Scene ????????
    /// 1. ?????? ?????? ????, ??????, ???? ???? ????
    /// 2. ?????? ???? ?????? ????
    /// </summary>
    public class TitleController : MonoBehaviour
    {
        // ???? ?????? ???? ????
        private bool loadComplete;

        // ???? ?????? ?????? ?????????????? ???? ????.
        private bool allLoaded = false;

        // ???????? -> loadComplete ???? 
        public bool LoadComplete
        {
            get => loadComplete;
            set
            {
                loadComplete = true;

                /* ???? ???? ?????? ?????? ????????,
                 * ???? Phase?? ???? */
                
                if (!allLoaded)
                {
                    //nextPhase();
                }
            }
        }

        [SerializeField]
        private TitleDlg title;


        // ???? ???????? ???? ???????? ???????? ????
        // IntroPhase => Define.cs
        [SerializeField]
        private IntroPhase introPhase = IntroPhase.Start;

        // ??????
        //public void Initialize() => onPhase(introPhase);

        public void RestartLogin()
        {
            introPhase = IntroPhase.BeforeLogin;

            GameManager.Instance.TitleController = this;
            title = FindObjectOfType<TitleDlg>();

            onPhase(introPhase);
        }

        // introPhase?????? ???? ?????? ????
        private void onPhase(IntroPhase phase)
        {
            GameManager.Instance.LoadScene(SceneType.GamePlay);
            allLoaded = true;
            LoadComplete = true;
            
            /*switch (phase)
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
                case IntroPhase.PoolableObject:
                    ResourcesManager.Instance.RegistAllPoolableObject();
                    break;
                case IntroPhase.Complete:
                    var stageManager = StageManager.Instance;
                    //GameManager.Instance.LoadScene(SceneType.GamePlay, stageManager.ChangeStage());
                    GameManager.Instance.LoadScene(SceneType.GamePlay);
                    allLoaded = true;
                    LoadComplete = true;
                    break;
                default:
                    break;
            }*/
        }

        private void nextPhase()
        {
            StartCoroutine(WaitForSeconds());

            IEnumerator WaitForSeconds()
            {
                yield return new WaitForSeconds(.25f);

                //loadComplete = true;
                //onPhase(++introPhase);

                var stageManager = StageManager.Instance;
                //GameManager.Instance.LoadScene(SceneType.GamePlay, stageManager.ChangeStage());
                GameManager.Instance.LoadScene(SceneType.GamePlay);
            }
        }

        public void SkipRegister()
            => introPhase = IntroPhase.Register;
    }
}