using script.Common;
using UnityEngine;
using UnityEngine.UI;
using WHDle.Util;
using WHDle.Util.Define;
using WHDle.Stage;
using Script.Controller;
using Script.Manager;
using Unity.VisualScripting;

public class MainUIManager : MonoBehaviour
{
    [SerializeField] private GameObject topObject;
    [SerializeField] private GameObject bottomObject;
    [SerializeField] private GameObject interactionObject;

    [SerializeField] private Button interactionButton;
    [SerializeField] private Button inventoryButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button skipButton;

    [SerializeField] private Button nextButtonBackGround;


    [SerializeField] private PlayerAction playerAction;

    DialogueManager _dialogueManager;
    SplashManager _splashManager;

    public string OptionMainText { get; set; }
    public string OptionSubText { get; set; }

    private void AddListener()
    {
        interactionButton.onClick.AddListener(OnClickButtonUIButtonInteraction);
        inventoryButton.onClick.AddListener(OnInventoryButtonClicked);

        nextButtonBackGround.onClick.AddListener(OnClick_Next_Button_BackGround);
        backButton.onClick.AddListener(OnClick_BottomUI_Button_Back);
        skipButton.onClick.AddListener(OnClick_BottomUI_Button_Skip);
    }

    void Start()
    {
        AddListener();

        playerAction = FindObjectOfType<PlayerAction>();
        _dialogueManager = FindObjectOfType<DialogueManager>();
        _splashManager = FindObjectOfType<SplashManager>();

        // Set
        _dialogueManager.SetDialogue(false);

        SoundManager.instance?.BGMPlay(true);
    }

    void Update()
    {
    }

    // OnClick
    void OnClickButtonUIButtonInteraction()
    {
        var interactionType = playerAction.InteractionType;
        var playerData = PlayerDataFileHandler.FileLoad("PlayerData");
        var routineMap = playerData.RoutineMap;

        SoundManager.instance.SFXPlay(SoundManager.SFXType.Interaction);

        switch (interactionType)
        {
            case ObjectController.ObjectType.Letter:
            {
                _dialogueManager.TempPlayStory();
            } break;
            case ObjectController.ObjectType.MiniGame1:
            {
                UIManager.Instance.OpenPanel("MiniGamePanel");

                // playerAction.currentDialogueGroup = "Prologue1";
                //
                // _dialogueManager.TempPlayStory();
            } break;
            case ObjectController.ObjectType.MiniGame2:
            {
                playerAction.currentDialogueGroup = "Prologue2";

                _dialogueManager.TempPlayStory();
            } break;
            case ObjectController.ObjectType.MiniGame3:
            {
                playerAction.currentDialogueGroup = "Prologue3";

                _dialogueManager.TempPlayStory();
            } break;
        }
    }

    void OnInventoryButtonClicked()
    {
        UIManager.Instance.OpenPopup("InventoryPopup");
    }

    void OnClick_Next_Button_BackGround()
    {
        
    }

    void OnClick_BottomUI_Button_Back()
    {
        GameManager.Instance.LoadScene(SceneType.Title, null, StageManager.Instance.OnChangeTitleScene);
    }

    void OnClick_BottomUI_Button_Skip()
    {
        
    }

    public void OnClick_NoButton()
    {
        
    }

    public void OnClick_YesClick()
    {
        _dialogueManager.EndStory();
        _dialogueManager.SetDialogue(false);

        _splashManager.Reset();

        OptionMainText = "";
        OptionSubText = "";
    }
}