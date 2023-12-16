using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using WHDle.Server;
using WHDle.Util;
using WHDle.Util.Define;

namespace WHDle.Controller {

    /// <summary>
    /// Title Scene 컨트롤러
    /// 1. 로딩에 필요한 서버, 데이터, 유저 정보 로딩
    /// 2. 진행도 표현 로딩바 제어
    /// </summary>
    public class TitleController : MonoBehaviour
    {
        // 로딩 완료에 대한 변수
        private bool loadComplete;

        // 모든 과정이 로딩이 완료되었는가에 대한 변수.
        private bool allLoaded = false;

        // 프로퍼티 -> loadComplete 변수 
        public bool LoadComplete
        {
            get => loadComplete;
            set
            {
                loadComplete = true;

                /* 만약 모든 작업이 끝나지 않았으면,
                 * 다음 Phase를 시작 */
                
                if (!allLoaded)
                {
                    nextPhase();
                }
            }
        }

        private Coroutine loadGagueUpdateCoroutine;

        public void SetLoadStateGagueAfterLogin(IntroPhase phase)
        {

        }

        // 현재 로딩하고 있는 페이지를 알려주는 변수
        // IntroPhase => Define.cs
        [SerializeField]
        private IntroPhase introPhase = IntroPhase.Start;

        // 초기화
        public void Initialize() => onPhase(introPhase);


        // introPhase변수에 맞는 초기화 로딩
        private void onPhase(IntroPhase phase)
        {
            if (phase > IntroPhase.Login)
                SetLoadStateGagueAfterLogin(phase);

            Debug.Log($"Phase = {phase}");

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
                case IntroPhase.Login:
                    break;
                case IntroPhase.StaticData:
                    break;
                case IntroPhase.UserData:
                    break;
                case IntroPhase.Resource:
                    break;
                case IntroPhase.UI:
                    break;
                case IntroPhase.Complete:
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
    }
}