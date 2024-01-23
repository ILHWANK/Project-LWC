using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using BackEnd;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using WHDle.Util;
using TMPro;
using WHDle.Util.Define;
using WHDle.Server;

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


    #region Before Login

    public void BeforeLogin()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            GameManager.Instance.loginType = LoginType.Google;
            SkipRegister();

            return;
        }

        /*Backend.BMember.DeleteGuestInfo();*/
        var guestId = Backend.BMember.GetGuestID();

        if (guestId != null && guestId != string.Empty)
        {
            GameManager.Instance.loginType = LoginType.Guest;
            SkipRegister();

            return;
        }

        GameManager.Instance.TitleController.LoadComplete = true;
    }

    private void SkipRegister()
    {
        GameManager.Instance.TitleController.SkipRegister();
        GameManager.Instance.TitleController.LoadComplete = true;

        ServerManager.Instance.isFirstLogin = false;
    }

    #endregion

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
                errorText.text = "구글 회원가입 실패";

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

    #region AfterLogin

    public void AfterLogin()
    {
        registerPanel.gameObject.SetActive(false);

        switch (GameManager.Instance.loginType)
        {
            case LoginType.Google:
                googleLogin();
                break;
            case LoginType.Guest:
                guestLogin();
                break;
        }
    }

    private void googleLogin()
    {
        Backend.BMember.AuthorizeFederation(getToken(), FederationType.Google, callback =>
        {
            if (callback.IsSuccess())
            {
                GameManager.Instance.TitleController.LoadComplete = true;
            }
            else
            {
                errorText.text = "구글 로그인 에러!";
                enableErrorPanel();
            }
        });
    }

    private void guestLogin()
    {
        Backend.BMember.GuestLogin("GuestLogin", callback =>
        {
            if (callback.IsSuccess())
            {
                GameManager.Instance.TitleController.LoadComplete = true;
            }
            else
            {
                errorText.text = "게스트 로그인 에러!";
                enableErrorPanel();
            }
        });
    }

    #endregion

    #region Save_Load

    public void SaveLoadPanelEnable()
    {
        saveLoadPanel.SetActive(true);

        loadGameButton.interactable = !ServerManager.Instance.isFirstLogin;
    }

    public void LoadNewGame()
    {
        ServerManager.Instance.isFirstLogin = true;
        LoadGame();
    }

    public void LoadGame()
    {
        saveLoadPanel.SetActive(false);

        GameManager.Instance.TitleController.LoadComplete = true;
    }

    #endregion

    #region Error
    private void enableErrorPanel()
    {
        errorPanel.SetActive(true);
    }

    #endregion

    private string getToken()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            string _IDtoken = PlayGamesPlatform.Instance.GetIdToken();

            if (ServerManager.Instance.isFirstLogin && _IDtoken.Length <= 0)
                _IDtoken = PlayGamesPlatform.Instance.GetIdToken();

            return _IDtoken;
        }

        return string.Empty;
    }
}
