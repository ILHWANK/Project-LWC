using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using WHDle.Util;
using WHDle.Util.Define;
using WHDle.Stage;
using BackEnd;

public class MainUIManager : MonoBehaviour
{
    public static MainUIManager Instance;

    // BottomUI
    [SerializeField]
    GameObject topObject, bottomObject, interactionObject;

    [SerializeField] 
    Button interactionButton, backButton;

    // StoryUI
    //[SerializeField]
    //GameObject nextObject, skipObject, contextObject, selectListOneObject, selectListTwoObject, selectListThreeObject;

    [SerializeField]
    Button nextButtonBackGround;

    DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();

        // Add Event
        interactionButton.onClick.AddListener(OnClick_ButtonUI_Button_Interaction);
        nextButtonBackGround.onClick.AddListener(OnClick_Next_Button_BackGround);
        backButton.onClick.AddListener(OnClick_BottomUI_Button_Back);

        // Set
        dialogueManager.SetShowStory(false);
    }

    void Update()
    {
        
    }

    /*
    void SetShowStory(bool pIsStoryShow){
        topObject.SetActive(!pIsStoryShow);
        bottomObject.SetActive(!pIsStoryShow);
        nextObject.SetActive(pIsStoryShow);
        skipObject.SetActive(pIsStoryShow);
        contextObject.SetActive(pIsStoryShow);
    }
    */

    void OnClick_ButtonUI_Button_Interaction()
    { 
        dialogueManager.TempPlayStory();
    }

    void OnClick_Next_Button_BackGround()
    {
        dialogueManager.SetIsNextStory();
    }

    void OnClick_BottomUI_Button_Back()
    {
        Backend.BMember.Logout();

        GameManager.Instance.LoadScene(SceneType.Title, StageManager.Instance.ChangeStage(), StageManager.Instance.OnChangeTitleScene);
    }

}
