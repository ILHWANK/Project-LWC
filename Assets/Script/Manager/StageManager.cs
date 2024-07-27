using UnityEngine;
using WHDle.Controller;
using WHDle.Util;
namespace WHDle.Stage
{
    public class StageManager : Singleton<StageManager>
    {
        public bool isReady;
        private GameObject currentStage;

        private void Update()
        {
            if (!isReady)
                return;
        }

        public void OnChangeTitleScene()
        {
            var titleController = GameManager.Instance.TitleController;

            if(titleController == null)
                titleController = FindObjectOfType<TitleController>();

            //titleController.RestartLogin();
        }

        /*
        public IEnumerator ChangeStage()
        {
            SceneManager.MoveGameObjectToScene(currentStage, SceneManager.GetSceneByName(SceneType.GamePlay.ToString()));

            if (currentStage != null)
                Destroy(currentStage);

            yield return null;
        }
        */
    }
}