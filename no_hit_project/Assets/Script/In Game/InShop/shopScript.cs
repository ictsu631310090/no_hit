using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class shopScript : MonoBehaviour
{
    private UIScript mainUI;
    [SerializeField] private GameObject bubbleTalk;
    private TextMeshProUGUI talkText;
    [SerializeField] private CreateArmorScript[] armorCanSell;
    [SerializeField] private CreateShieldScript[] shieldCanSell;
    [SerializeField] private CreateWeaponScript[] weaponCanSell;
    [SerializeField] private buyItemScript buttonBuy;
    [SerializeField] private Transform buyUI;
    private GameObject[] allButtonBuy = new GameObject[4];
    [HideInInspector] public buyItemScript itemBuy;
    private void RandromItemShop()
    {
        bool haveShield = false;
        int haveTwoArmor = 0;
        for (int i = 0; i < allButtonBuy.Length; i++)
        {
            int a = armorCanSell.Length + shieldCanSell.Length + weaponCanSell.Length;
            int r = Random.Range(0, a);
            if (r < armorCanSell.Length && haveTwoArmor < 2)
            {
                haveTwoArmor++;
                GameObject bb = Instantiate(buttonBuy.gameObject, buyUI, false);
                bb.GetComponent<buyItemScript>().shop = this;
                bb.GetComponent<buyItemScript>().type = 0;
                bb.GetComponent<buyItemScript>().dataArmor = armorCanSell[r];
                allButtonBuy[i] = bb;
            }
            else if (r < armorCanSell.Length && haveTwoArmor >= 2)//just 2 armor
            {
                GameObject bb = Instantiate(buttonBuy.gameObject, buyUI, false);
                bb.GetComponent<buyItemScript>().shop = this;
                bb.GetComponent<buyItemScript>().type = 2;
                bb.GetComponent<buyItemScript>().dataWeapon = weaponCanSell[0];
                allButtonBuy[i] = bb;
            }
            else if (r < armorCanSell.Length + shieldCanSell.Length && !haveShield)
            {
                haveShield = true;
                GameObject bb = Instantiate(buttonBuy.gameObject, buyUI, false);
                bb.GetComponent<buyItemScript>().shop = this;
                bb.GetComponent<buyItemScript>().type = 1;
                bb.GetComponent<buyItemScript>().dataShiel = shieldCanSell[r - armorCanSell.Length];
                allButtonBuy[i] = bb;
            }
            else if (r < armorCanSell.Length + shieldCanSell.Length && haveShield)//no 2 shield
            {
                GameObject bb = Instantiate(buttonBuy.gameObject, buyUI, false);
                bb.GetComponent<buyItemScript>().shop = this;
                bb.GetComponent<buyItemScript>().type = 2;
                bb.GetComponent<buyItemScript>().dataWeapon = weaponCanSell[0];
                allButtonBuy[i] = bb;
            }
            else
            {
                GameObject bb = Instantiate(buttonBuy.gameObject, buyUI, false);
                bb.GetComponent<buyItemScript>().shop = this;
                bb.GetComponent<buyItemScript>().type = 2;
                bb.GetComponent<buyItemScript>().dataWeapon = weaponCanSell[r - (armorCanSell.Length + shieldCanSell.Length)];
                allButtonBuy[i] = bb;
            }
        }
        
    }
    private void TextTelk(bool canbuy)
    {
        if (canbuy)
        {
            bubbleTalk.SetActive(true);
            talkText.text = "Thank you. ~ ~ ~";
        }
        else
        {
            bubbleTalk.SetActive(true);
            talkText.text = "There's not enough money.";
        }
    }
    private void Awake()
    {
        mainUI = this.GetComponent<UIScript>();
    }
    private void Start()
    {
        bubbleTalk.SetActive(false);
        talkText = bubbleTalk.GetComponentInChildren<TextMeshProUGUI>();
        RandromItemShop();
    }
    private void Update()
    {
        if (itemBuy != null)
        {
            switch (itemBuy.type)
            {
                case 0:
                    if (itemBuy.dataArmor.price > mainUI.moneyPlayer)
                    {
                        TextTelk(false);
                    }
                    else
                    {
                        mainUI.moneyPlayer -= itemBuy.dataArmor.price;
                        mainUI.dataPlayer.listArmor.Add(itemBuy.dataArmor);
                        TextTelk(true);
                    }
                    break;
                case 1:
                    if (itemBuy.dataShiel.price > mainUI.moneyPlayer)
                    {
                        TextTelk(false);
                    }
                    else
                    {
                        mainUI.moneyPlayer -= itemBuy.dataShiel.price;
                        mainUI.dataPlayer.listShield.Add(itemBuy.dataShiel);
                        TextTelk(true);
                    }
                    break;
                case 2:
                    if (itemBuy.dataWeapon.price > mainUI.moneyPlayer)
                    {
                        TextTelk(false);
                    }
                    else
                    {
                        mainUI.moneyPlayer -= itemBuy.dataWeapon.price;
                        mainUI.dataPlayer.listWeapon.Add(itemBuy.dataWeapon);
                        TextTelk(true);
                    }
                    break;
                default:
                    break;
            }
            itemBuy = null;
        }
    }
}
