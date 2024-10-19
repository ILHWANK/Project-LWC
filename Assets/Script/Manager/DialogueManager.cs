using System;
using System.Collections;
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

        [SerializeField] private Image fadeImage;
        
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

        public FadeManager fadeManager;
        
        PlayerAction _playerAction;

        //
        DialogueEnum.DialogueType _currentDialogueType = DialogueEnum.DialogueType.ContextUp;
        private DialogueEnum.CameraActionType _cameraActionType = DialogueEnum.CameraActionType.None;

        
        //Dialogue[] dialogues;
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
        //SplashManager splashManager;

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
            isNext = true;
            LoadDialogueData("Prologue_Start");
            
            // 버튼 클릭 이벤트 연결
            nextButtonBackGround.onClick.AddListener(NextDialogue);
            
            //
            mainUIManager = FindObjectOfType<MainUIManager>();
            _playerAction = FindObjectOfType<PlayerAction>();

            // Set
            SetDialogue(false);

            choiceManager = FindObjectOfType<ChoiceManager>();
            spriteManager = FindObjectOfType<SpriteManager>();
            //splashManager = FindObjectOfType<SplashManager>();
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
                    cameraType = (DialogueEnum.CameraActionType)Enum.Parse(typeof(DialogueEnum.CameraActionType), data["Dialogue_Action"]),
                    dialogueType = (DialogueEnum.DialogueType)Enum.Parse(typeof(DialogueEnum.DialogueType), data["Dialogue_Type"]),
                    contexts = data["Context_Text"].Split('|'),
                    spriteNames = data["Context_Sprite"].Split('|'),
                    contextName = data["Context_CharacterName"]
                }).ToArray()
            };
        }

        public void StartDialogue()
        {
            SetDialogue(true);
            
            isPlaying = true;
            currentLine = 0;
            currentContext = 0;
            
            contextUpObject.SetActive(false);
            contextDownObject.SetActive(false);
            letterObject.SetActive(false);
            narrationObject.SetActive(false);
            
            ShowCurrentDialogue();
        }
        private void ShowCurrentDialogue()
        {
            if (!isPlaying || dialogueEvent.dialogues.Length <= currentLine)
                return;

            var dialogue = dialogueEvent.dialogues[currentLine];

            StartCoroutine(spriteManager.SpriteChangeCoroutine(tempTaregt, dialogue.spriteNames[currentContext]));
            StartCoroutine(ShowCurrentDialogueCoroutine(dialogue));
        }

        private IEnumerator ShowCurrentDialogueCoroutine(Dialogue dialogue)
        {
            isNext = false; // 다음으로 넘어갈 수 없도록 설정
                
            ResetText(); // 기존 텍스트 초기화
            
            yield return StartCoroutine(CameraAction(dialogue));

            var nameText = dialogue.contextName;
            
            _currentDialogueType = dialogue.dialogueType;
            _cameraActionType = dialogue.cameraType;
            
            nameUpText.text = nameText;
            
            SetDialogue();

            yield return DialoguePlayCoroutine(dialogue.contexts[currentContext]);

            isNext = true;
        }

        private IEnumerator DialoguePlayCoroutine(string context)
        {
            var fontConfiguration = new FontConfiguration { fontColor = FontColor.Black };

            context = context.Replace("\\", ",");
            
            foreach (var text in context)
            {
                var isWrite = false;
                var letter = text.ToString();

                switch (letter)
                {
                    case "ⓑ":
                        fontConfiguration.fontColor = FontColor.Black;
                        break;
                    case "ⓦ":
                        fontConfiguration.fontColor = FontColor.White;
                        break;
                    case "ⓝ":
                        fontConfiguration.fontStyle = FontStyle.None;
                        break;
                    case "ⓜ":
                        fontConfiguration.fontStyle = FontStyle.Bold;
                        break;
                    case "ⓘ":
                        fontConfiguration.fontStyle = FontStyle.Italic;
                        break;
                    default:
                        isWrite = true;
                        break;
                }

                if (!isWrite) 
                    continue;

                letter = letterFont(letter, fontConfiguration);
                AddLetterToCurrentDialogue(letter);
                
                yield return new WaitForSeconds(textDelay);
            }
        }

        private void SetData()
        {
            
        }
        
        private void SetDialogue()
        {
            contextUpObject.SetActive(false);
            contextDownObject.SetActive(false);
            letterObject.SetActive(false);
            narrationObject.SetActive(false);
            
            switch (_currentDialogueType)
            {
                case DialogueEnum.DialogueType.ContextUp:
                    contextUpObject.SetActive(true);
                    
                    break;
                case DialogueEnum.DialogueType.ContextDown:
                    contextDownObject.SetActive(true);
                    
                    break;
                case DialogueEnum.DialogueType.Letter:
                    letterObject.SetActive(true);
                    
                    break;
                case DialogueEnum.DialogueType.Narration:
                    narrationObject.SetActive(true);
                    
                    break;
                case DialogueEnum.DialogueType.None:
                default: 
                    break;
            }
        }
        
        private void AddLetterToCurrentDialogue(string letter)
        {
            switch (_currentDialogueType)
            {
                case DialogueEnum.DialogueType.ContextUp:
                    contextUpText.text += letter;
                    break;
                case DialogueEnum.DialogueType.ContextDown:
                    contextDownText.text += letter;
                    break;
                case DialogueEnum.DialogueType.Narration:
                    narrationText.text += letter;
                    break;
                case DialogueEnum.DialogueType.Letter:
                    letterText.text += letter;
                    break;
            }
        }

        private void NextDialogue()
        {
            if (!isNext)
                return;
            
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
                    case DialogueEnum.DialogueType.None:
                    default:
                    {
                        contextUpObject.SetActive(false);
                        contextDownObject.SetActive(false);
                        letterObject.SetActive(false);
                        narrationObject.SetActive(false);
                        break;
                    }
                }
            }
        }

        IEnumerator CameraAction(Dialogue dialogue)
        {
            var white = new Color(1, 1, 1, 1);
            var black = new Color(0, 0, 0, 1);
            
            switch (dialogue.cameraType)
            {
                case DialogueEnum.CameraActionType.FadeOut:
                {
                    SetDialogue();
                    
                    fadeImage.color = black;
                    yield return StartCoroutine(fadeManager.FadeOut(fadeImage, 1f));
                    
                    break;
                }
                case DialogueEnum.CameraActionType.FadeIn:
                {
                    SetDialogue();

                    fadeImage.color = black;
                    yield return StartCoroutine(fadeManager.FadeIn(fadeImage, 1f));
                    
                    break;
                }
                case DialogueEnum.CameraActionType.FlashOut:
                {
                    SetDialogue();
                    
                    fadeImage.color = white;
                    yield return StartCoroutine(fadeManager.FadeOut(fadeImage, 1f));
                    
                    break;
                }
                case DialogueEnum.CameraActionType.FlashIn:
                {
                    SetDialogue();

                    fadeImage.color = white;
                    yield return StartCoroutine(fadeManager.FadeIn(fadeImage, 1f));
                    
                    break;
                }
                case DialogueEnum.CameraActionType.None:
                default:
                {
                    break;
                }
            }
        }
    
        public void EndStory()
        {
            contextIndex = 0;
            lineIndex = 0;
            isNext = false;
        }

        void ShowChoice(string _choiceGroup)
        {
            if (_choiceGroup != null && _choiceGroup != "")
            {
                choiceManager.SetChoiceData(_choiceGroup);
            }
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
        }
    }
}
