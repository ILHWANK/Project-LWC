using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WHDle.Util.Define;

namespace WHDle.Controller {
    public class TitleController : MonoBehaviour
    {
        private bool loadComplete;

        private bool allLoaded = false;

        public bool LoadComplete
        {
            get => loadComplete;
            set
            {
                loadComplete = true;

                if (loadComplete && !allLoaded)
                {
                    nextPhase();
                }
            }
        }

        private Coroutine loadGagueUpdateCoroutine;

        public void SetLoadStateGagueBeforeLogin(IntroPhase phase)
        {

        }

        public void SetLoadStateGagueAfterLogin(IntroPhase phase)
        {

        }


        private IntroPhase introPhase = IntroPhase.Start;

        public void Initialize() => onPhase(introPhase);

        private void onPhase(IntroPhase phase)
        {
            if (phase <= IntroPhase.Login) { SetLoadStateGagueBeforeLogin(phase); }
        }
    
        private void nextPhase()
        {
            StartCoroutine(WaitForSeconds());

            IEnumerator WaitForSeconds()
            {
                yield return new WaitForSeconds(1f);

                loadComplete = true;
                onPhase(introPhase);
            }
        }
    }
}