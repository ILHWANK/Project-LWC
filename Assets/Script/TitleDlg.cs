using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleDlg : MonoBehaviour
{
    Button newGameButton, loadGameButton, devCloseButton;

    GameObject panelAnnouncement;

    // Start is called before the first frame update
    void Start()
    {
        // Button
        newGameButton  = GameObject.Find("TitleDlg_Button_NewGame").GetComponent<Button>();
        loadGameButton = GameObject.Find("TitleDlg_Button_LoadGame").GetComponent<Button>();
        devCloseButton = GameObject.Find("Announcement_Button_Close").GetComponent<Button>();

        //Panel
        panelAnnouncement = GameObject.Find("Announcement_Panel_Dev");

        //Add Event
        newGameButton.onClick.AddListener(OnClick_TitleDlg_Button_NewGame);
        loadGameButton.onClick.AddListener(OnClick_TitleDlg_Button_LoadGame);
        devCloseButton.onClick.AddListener(OnClick_Announcement_Button_Close);

        EnablePanelAnnouncement(false);

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
}
