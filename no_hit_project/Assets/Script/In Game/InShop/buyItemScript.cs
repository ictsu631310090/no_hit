using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class buyItemScript : MonoBehaviour
{
    [SerializeField] private Image imageItem;
    [SerializeField] private TextMeshProUGUI nameItemText;
    [HideInInspector] public int type;//0 = armor, 1 = shield, 2 = weapon
    [HideInInspector] public CreateArmorScript dataArmor;
    [HideInInspector] public CreateShieldScript dataShiel;
    [HideInInspector] public CreateWeaponScript dataWeapon;
    [HideInInspector] public shopScript shop;
    public void BuyItem()
    {
        shop.itemBuy = this;
    }
    public void CannotBuyAgain(Button button)
    {
        button.interactable = false;
    }
    private void Start()
    {
        switch (type)
        {
            case 0:
                imageItem.sprite = dataArmor.image[1];
                imageItem.SetNativeSize();
                nameItemText.text = dataArmor.nameArmor + "\n " + dataArmor.price + " gp";
                break;
            case 1:
                imageItem.sprite = dataShiel.image;
                imageItem.SetNativeSize();
                nameItemText.text = dataShiel.nameWeapon + "\n " + dataShiel.price + " gp";
                break;
            case 2:
                imageItem.sprite = dataWeapon.image;
                nameItemText.text = dataWeapon.nameWeapon + "\n " + dataWeapon.price + " gp";
                break;
            default:
                break;
        }
    }
}
