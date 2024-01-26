using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    // BottomUI
    [SerializeField]
    GameObject bottomObject;

    // StoryUI
    [SerializeField]
    GameObject nextObject, characterObject, contextObject, selectListOneObject, selectListTwoObject, selectListThreeObject, selectListFourObject;

    [SerializeField]
    Button nextButtonBackGround, selectListOneButton, selectListTwoButton, selectListThreeButton, selectListFourButton;

    [SerializeField]
    Text backgroundText, nameText;

    [SerializeField]
    Transform tempTaregt;

    //
    [SerializeField] StoryEvent storyEvent;

    Story[] storys;
    bool isStoryPlay = false;
    bool isNext = false;
    bool isNextStory = false;

    int lineIndex = 0;
    int contextIndex = 0;
    float textDelay = 0.05f;

    IEnumerator storyCoroutine = null;

    SpriteManager spriteManager;

    void Start()
    {
        // Set
        SetShowStory(false);

        spriteManager = FindObjectOfType<SpriteManager>();
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
                }
                else
                {
                    contextIndex = 0;
                    if (++lineIndex < storys.Length)
                    {
                        StartCoroutine(storyCoroutine);
                    }
                    else
                    {
                        EndStory();
                    }
                }
            }
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
        bottomObject.SetActive(!pIsStoryShow);
        nextObject.SetActive(pIsStoryShow);
        characterObject.SetActive(pIsStoryShow);
        contextObject.SetActive(pIsStoryShow);
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
        if(storys[lineIndex].spriteName[contextIndex] != "")
        {
            StartCoroutine(spriteManager.SpriteChangeCoroutine(tempTaregt, storys[lineIndex].spriteName[contextIndex]));
        }
    }

    IEnumerator StoryPlayCoroutine()
    {
        ChangeSprite();

        string context = storys[lineIndex].contexts[contextIndex];

        context = context.Replace("\\", ",");

        nameText.text = storys[lineIndex].characterName;

        for (int i = 0; i < context.Length; ++i)
        {
            backgroundText.text += context[i];
            //backgroundText.text = context;

            yield return new WaitForSeconds(textDelay);
        }

        isNext = true;
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
        TempPlayStory();
    }

    void OnClick_Next_Button_BackGround()
    {
        SetIsNextStory();
    }
}
