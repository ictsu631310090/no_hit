using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMeunScript : MonoBehaviour
{
    [SerializeField] private GameObject settingUI;
    private bool settingOpen;
    [SerializeField] private GameObject exitGameUI;
    private bool exitGameOpen;
    private SaveManagerScript saveManager;
    //1 = cut scene, 2 = Map, 3 = combat, 4 = shop, 5 = rest, 6 = event
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void MapScene()
    {
        SceneManager.LoadScene(2);
    }
    public void GoShopScene()
    {
        SceneManager.LoadScene(4);
    }
    public void GoRestScene()
    {
        SceneManager.LoadScene(5);
    }
    public void OpenSettingUI()
    {
        if (!settingOpen)
        {
            settingUI.SetActive(true);
            settingOpen = true;
        }
        else
        {
            settingUI.SetActive(false);
            settingOpen = false;
        }
    }
    public void OpenEixtGameUI()
    {
        if (!exitGameOpen)
        {
            exitGameUI.SetActive(true);
            exitGameOpen = true;
        }
        else
        {
            exitGameUI.SetActive(false);
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
        if (saveManager != null)
        {
            saveManager = GetComponent<SaveManagerScript>();
        }
    }
    private void Start()
    {
        settingOpen = false;
        exitGameOpen = false;
        if (settingUI != null)
        {
            settingUI.SetActive(false);
        }
        if (exitGameUI != null)
        {
            exitGameUI.SetActive(false);
        }
    }
}
