using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using WHDle.Util;
using TMPro;
using WHDle.Util.Define;

public class TitleDlg : MonoBehaviour
{
    [SerializeField]
    private GameObject saveLoadPanel;

    [SerializeField]
    Button newGameButton, loadGameButton, devCloseButton;

    [SerializeField]
    private Button googleLoginButton;

    [SerializeField]
    GameObject panelAnnouncement;

    [SerializeField]
    private GameObject registerPanel;

    [SerializeField]
    private GameObject errorPanel;

    [SerializeField]
    private Button googleRegisterButton, guestRegisterButton;

    [SerializeField]
    private TMP_Text errorText;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.TitleController.LoadComplete = false;
        
        devCloseButton.onClick.AddListener(OnClick_Announcement_Button_Close);

        EnablePanelAnnouncement(false);

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration
        .Builder()
        .RequestServerAuthCode(false)
        .RequestEmail()
        .RequestIdToken()
        .Build();

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;

        PlayGamesPlatform.Activate();
        
        //
        
        var loadData = SaveDataManager.FileLoad("playerData");

        var isLoadData = loadData != null;
        
        newGameButton.interactable = true;
        loadGameButton.interactable = isLoadData;
    }

    public void EnablePanelAnnouncement(bool pBool)
    {
        panelAnnouncement.SetActive(pBool);
    }

    public void OnClick_TitleDlg_Button_LoadGame()
    {
        EnablePanelAnnouncement(true);
    }

    public void OnClick_Announcement_Button_Close()
    {
        EnablePanelAnnouncement(false);
    }

    #region Register

    public void EnableRegisterPanel()
    {
        registerPanel.gameObject.SetActive(true);

#if UNITY_EDITOR
        googleRegisterButton.interactable = false;
#endif
    }

    #endregion

    #region Button Function
    public void RegisterGoogle()
    {
        Social.localUser.Authenticate((bool success) => 
        {
            if (success)
            {
                GameManager.Instance.TitleController.LoadComplete = true;
                GameManager.Instance.loginType = LoginType.Google;
            }
            else
            {
                errorText.text = "Error";

                enableErrorPanel();
            }
        });
    }

    public void RegisterGuest()
    {
        GameManager.Instance.TitleController.LoadComplete = true;
        GameManager.Instance.loginType = LoginType.Guest;
    }

    #endregion

    #region Save_Load


    public void SaveLoadPanelEnable()
    {
        saveLoadPanel.SetActive(true);

        //FixMe
        //loadGameButton.interactable = !ServerManager.Instance.isFirstLogin;
    }

    // OnClick
    
    public void LoadNewGame()
    {
        var routineMap = new Dictionary<string, bool>();
        var inventory = new List<string>();

        routineMap.Add("WitchTalk", false);
        
        var playerData = new PlayerData(1, routineMap, "CurrentStoryGroup", "NextStoryGroup", inventory);

        SaveDataManager.FileSave(playerData, "playerData");

        LoadGame();
    }

    public void LoadGame()
    {
        var loadData = SaveDataManager.FileLoad("playerData");

        GameManager.Instance.TitleController.LoadComplete = true;
        
        GameManager.Instance.LoadScene(SceneType.GamePlay);
    }

    #endregion

    #region Error
    private void enableErrorPanel()
    {
        errorPanel.SetActive(true);
    }

    #endregion
}
