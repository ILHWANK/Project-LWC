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
    Button interactionButton, backButton, skipButton;

    // OptionPopup
    [SerializeField]
    GameObject optionPopup;

    [SerializeField]
    Text optionMainText, optionSubText;

    [SerializeField]
    Button nextButtonBackGround;

    DialogueManager dialogueManager;
    SplashManager splashManager;

    public string OptionMainText { get; set; }
    public string OptionSubText  { get; set; }

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        splashManager = FindObjectOfType<SplashManager>();

        // Add Event
        interactionButton.onClick.AddListener(OnClick_ButtonUI_Button_Interaction);
        nextButtonBackGround.onClick.AddListener(OnClick_Next_Button_BackGround);
        backButton.onClick.AddListener(OnClick_BottomUI_Button_Back);
        skipButton.onClick.AddListener(OnClick_BottomUI_Button_Skip);

        // Set
        dialogueManager.SetDialogue(false);
    }

    void Update()
    {
        
    }

    // OnClick
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
        //Backend.BMember.Logout();

        GameManager.Instance.LoadScene(SceneType.Title, StageManager.Instance.ChangeStage(), StageManager.Instance.OnChangeTitleScene);
    }

    void OnClick_BottomUI_Button_Skip()
    {
        OpenOptionPopup();
    }

    public void OnClick_NoButton()
    {
        optionPopup.SetActive(false);
    }

    public void OnClick_YesClick()
    {
        dialogueManager.SetDialogue(false);
        splashManager.Reset();

        optionPopup.SetActive(false);

        OptionMainText = "";
        OptionSubText = "";
    }

    // Evnet
    public void OpenOptionPopup()
    {
        optionPopup.SetActive(true);

        optionMainText.text = OptionMainText;
        optionSubText.text = OptionSubText;
    }
}
