using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Manager
{
    public class DialogueManager : MonoBehaviour
    { 
        // BottomUI
        [SerializeField] private GameObject topObject;
        [SerializeField] private GameObject bottomObject;

        // StoryUI
        [SerializeField] private GameObject nextObject;
        [SerializeField] private GameObject characterObject;
        [SerializeField] private GameObject skipObject;
        [SerializeField] private GameObject contextUpObject;
        [SerializeField] private GameObject contextDownObject;
        [SerializeField] private GameObject letterObject;
        [SerializeField] private GameObject narrationObject;
        [SerializeField] private GameObject choicetListObject;

        [SerializeField] private Button nextButtonBackGround;
        [SerializeField] private Button selectListOneButton;
        [SerializeField] private Button selectListTwoButton;
        [SerializeField] private Button selectListThreeButton;

        [SerializeField] private Text nameUpText;
        [SerializeField] private Text contextUpText;
        [SerializeField] private Text nameDownText;
        [SerializeField] private Text contextDownText;
        [SerializeField] private Text letterText;
        [SerializeField] private Text narrationText;

        [SerializeField] Transform tempTaregt;
        [SerializeField] Camera tempCamera;
        [SerializeField] DialogueEvent dialogueEvent;

        PlayerAction _playerAction;

        //
        DialogueEnum.DialogueType _currentDialogueType = DialogueEnum.DialogueType.ContextUp;

        Dialogue[] dialogues;
        bool isStoryPlay = false;
        bool isNext = false;
        bool isNextStory = false;
        bool isStoryContextEnd = false;

        int lineIndex = 0;
        int contextIndex = 0;
        float textDelay = 0.05f;

        IEnumerator dialogueCoroutine = null;

        MainUIManager mainUIManager;
        ChoiceManager choiceManager;

        SpriteManager spriteManager;
        SplashManager splashManager;

        //
        private int currentLine = 0;
        private int currentContext = 0;
        private bool isPlaying = false;
        
        public enum FontStyle
        {
            None,
            Bold,
            Italic,
            BoldAndItalic
        }

        public enum FontColor
        {
            None,
            White,
            Black
        }

        public struct FontConfiguration
        {
            public FontStyle fontStyle;
            public FontColor fontColor;
        }

        void Start()
        {
            LoadDialogueData("Prologue_Start");
            
            // 버튼 클릭 이벤트 연결
            nextButtonBackGround.onClick.AddListener(OnNextButtonClick);

            // 첫 번째 대화 출력
            ShowCurrentContext();
            
            //
            mainUIManager = FindObjectOfType<MainUIManager>();
            _playerAction = FindObjectOfType<PlayerAction>();

            // Set
            SetDialogue(false);

            choiceManager = FindObjectOfType<ChoiceManager>();
            spriteManager = FindObjectOfType<SpriteManager>();
            splashManager = FindObjectOfType<SplashManager>();
        }

        void Update()
        {
            if (isStoryPlay)
            {
                if (isNext && isNextStory)
                {
                    isNext = false;
                    isNextStory = false;

                    ResetText();

                    if (dialogueCoroutine != null)
                    {
                        StopCoroutine(dialogueCoroutine);

                        dialogueCoroutine = null;
                    }

                    dialogueCoroutine = DialoguePlayCoroutine();

                    if (++contextIndex < dialogues[lineIndex].contexts.Length)
                    {
                        StartCoroutine(dialogueCoroutine);
                        StartCoroutine(CameraAction());
                    }
                    else
                    {
                        contextIndex = 0;
                        if (++lineIndex < dialogues.Length)
                        {
                            StartCoroutine(dialogueCoroutine);
                            StartCoroutine(CameraAction());
                        }
                        else
                        {
                            EndStory();
                        }
                    }
                }

                Vector3 characterPosition = new Vector3(tempCamera.transform.position.x, tempCamera.transform.position.y -1.5f, 0);

                characterObject.transform.position = characterPosition;
            }
        }
        
        // 다음 Context로 넘어가는 함수
        private void OnNextButtonClick()
        {
            // 현재 Context가 마지막이면 다음 대화로 넘어감
            if (contextIndex < dialogues[lineIndex].contexts.Length - 1)
            {
                contextIndex++;
            }
            else
            {
                contextIndex = 0;
                if (lineIndex < dialogues.Length - 1)
                {
                    lineIndex++;
                }
                else
                {
                    // 모든 대화가 끝났을 때
                    Debug.Log("대화가 종료되었습니다.");
                    return;
                }
            }

            // 다음 Context 출력
            ShowCurrentContext();
        }
        
        // 현재 Context를 UI에 표시하는 함수
        private void ShowCurrentContext()
        {
            var currentDialogue = dialogues[lineIndex];
            contextDownText.text = currentDialogue.contexts[contextIndex];  // UI에 현재 대화 표시
            Debug.Log($"대화 중: {currentDialogue.contextName} - {currentDialogue.contexts[contextIndex]}");
        }
        
        private void LoadDialogueData(string dialogueGroup)
        {
            // CSV 데이터를 로드
            var dialogueTable = CSVDialogueParser.LoadDialogueTable("Assets/Resources/DataTable/DialogueTable.csv");
    
            // 입력된 dialogueGroup에 따라 데이터를 필터링
            var groupMap = new[] { ("Dialogue_Group", dialogueGroup) };
            var dialogueList = dialogueTable.GetByMultipleColumnsGroup(groupMap);

            // dialogueEvent에 데이터 매핑
            dialogueEvent = new DialogueEvent
            {
                dialogues = dialogueList.Select(data => new Dialogue
                {
                    //cameraType = (DialogueEnum.CameraActionType)Enum.Parse(typeof(DialogueEnum.CameraActionType), data["Dialogue_Action"]),
                    dialogueType = (DialogueEnum.DialogueType)Enum.Parse(typeof(DialogueEnum.DialogueType), data["Dialogue_Type"]),
                    contexts = data["Context_Text"].Split('|'),
                    //spriteNames = data["Context_Sprite"].Split('|'),
                    contextName = data["Context_CharacterName"]
                }).ToArray()
            };

            // 추가된 로그: dialogueEvent 데이터 확인
            foreach (var dialogue in dialogueEvent.dialogues)
            {
                Debug.Log($"CameraType: {dialogue.cameraType}, DialogueType: {dialogue.dialogueType}, CharacterName: {dialogue.contextName}, Contexts: {string.Join(", ", dialogue.contexts)}");
            }

            Debug.Log($"Total Dialogues Loaded: {dialogueEvent.dialogues.Length}");
        }

        public void StartDialogue()
        {
            SetDialogue(true);
            
            isPlaying = true;
            currentLine = 0;
            currentContext = 0;
            
            ShowCurrentDialogue();
        }

        private void ShowCurrentDialogue()
        {
            if (!isPlaying || dialogueEvent.dialogues.Length <= currentLine) 
                return;
            
            var dialogue = dialogueEvent.dialogues[currentLine];
            
            /*switch (dialogue.dialogueType)
            {
                case DialogueEnum.DialogueType.ContextUp:
                {
                    contextUpObject.SetActive(true);
                    break;
                }
                case DialogueEnum.DialogueType.ContextDown:
                {
                    contextDownObject.SetActive(true);
                    break;
                }
                case DialogueEnum.DialogueType.Letter:
                {
                    letterObject.SetActive(true);
                    break;
                }
                case DialogueEnum.DialogueType.Narration:
                {
                    narrationObject.SetActive(true);
                    break;
                }
                default:
                {
                    contextUpObject.SetActive(true);
                    contextDownObject.SetActive(false);
                    letterObject.SetActive(false);
                    narrationObject.SetActive(false);
                    break;
                }
            }*/
            
            /*if(dialogue.spriteNames[currentContext] != "")
            {
                StartCoroutine(spriteManager.SpriteChangeCoroutine(tempTaregt, dialogue.spriteNames[currentContext]));
            }*/
            
            nameUpText.text = dialogue.contextName;
            contextUpText.text = dialogue.contexts[currentContext];
            
            // 카메라 및 대화 스타일 처리 추가 가능
        }

        public void NextDialogue()
        {
            if (++currentContext >= dialogueEvent.dialogues[currentLine].contexts.Length)
            {
                currentContext = 0;
                if (++currentLine >= dialogueEvent.dialogues.Length)
                {
                    EndDialogue();
                }
                else
                {
                    ShowCurrentDialogue();
                }
            }
            else
            {
                ShowCurrentDialogue();
            }
        }

        private void EndDialogue()
        {
            isPlaying = false;
            
            SetDialogue(false);
            // 대화 종료 처리
        }
        
        private Dialogue[] GetStory()
        {
            var dialogueTable = CSVDialogueParser.LoadDialogueTable("Assets/Resources/DataTable/DialogueTable.csv");
            var dialogueList = dialogueTable.GetByMultipleColumnsGroup(new [] 
            {
                ("Dialogue_Group", "Prologue_Start")
            });
    
            dialogueEvent.dialogues = dialogueList.Select(data => new Dialogue
            {
                cameraType = (DialogueEnum.CameraActionType)Enum.Parse(typeof(DialogueEnum.CameraActionType), data["CameraType"]),
                dialogueType = (DialogueEnum.DialogueType)Enum.Parse(typeof(DialogueEnum.DialogueType), data["DialogueType"]),
                contexts = data["Context_Text"].Split('|'),
                spriteNames = data["Sprite_Name"].Split('|'),
                contextName = data["Context_CharacterName"]
            }).ToArray();

            return dialogueEvent.dialogues;
        }

        public void SetDialogue(bool pIsStoryShow)
        {
            // false
            topObject.SetActive(!pIsStoryShow);
            bottomObject.SetActive(!pIsStoryShow);

            // true
            nextObject.SetActive(pIsStoryShow);
            skipObject.SetActive(pIsStoryShow);
            characterObject.SetActive(pIsStoryShow);

            if (pIsStoryShow)
            {
                contextUpObject.SetActive(false);
                contextDownObject.SetActive(false);
                letterObject.SetActive(false);
                narrationObject.SetActive(false);

                switch (_currentDialogueType)
                {
                    case DialogueEnum.DialogueType.ContextUp:
                    {
                        contextUpObject.SetActive(true);
                        break;
                    }
                    case DialogueEnum.DialogueType.ContextDown:
                    {
                        contextDownObject.SetActive(true);
                        break;
                    }
                    case DialogueEnum.DialogueType.Letter:
                    {
                        letterObject.SetActive(true);
                        break;
                    }
                    case DialogueEnum.DialogueType.Narration:
                    {
                        narrationObject.SetActive(true);
                        break;
                    }
                    default:
                    {
                        contextUpObject.SetActive(true);
                        contextDownObject.SetActive(false);
                        letterObject.SetActive(false);
                        narrationObject.SetActive(false);
                        break;
                    }
                }
            }
            else
            {
                contextUpObject.SetActive(false);
                contextDownObject.SetActive(false);
                letterObject.SetActive(false);
                narrationObject.SetActive(false);
            }

            //StartCoroutine(CameraAction());
        }

        IEnumerator CameraAction()
        {
            dialogues = GetStory();

            if (isStoryPlay && dialogues != null && splashManager != null)
            {
                switch (dialogues[lineIndex].cameraActions[contextIndex])
                {
                    case DialogueEnum.CameraActionType.FadeOut:
                    {
                        SplashManager.isFinish = false;
                        StartCoroutine(splashManager.FadeOut(false, true));

                        yield return new WaitUntil(() => SplashManager.isFinish);
                        break;
                    }
                    case DialogueEnum.CameraActionType.FadeIn:
                    {
                        SplashManager.isFinish = false;
                        StartCoroutine(splashManager.FadeIn(false, true));

                        yield return new WaitUntil(() => SplashManager.isFinish);
                        break;
                    }
                    case DialogueEnum.CameraActionType.FlashOut:
                    {
                        SplashManager.isFinish = false;
                        StartCoroutine(splashManager.FadeOut(true, true));

                        yield return new WaitUntil(() => SplashManager.isFinish);
                        break;
                    }
                    case DialogueEnum.CameraActionType.FlashIn:
                    {
                        SplashManager.isFinish = false;
                        StartCoroutine(splashManager.FadeIn(true, true));

                        yield return new WaitUntil(() => SplashManager.isFinish);
                        break;
                    }
                    default:
                    {
                        break;
                    }
                }

                SplashManager.isFinish = true;
            }
        }
    
        public void EndStory()
        {
            isStoryPlay = false;
            contextIndex = 0;
            lineIndex = 0;
            dialogues = null;
            isNext = false;

            SetDialogue(false);

            var dialogueProceeding = CSVDataManager.Instance.GetDialogueProceedingData(_playerAction?.currentDialogueGroup);
            
            Debug.Log("확인용 : " + dialogueProceeding.nextDialogue);
        }

        void ShowStory(Dialogue[] pStorys)
        {
            if (dialogues[lineIndex].dialogueType != DialogueEnum.DialogueType.None)
                _currentDialogueType = dialogues[lineIndex].dialogueType;

            if (dialogues[lineIndex].skipContext != "") {
                mainUIManager.OptionMainText = dialogues[lineIndex].skipContext;
                mainUIManager.OptionSubText = "스토리를 Skip 하시겠습니?";
            }

            SetDialogue(true);

            ResetText();

            dialogues = pStorys;


            if (dialogueCoroutine != null)
            {
                StopCoroutine(dialogueCoroutine);

                dialogueCoroutine = null;
            }

            dialogueCoroutine = DialoguePlayCoroutine();
            StartCoroutine(dialogueCoroutine);
        }

        void ChangeSprite()
        {
            if(dialogues[lineIndex].spriteNames[contextIndex] != "")
            {
                StartCoroutine(spriteManager.SpriteChangeCoroutine(tempTaregt, dialogues[lineIndex].spriteNames[contextIndex]));
            }
        }

        void ShowChoice(string _choiceGroup)
        {
            if (_choiceGroup != null && _choiceGroup != "")
            {
                choiceManager.SetChoiceData(_choiceGroup);
            }
        }

        IEnumerator DialoguePlayCoroutine()
        {
            ChangeSprite();

            string context = dialogues[lineIndex].contexts[contextIndex];

            context = context.Replace("\\", ",");

            FontConfiguration fontConfiguration = new FontConfiguration();

            fontConfiguration.fontColor = FontColor.Black;

            string nameText = dialogues[lineIndex].contextName;

            _currentDialogueType = 
                dialogues[lineIndex].dialogueType != DialogueEnum.DialogueType.None ? 
                    dialogues[lineIndex].dialogueType : _currentDialogueType;

            SetDialogue(true);

            switch (_currentDialogueType)
            {
                case DialogueEnum.DialogueType.ContextUp:
                {
                    nameUpText.text = nameText;

                    break;
                }
                case DialogueEnum.DialogueType.ContextDown:
                {
                    nameDownText.text = nameText;

                    break;
                }
                default:
                {
                    nameUpText.text = "";
                    nameDownText.text = "";

                    break;
                }
            }

            SetDialogue(true);

            for (int i = 0; i < context.Length; ++i)
            {
                bool isWrite = false;

                string letter = context[i].ToString();

                switch (letter)
                {
                    case "ⓑ":
                    {
                        fontConfiguration.fontColor = FontColor.Black;

                        break;
                    }
                    case "ⓦ":
                    {
                        fontConfiguration.fontColor = FontColor.White;

                        break;
                    }
                    case "ⓝ":
                    {
                        fontConfiguration.fontStyle = FontStyle.None;

                        break;
                    }
                    case "ⓜ":
                    {
                        fontConfiguration.fontStyle = FontStyle.Bold;

                        break;
                    }
                    case "ⓘ":
                    {
                        fontConfiguration.fontStyle = FontStyle.Italic;

                        break;
                    }
                    default:
                    {
                        isWrite = true;

                        break;
                    }
                }

                if (isWrite)
                {
                    letter = letterFont(letter, fontConfiguration);

                    switch (_currentDialogueType)
                    {
                        case DialogueEnum.DialogueType.ContextUp:
                        {
                            contextUpText.text += letter;
                            break;
                        }
                        case DialogueEnum.DialogueType.ContextDown:
                        {
                            contextDownText.text += letter;
                            break;
                        }
                        case DialogueEnum.DialogueType.Narration:
                        {
                            narrationText.text += letter;
                            break;
                        }
                        case DialogueEnum.DialogueType.Letter:
                        {
                            letterText.text += letter;
                            break;
                        }
                        default:
                        {
                            contextUpText.text += letter;
                            break;
                        }
                    }

                    yield return new WaitForSeconds(textDelay);
                }
                else
                {
                    continue;
                }
            }

            ShowChoice(dialogues[lineIndex].choiceGroup);

            isNext = true;
        }

        string letterFont(string pLetter, FontConfiguration pFontConfiguration)
        {
            switch (pFontConfiguration.fontColor)
            {
                case FontColor.Black:
                {
                    pLetter = "<color=#000000>" + pLetter + "</color>";

                    break;
                }
                case FontColor.White:
                {
                    pLetter = "<color=#ffffff>" + pLetter + "</color>";

                    break;
                }
                default:
                {
                    break;
                }
            }

            switch (pFontConfiguration.fontStyle)
            {
                case FontStyle.None:
                {
                    break;
                }
                case FontStyle.Bold:
                {
                    pLetter = "<b>" + pLetter + "</b>";

                    break;
                }
                case FontStyle.Italic:
                {
                    pLetter = "<i>" + pLetter + "</i>";

                    break;
                }
                default:
                {
                    break;
                }
            }

            return pLetter;
        }

        void ResetText()
        {
            nameUpText.text = "";
            contextUpText.text = "";
            nameDownText.text = "";
            contextDownText.text = "";
            letterText.text = "";
            narrationText.text = "";
        }

        public void SetIsNextStory()
        {
            isNextStory = true;
        }

        public void TempPlayStory()
        {
            StartDialogue();
                
            // lineIndex = 0;
            // isNext = true;
            // isStoryPlay = true;
            // ShowStory(GetStory());
        }
    }
}
