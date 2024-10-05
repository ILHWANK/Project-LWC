using System.Collections;
using System.Collections.Generic;
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
        DialogueType _currentDialogueType = DialogueType.ContextUp;

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

        private Dialogue[] GetStory()
        {
            var dialogueTable = new CsvTable();
            
            var groupMap = new Dictionary<string, string>
            {
                { "Dialogue_Group", "Prologue_Start" },
                { "Context_Index", "1" }
            };
            
            dialogueTable.ReadCsv("Assets/Resources/DataTable/DialogueTable.csv");
            var dialogueList = dialogueTable.GetByMultipleColumnsGroup(groupMap);
            
            CSVDataManager.Instance.SetDialogueData(_playerAction?.currentDialogueGroup);

            // int endIndex = CSVDataManager.Instance.GetEndIndex(CSVDataManager.DataType.Dialogue);
            var endIndex = dialogueList.Count;

            foreach (var dialogue in dialogueList)
            {
                Debug.Log(string.Format(dialogue["Context_CharacterName"] + " / " + dialogue["Context_Text"]));
            }
            
            //dialogueEvent.dialogues = CSVDataManager.Instance.GetDialogue(1, endIndex);

            Debug.Log("확인용 Null" + endIndex);
            
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
                    case DialogueType.ContextUp:
                    {
                        contextUpObject.SetActive(true);
                        break;
                    }
                    case DialogueType.ContextDown:
                    {
                        contextDownObject.SetActive(true);
                        break;
                    }
                    case DialogueType.Letter:
                    {
                        letterObject.SetActive(true);
                        break;
                    }
                    case DialogueType.Narration:
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

            StartCoroutine(CameraAction());
        }

        IEnumerator CameraAction()
        {
            dialogues = GetStory();

            if (isStoryPlay && dialogues != null && splashManager != null)
            {
                switch (dialogues[lineIndex].cameraActions[contextIndex])
                {
                    case CameraType.FadeOut:
                    {
                        SplashManager.isFinish = false;
                        StartCoroutine(splashManager.FadeOut(false, true));

                        yield return new WaitUntil(() => SplashManager.isFinish);
                        break;
                    }
                    case CameraType.FadeIn:
                    {
                        SplashManager.isFinish = false;
                        StartCoroutine(splashManager.FadeIn(false, true));

                        yield return new WaitUntil(() => SplashManager.isFinish);
                        break;
                    }
                    case CameraType.FlashOut:
                    {
                        SplashManager.isFinish = false;
                        StartCoroutine(splashManager.FadeOut(true, true));

                        yield return new WaitUntil(() => SplashManager.isFinish);
                        break;
                    }
                    case CameraType.FlashIn:
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
            if (dialogues[lineIndex].dialogueType != DialogueType.None)
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

            _currentDialogueType
                = dialogues[lineIndex].dialogueType != DialogueType.None ? dialogues[lineIndex].dialogueType : _currentDialogueType;

            SetDialogue(true);

            switch (_currentDialogueType)
            {
                case DialogueType.ContextUp:
                {
                    nameUpText.text = nameText;

                    break;
                }
                case DialogueType.ContextDown:
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
                        case DialogueType.ContextUp:
                        {
                            contextUpText.text += letter;
                            break;
                        }
                        case DialogueType.ContextDown:
                        {
                            contextDownText.text += letter;
                            break;
                        }
                        case DialogueType.Narration:
                        {
                            narrationText.text += letter;
                            break;
                        }
                        case DialogueType.Letter:
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
            lineIndex = 0;
            isNext = true;
            isStoryPlay = true;
            ShowStory(GetStory());
        }

        void OnClick_ButtonUI_Button_Interaction()
        {
            if (isNext)
                TempPlayStory();
        }

        void OnClick_Next_Button_BackGround()
        {
            SetIsNextStory();
        }
    }
}
