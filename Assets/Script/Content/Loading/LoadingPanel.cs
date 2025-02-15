using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Script.Core.UI;

public class LoadingPanel : UIPanel
{
    [SerializeField] private Slider _progressBar;
    [SerializeField] private Text _progressText;

    public override void OnEnter()
    {
        base.OnEnter();

        UpdateProgress(0f);
    }

    private void UpdateProgress(float progress)
    {
        if (_progressBar != null)
            _progressBar.value = progress;

        if (_progressText != null)
            _progressText.text = $"{Mathf.CeilToInt(progress * 100)}%";
    }

    public void LoadSceneAsync(string sceneName, Action onComplete = null)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName, onComplete));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName, Action onComplete)
    {
        OnEnter();

        var operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            UpdateProgress(progress);

            if (operation.progress >= 0.9f)
            {
                UpdateProgress(1f);
                yield return new WaitForSeconds(0.5f);
                operation.allowSceneActivation = true;
            }

            yield return null;
        }

        OnExit();
        onComplete?.Invoke();
    }
}