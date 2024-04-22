using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class UIScript : MonoBehaviour
{
    public Animator blackScene;
    private int moneyPlayer;
    public static int addMoney;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private GameObject mapUI;
    private bool openMap;
    [SerializeField] private GameObject bagUI;
    private bool openBag;
    [HideInInspector] public bool nextScene;
    public void OpenMapUI()
    {
        if (!openMap)
        {
            mapUI.SetActive(true);
            openMap = true;
        }
        else
        {
            mapUI.SetActive(false);
            openMap = false;
        }
    }
    public void OpenBagUI()
    {
        if (!openBag)
        {
            bagUI.SetActive(true);
            openBag = true;
        }
        else
        {
            bagUI.SetActive(false);
            openBag = false;
        }
    }
    private void CloseScene()
    {
        if (nextScene)
        {
            blackScene.SetBool("close", true);
            StartCoroutine(DelayChangeScene());
        }
    }
    IEnumerator DelayChangeScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);//main menu test
    }
    private void Start()
    {
        moneyPlayer = 0;

        moneyText.text = moneyPlayer.ToString();
        mapUI.SetActive(false);
        openMap = false;
        bagUI.SetActive(false);
        openBag = false;

        nextScene = false;
    }
    void Update()
    {
        if (addMoney != 0)
        {
            moneyPlayer += addMoney;
            addMoney = 0;
            moneyText.text = moneyPlayer.ToString();
        }
        CloseScene();
    }
}
