using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    // BottomUI
    [SerializeField]
    GameObject topObject, bottomObject;

    // StoryUI
    [SerializeField]
    GameObject nextObject, characterObject, contextObject, selectListOneObject, selectListTwoObject, selectListThreeObject, selectListFourObject;

    [SerializeField]
    Button nextButtonBackGround, selectListOneButton, selectListTwoButton, selectListThreeButton, selectListFourButton;

    [SerializeField]
    Text backgroundText, nameText;

    [SerializeField]
    Transform tempTaregt;

    [SerializeField]
    Camera tempCamera;

    //
    [SerializeField] StoryEvent storyEvent;

    Story[] storys;
    bool isStoryPlay = false;
    bool isNext = false;
    bool isNextStory = false;
    bool isStoryContextEnd = false;

    int lineIndex = 0;
    int contextIndex = 0;
    float textDelay = 0.05f;

    IEnumerator storyCoroutine = null;

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
        // Set
        SetShowStory(false);

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

                backgroundText.text = "";
                nameText.text = "";

                storyCoroutine = StoryPlayCoroutine();

                if (++contextIndex < storys[lineIndex].contexts.Length)
                {
                    StartCoroutine(storyCoroutine);
                    StartCoroutine(CameraAction());
                }
                else
                {
                    contextIndex = 0;
                    if (++lineIndex < storys.Length)
                    {
                        StartCoroutine(storyCoroutine);
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

    public Story[] GetStory()
    {
        int endIndex = CSVDataManager.Instance.GetEndIndex();

        storyEvent.storys = CSVDataManager.Instance.GetStory(1, endIndex);

        return storyEvent.storys;
    }

    void SetShowStory(bool pIsStoryShow)
    {
        topObject.SetActive(!pIsStoryShow);
        bottomObject.SetActive(!pIsStoryShow);
        nextObject.SetActive(pIsStoryShow);
        characterObject.SetActive(pIsStoryShow);
        contextObject.SetActive(pIsStoryShow);

        StartCoroutine(CameraAction());
    }

    IEnumerator CameraAction()
    {
        storys = GetStory();

        if (isStoryPlay && storys != null && splashManager != null)
        {
            switch (storys[lineIndex].cameraActions[contextIndex])
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

    void EndStory()
    {
        isStoryPlay = false;
        contextIndex = 0;
        lineIndex = 0;
        storys = null;
        isNext = false;

        SetShowStory(false);
    }

    void ShowStory(Story[] pStorys)
    {
        SetShowStory(true);

        backgroundText.text = "";
        nameText.text = "";

        storys = pStorys;

        if (storyCoroutine != null)
        {
            storyCoroutine = null;
        }

        storyCoroutine = StoryPlayCoroutine();
        StartCoroutine(storyCoroutine);
    }

    void ChangeSprite()
    {
        if(storys[lineIndex].spriteNames[contextIndex] != "")
        {
            StartCoroutine(spriteManager.SpriteChangeCoroutine(tempTaregt, storys[lineIndex].spriteNames[contextIndex]));
        }
    }

    IEnumerator StoryPlayCoroutine()
    {
        ChangeSprite();

        string context = storys[lineIndex].contexts[contextIndex];

        context = context.Replace("\\", ",");

        FontConfiguration fontConfiguration = new FontConfiguration();

        fontConfiguration.fontColor = FontColor.Black;

        nameText.text = storys[lineIndex].characterName;

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

                backgroundText.text += letter;

                yield return new WaitForSeconds(textDelay);
            }
            else
            {
                continue;
            }
        }

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
                    //pLetter = "<i>" + pLetter + "</i>";
                    //pLetter = "<b>" + pLetter + "</b>";

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

    public void SetIsNextStory()
    {
        isNextStory = true;
    }

    public void TempPlayStory()
    {
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
