using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainDlg : MonoBehaviour
{
    Button backButton;

    // Start is called before the first frame update
    void Start()
    {
        // Button
        backButton = GameObject.Find("LeftTop_Button_Back").GetComponent<Button>();

        //Add Event
        backButton.onClick.AddListener(OnClick_LeftTop_Button_Back);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick_LeftTop_Button_Back()
    {
        SceneManager.LoadScene("Title");
    }
}
