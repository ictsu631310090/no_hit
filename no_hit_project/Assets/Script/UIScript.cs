using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{
    private int moneyPlayer;
    public static int addMoney;
    [SerializeField] private TextMeshProUGUI moneyText;

    private void Start()
    {
        moneyPlayer = 0;
        moneyText.text = moneyPlayer.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (addMoney != 0)
        {
            moneyPlayer += addMoney;
            addMoney = 0;
            moneyText.text = moneyPlayer.ToString();
        }
    }
}
