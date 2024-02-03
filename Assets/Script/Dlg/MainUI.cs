using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    public static MainUI Instance;

    [SerializeField] StoryManager storyManager;

    // BottomUI
    [SerializeField]
    GameObject topObject, bottomObject, interactionObject;

    [SerializeField] 
    Button interactionButton, backButton;

    // StoryUI
    [SerializeField]
    GameObject nextObject, contextObject, selectListOneObject, selectListTwoObject, selectListThreeObject, selectListFourObject;

    [SerializeField]
    Button nextButtonBackGround;

    IEnumerator storyCoroutine = null;

    void Start()
    {
        storyManager = FindObjectOfType<StoryManager>();

        // Add Event
        interactionButton.onClick.AddListener(OnClick_ButtonUI_Button_Interaction);
        nextButtonBackGround.onClick.AddListener(OnClick_Next_Button_BackGround);
        backButton.onClick.AddListener(OnClick_BottomUI_Button_Back);

        // Set
		SetShowStory(false);
    }

    void Update()
    {
        
    }

    void SetShowStory(bool pIsStoryShow){
        topObject.SetActive(!pIsStoryShow);
        bottomObject.SetActive(!pIsStoryShow);
        nextObject.SetActive(pIsStoryShow);
        contextObject.SetActive(pIsStoryShow);
    }

    void OnClick_ButtonUI_Button_Interaction()
    {
        storyManager.TempPlayStory();
    }

    void OnClick_Next_Button_BackGround()
    {
        storyManager.SetIsNextStory();
    }

    void OnClick_BottomUI_Button_Back()
    {
        SceneManager.LoadScene("Title");
    }
}
