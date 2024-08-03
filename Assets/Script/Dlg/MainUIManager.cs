using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using WHDle.Util;
using WHDle.Util.Define;
using WHDle.Stage;
using BackEnd;
using Script.Controller;
using Script.Manager;

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

    // MiniGame
    [SerializeField]
    Minigame miniGame;

    // Player
    [SerializeField]
    PlayerAction playerAction;

    DialogueManager dialogueManager;
    SplashManager splashManager;

    public string OptionMainText { get; set; }
    public string OptionSubText  { get; set; }

    void Start()
    {
        playerAction = FindObjectOfType<PlayerAction>();

        dialogueManager = FindObjectOfType<DialogueManager>();
        splashManager = FindObjectOfType<SplashManager>();

        // Add Event
        interactionButton.onClick.AddListener(OnClick_ButtonUI_Button_Interaction);
        nextButtonBackGround.onClick.AddListener(OnClick_Next_Button_BackGround);
        backButton.onClick.AddListener(OnClick_BottomUI_Button_Back);
        skipButton.onClick.AddListener(OnClick_BottomUI_Button_Skip);

        // Set
        dialogueManager.SetDialogue(false);

        SoundManager.instance?.BGMPlay(true);
    }

    void Update()
    {
        
    }

    // OnClick
    void OnClick_ButtonUI_Button_Interaction()
    {
        var objectType = playerAction.InteractionType;

        SoundManager.instance.SFXPlay(SoundManager.SFXType.Interaction);

        Debug.Log(objectType);

        if (objectType == ObjectController.ObjectType.Letter)
        {
            dialogueManager.TempPlayStory();
        }
        else if (objectType == ObjectController.ObjectType.MiniGame1)
        {
            /*miniGame.SetLevel(0);

            miniGame.Open();

            miniGame.GameStart();*/
            
            playerAction.currentDialogueGroup = "Prologue1";
            
            dialogueManager.TempPlayStory();
        }
        else if (objectType == ObjectController.ObjectType.MiniGame2)
        {
            /*miniGame.SetLevel(1);

            miniGame.Open();

            miniGame.GameStart();*/

            playerAction.currentDialogueGroup = "Prologue2";
            
            dialogueManager.TempPlayStory();
        }
        else if (objectType == ObjectController.ObjectType.MiniGame3)
        {
            /*miniGame.SetLevel(2);

            miniGame.Open();

            miniGame.GameStart();*/

            playerAction.currentDialogueGroup = "Prologue3";
            
            dialogueManager.TempPlayStory();
        }
    }

    void OnClick_Next_Button_BackGround()
    {
        dialogueManager.SetIsNextStory();
    }

    void OnClick_BottomUI_Button_Back()
    {
        //Backend.BMember.Logout();

        //GameManager.Instance.LoadScene(SceneType.Title, StageManager.Instance.ChangeStage(), StageManager.Instance.OnChangeTitleScene);
        GameManager.Instance.LoadScene(SceneType.Title, null, StageManager.Instance.OnChangeTitleScene);
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
        dialogueManager.EndStory();
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
