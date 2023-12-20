using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using BackEnd;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class TitleDlg : MonoBehaviour
{
    [SerializeField]
    Button newGameButton, loadGameButton, devCloseButton;

    [SerializeField]
    private Button googleLoginButton;

    [SerializeField]
    GameObject panelAnnouncement;

    //TEST
    [SerializeField]
    private Text text;


    // Start is called before the first frame update
    void Start()
    {
        //Add Event
        newGameButton.onClick.AddListener(OnClick_TitleDlg_Button_NewGame);
        loadGameButton.onClick.AddListener(OnClick_TitleDlg_Button_LoadGame);
        devCloseButton.onClick.AddListener(OnClick_Announcement_Button_Close);

        EnablePanelAnnouncement(false);

        newGameButton.interactable = false;
        loadGameButton.interactable = false;
        devCloseButton.interactable = false;
        googleLoginButton.interactable = false;

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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnablePanelAnnouncement(bool pBool)
    {
        panelAnnouncement.SetActive(pBool);
    }

    public void OnClick_TitleDlg_Button_NewGame()
    {
        SceneManager.LoadScene("Loading");     
    }

    public void OnClick_TitleDlg_Button_LoadGame()
    {
        EnablePanelAnnouncement(true);
    }

    public void OnClick_Announcement_Button_Close()
    {
        EnablePanelAnnouncement(false);
    }

    public void EnableGoogleLoginButton() => googleLoginButton.interactable = true;

    public void GoogleLogin()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            Debug.Log($"success = {success}");

            if (!success)
            {
                Debug.LogError("Failure reason: ");
            }
        });

        Backend.BMember.AuthorizeFederation(getToken(true), FederationType.Google, callback =>
        {
            if (callback.IsSuccess())
            {
                text.text = "LOGIN SUCCESS";
            }
            else
            {
                text.text = $"localUser = {PlayGamesPlatform.Instance.localUser}\n" 
                                + $"authenticated = {PlayGamesPlatform.Instance.localUser.authenticated}\n"
                                + $"token = {PlayGamesPlatform.Instance.GetIdToken()}"; 
            }
        });
    }

    private string getToken(bool isFirst)
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            string _IDtoken = PlayGamesPlatform.Instance.GetIdToken();

            return _IDtoken;
        }
        else
        {
            var token = isFirst ? getToken(false) : null;
            return token;
        }
    }
}
