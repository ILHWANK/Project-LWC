using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingDlg : MonoBehaviour
{
    public Slider loadingSlider;

    int loadingValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        loadingSlider.minValue = 0;
        loadingSlider.maxValue = 3;
        loadingSlider.value = 0;

        StartCoroutine(LoadingCoroutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator LoadingCoroutine()
    {
        while (loadingValue < loadingSlider.maxValue)
        {
            loadingValue++;

            loadingSlider.value = loadingValue;

            if (loadingValue == 3)
                SceneManager.LoadScene("GamePlay");

            yield return new WaitForSeconds(1f);
        }
    }
}
