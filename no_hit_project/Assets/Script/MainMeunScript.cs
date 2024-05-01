using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMeunScript : MonoBehaviour
{
    [SerializeField] private GameObject SettingUI;
    private bool settingOpen;
    [SerializeField] private GameObject ExitGameUI;
    private bool exitGameOpen;
    private SaveManagerScript saveManager;
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void GoShopScene()
    {

        SceneManager.LoadScene(2);
    }
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
        //saveManager.SaveGame();
        Application.Quit();
    }
    private void Awake()
    {
        saveManager = GetComponent<SaveManagerScript>();
    }
    private void Start()
    {
        SettingUI.SetActive(false);
        settingOpen = false;
        ExitGameUI.SetActive(false);
        exitGameOpen = false;
    }
}
