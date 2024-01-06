using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    public static MainUI Instance;

    [SerializeField] StoryEvent storyEvent;

    [SerializeField]
    // BottomUI
    GameObject bottomObject;

    [SerializeField] 
    Button interactionButton, backButton;

    [SerializeField]
    GameObject interactionObject;

    // StoryUI
    [SerializeField]
    GameObject nextObject, contextObject, selectListOneObject, selectListTwoObject, selectListThreeObject, selectListFourObject;

    [SerializeField] 
    Button nextButtonBackGround, selectListOneButton, selectListTwoButton, selectListThreeButton, selectListFourButton;

    [SerializeField] 
    Text backgroundText, nameText;

    //
    Story[] storys;
    bool isStoryPlay = false;
    bool isNext = false;
    bool isNextStory = false;

    int lineInext = 0;
    int contextIndex = 0;
    float textDelay = 0.05f;

    IEnumerator storyCoroutine = null;

    // Start is called before the first frame update
    void Start()
    {
        // Add Event
        interactionButton.onClick.AddListener(OnClick_ButtonUI_Button_Interaction);
        backButton.onClick.AddListener(OnClick_BottomUI_Button_Back);
        nextButtonBackGround.onClick.AddListener(OnClick_Next_Button_BackGround);

        // Set
        SetShowStory(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isStoryPlay){
            if (isNext && isNextStory){
                isNext = false;
                isNextStory = false;

                backgroundText.text = "";
                nameText.text = "";

                storyCoroutine = StoryPlayCoroutine();

                if (++contextIndex < storys[lineInext].contexts.Length){
                    StartCoroutine(storyCoroutine);
                }
                else {
                    contextIndex = 0;
                    if (++lineInext < storys.Length){
                        StartCoroutine(storyCoroutine);
                    }
                    else {
                        EndStory();
                    }
                }
            }
        }
    }

    public Story[] GetStory(){
        int endIndex = CSVDataManager.Instance.GetEndIndex();

        storyEvent.storys = CSVDataManager.Instance.GetStory(1, endIndex);

        return storyEvent.storys;
    }

    void SetShowStory(bool pIsStoryShow){
        bottomObject.SetActive(!pIsStoryShow);
        nextObject.SetActive(pIsStoryShow);
        contextObject.SetActive(pIsStoryShow);
    }

    void EndStory(){
        isStoryPlay = false;
        contextIndex = 0;
        lineInext = 0;
        storys = null;
        isNext = false;

        SetShowStory(false);
    }

    void ShowStory(Story[] pStorys){
        SetShowStory(true);

        backgroundText.text = "";
        nameText.text = "";

        storys = pStorys;

        if (storyCoroutine != null){
            storyCoroutine = null;            
        }

        storyCoroutine = StoryPlayCoroutine();
        StartCoroutine(storyCoroutine);
    }

    IEnumerator StoryPlayCoroutine(){
        string context = storys[lineInext].contexts[contextIndex];
        
        //context = backgroundText.Replace("", "");

        nameText.text = storys[lineInext].characterName;

        for (int i = 0 ; i < context.Length ; ++i){
            backgroundText.text += context[i];   

            yield return new WaitForSeconds(textDelay);
            //backgroundText.text = context;
        }

        isNext = true;
    }

    void OnClick_ButtonUI_Button_Interaction()
    {
        // temp Story
        isNext = true;
        isStoryPlay = true;
        ShowStory(GetStory());
    }

    void OnClick_BottomUI_Button_Back()
    {
        SceneManager.LoadScene("Title");
    }

    void OnClick_Next_Button_BackGround()
    {
        isNextStory = true;
    }
}
