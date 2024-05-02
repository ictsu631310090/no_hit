using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class shopScript : MonoBehaviour
{
    private UIScript mainUI;
    [SerializeField] private Animator merchantAni;
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
            merchantAni.SetInteger("buy", 1);
            talkText.text = "Thank you. ~ ~ ~";
        }
        else
        {
            bubbleTalk.SetActive(true);
            merchantAni.SetInteger("buy", 2);
            talkText.text = "There's not enough money.";
        }
        StartCoroutine(DelayAnimation());
    }
    IEnumerator DelayAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        merchantAni.SetInteger("buy", 0);
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
                        UIScript.addMoney = itemBuy.dataArmor.price * -1;
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
                        UIScript.addMoney = itemBuy.dataShiel.price * -1;
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
                        UIScript.addMoney = itemBuy.dataWeapon.price * -1;
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
