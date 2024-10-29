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
    [SerializeField] private GameObject _inventoyPopup;
    
    //
    [SerializeField] private GameObject topObject;
    [SerializeField] private GameObject bottomObject; 
    [SerializeField] private GameObject interactionObject;
    [SerializeField] private GameObject optionPopup;

    [SerializeField] private Button interactionButton;
    [SerializeField] private Button inventoryButton;
    [SerializeField] private Button backButton; 
    [SerializeField] private Button skipButton;

    [SerializeField] private Text optionMainText;
    [SerializeField] private Text optionSubText;

    [SerializeField] private Button nextButtonBackGround;
    
    [SerializeField] private MiniGamePanel miniGame;
    
    [SerializeField] private PlayerAction playerAction;
    
    DialogueManager _dialogueManager;
    SplashManager _splashManager;

    public string OptionMainText { get; set; }
    public string OptionSubText  { get; set; }
    
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
        var playerData = SaveDataManager.FileLoad("PlayerData");
        var routineMap = playerData.RoutineMapMap;

        SoundManager.instance.SFXPlay(SoundManager.SFXType.Interaction);

        switch (interactionType)
        {
            case ObjectController.ObjectType.Letter:
            {
                _dialogueManager.TempPlayStory();
                
                /*routineList.Exists(x => ObjectController.ObjectType.Letter.ToString());

                if (playerData.Routine.Exists(ObjectController.ObjectType.Letter.ToString()))
                {
                    dialogueManager.TempPlayStory();
                }
                else
                {
                    Debug.Log("지금은 확인할 편지가 없어");
                }*/
            }
                break;
            case ObjectController.ObjectType.MiniGame1:
                {
                    playerAction.currentDialogueGroup = "Prologue1";
                    
                    _dialogueManager.TempPlayStory();
                }            
                break;
            case ObjectController.ObjectType.MiniGame2:
                {
                    playerAction.currentDialogueGroup = "Prologue2";
                
                    _dialogueManager.TempPlayStory();
                }
                break;
            case ObjectController.ObjectType.MiniGame3:
                {
                    playerAction.currentDialogueGroup = "Prologue3";

                    _dialogueManager.TempPlayStory();
                }
                break;
        }
    }

    void OnInventoryButtonClicked()
    {
        UIManager.Instance.CreatePopup(_inventoyPopup);
    }

    void OnClick_Next_Button_BackGround()
    {
        //_dialogueManager.SetIsNextStory();
    }

    void OnClick_BottomUI_Button_Back()
    {
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
        _dialogueManager.EndStory();
        _dialogueManager.SetDialogue(false);
        
        _splashManager.Reset();

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
