using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIScript : MonoBehaviour
{
    [SerializeField] private GameObject blackScene;
    [SerializeField] private AnimationClip animationBlack;
    private float timeAnimaBlack;
    private int moneyPlayer;
    public static int addMoney;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private GameObject mapUI;
    private bool openMap;
    [SerializeField] private GameObject bagUI;
    private bool openBag;
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
    private void Start()
    {
        moneyPlayer = 0;
        moneyText.text = moneyPlayer.ToString();
        mapUI.SetActive(false);
        openMap = false;
        bagUI.SetActive(false);
        openBag = false;

        timeAnimaBlack = animationBlack.length;
        blackScene.SetActive(true);
    }
    void Update()
    {
        if (addMoney != 0)
        {
            moneyPlayer += addMoney;
            addMoney = 0;
            moneyText.text = moneyPlayer.ToString();
        }

        if (timeAnimaBlack >= 0)
        {
            timeAnimaBlack -= Time.deltaTime;
            if (timeAnimaBlack <= 0)
            {
                blackScene.SetActive(false);
            }
        }
    }
}
