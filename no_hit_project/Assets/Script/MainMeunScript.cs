using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMeunScript : MonoBehaviour
{
    [SerializeField] private GameObject SettingUI;
    private bool settingOpen;
    [SerializeField] private GameObject ExitGameUI;
    private bool exitGameOpen;
    private SaveManagerScript saveManager;
    public void OpenSettingUI()
    {
        if (!settingOpen)
        {
            SettingUI.SetActive(true);
            settingOpen = true;
        }
        else
        {
            SettingUI.SetActive(false);
            settingOpen = false;
        }
    }
    public void OpenEixtGameUI()
    {
        if (!exitGameOpen)
        {
            ExitGameUI.SetActive(true);
            exitGameOpen = true;
        }
        else
        {
            ExitGameUI.SetActive(false);
            exitGameOpen = false;
        }
    }
    public void EixtGame()
    {
        saveManager.SaveGame();
        Application.Quit();
    }
    private void Start()
    {
        SettingUI.SetActive(false);
        settingOpen = false;
        ExitGameUI.SetActive(false);
        exitGameOpen = false;
        saveManager = GetComponent<SaveManagerScript>();
    }
}
