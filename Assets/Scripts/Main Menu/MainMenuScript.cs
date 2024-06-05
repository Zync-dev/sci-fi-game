using JetBrains.Annotations;
using Michsky.UI.Shift;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuScript : MonoBehaviour
{
    public GameObject playSwitch;

    SwitchManager switchManager;

    public GameObject LoadingScreen;

    public GameObject settingsPanel;
    public GameObject informationPanel;
    public GameObject creditsPanel;

    public GameObject playBtn;

    // BTNS
    bool playBtnClick = false;
    bool endless = false;
    bool story = true;

    private void Start()
    {
        switchManager = playSwitch.GetComponent<SwitchManager>();
    }

    private void Update()
    {
        if(switchManager.isOn != true)
        {
            GameObject.FindGameObjectWithTag("PlayDescription").GetComponent<TMP_Text>().text = "ENDLESS ER IKKE TILGÆNGELIGT";
            playBtn.GetComponent<Button>().interactable = false;
        } else if (switchManager.isOn)
        {
            GameObject.FindGameObjectWithTag("PlayDescription").GetComponent<TMP_Text>().text = "VÆLG MELLEM ENDLESS OG STORY";
            playBtn.GetComponent<Button>().interactable = true;
        }
    }

    public void PlayBtn()
    {
        StartFadeAnim();

        playBtnClick = true;

        if(switchManager.isOn == true)
        {
            story = true;
        } else if( switchManager.isOn == false)
        {
            endless = true;
        }
    }

    void StartFadeAnim()
    {
        LoadingScreen.SetActive(true);
        LoadingScreen.GetComponent<Animator>().Play("LoadingAnimIn");
    }

    public void OnLoadInFinish()
    {
        if (playBtnClick == true)
        {
            if (endless == true)
            {
                SceneManager.LoadScene("Endless");
            }
            else if (story == true)
            {
                SceneManager.LoadScene("Storymode");
            }
        }
    }

    public void SettingsBtnClick()
    {
        settingsPanel.SetActive(true);
        settingsPanel.GetComponent<Animator>().Play("PanelIn");
    }

    public void InformationBtnClick()
    {
        informationPanel.SetActive(true);
        informationPanel.GetComponent<Animator>().Play("PanelIn");
    }

    public void CreditsBtnClick()
    {
        creditsPanel.SetActive(true);
        creditsPanel.GetComponent<Animator>().Play("PanelIn");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void InfoBtn()
    {
        Application.OpenURL("https://sexogsamfund.dk");
    }
}
