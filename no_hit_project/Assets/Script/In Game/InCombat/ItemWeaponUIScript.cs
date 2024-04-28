using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemWeaponUIScript : MonoBehaviour
{
    public CreateWeaponScript dataWeapon;
    [SerializeField] private GameObject twoButton; //can equip two Hand
    [SerializeField] private GameObject threeButton; //versatile
    [SerializeField] private GameObject oneButton; // two hand
    [SerializeField] private TextMeshProUGUI nameItemText;
    [SerializeField] private TextMeshProUGUI addOnText;
    [SerializeField] private TextMeshProUGUI detailText;
    [HideInInspector] public UIScript mainUI;
    [HideInInspector] public CombatScript combat;
    public void CilckLookImage()
    {
        mainUI.warnText.text = null;
        mainUI.ImageItemShow(dataWeapon.image);
    }
    public void UseTwoHandWeapon(int right)//r = 0, L = 1
    {
        bool canChange = false;
        switch (right)
        {
            case 0:
                if (combat.rightAttack.interactable)
                {
                    combat.rightAttack.interactable = false;
                    canChange = true;
                }
                else
                {
                    mainUI.warnText.text = "Action is not enough.";
                }
                break;
            case 1:
                if (combat.leftAttack.interactable)
                {
                    canChange = true;
                    combat.leftAttack.interactable = false;
                }
                else
                {
                    mainUI.warnText.text = "Action is not enough.";
                }
                break;
            default:
                break;
        }
        if (mainUI.dataPlayer.rlHandWeapon[right] == null && mainUI.dataPlayer.rlHandShield[right] == null && canChange)
        {
            mainUI.dataPlayer.rlHandWeapon[right] = dataWeapon;
            mainUI.dataPlayer.diceDamage = dataWeapon.damage;
            ChangeItemInHand(right);
        }//free hand
        else if (canChange)
        {
            if (mainUI.dataPlayer.rlHandWeapon[right] == dataWeapon)
            {
                mainUI.warnText.text = "You already equip that weapon.";
            }
            else if (mainUI.dataPlayer.rlHandWeapon[right] != null)//have weapon
            {
                mainUI.dataPlayer.listWeapon.Add(mainUI.dataPlayer.rlHandWeapon[right]);
                mainUI.dataPlayer.rlHandWeapon[right] = dataWeapon;
                mainUI.dataPlayer.diceDamage = dataWeapon.damage;
                ChangeItemInHand(right);
            }
            else if (mainUI.dataPlayer.rlHandShield[right] != null)//have shield
            {
                mainUI.dataPlayer.listShield.Add(mainUI.dataPlayer.rlHandShield[right]);
                mainUI.dataPlayer.rlHandWeapon[right] = dataWeapon;
                mainUI.dataPlayer.diceDamage = dataWeapon.damage;
                ChangeItemInHand(right);
            }
        }//have something
    }
    public void UseBothHandWeapon()
    {
        if (combat.rightAttack.interactable && combat.rightAttack.interactable && combat.rightAttack.interactable)
        {
            mainUI.dataPlayer.weaponTwoHand = true;
            if (mainUI.dataPlayer.rlHandWeapon[0] == null && mainUI.dataPlayer.rlHandWeapon[1] == null)
            {
                mainUI.dataPlayer.rlHandWeapon[0] = dataWeapon;
                mainUI.dataPlayer.rlHandWeapon[1] = dataWeapon;
                if (!dataWeapon.canTwoHand)
                {
                    mainUI.dataPlayer.diceDamage = dataWeapon.damage;
                }
                else
                {
                    mainUI.dataPlayer.diceDamage = dataWeapon.damageTwoHand;
                }
                ChangeItemInHand(0);
            }//free hand
            else
            {
                if (mainUI.dataPlayer.rlHandWeapon[0] == dataWeapon)
                {
                    mainUI.warnText.text = "You already equip that weapon.";
                }
                else if (mainUI.dataPlayer.rlHandWeapon[0] != null)
                {
                    mainUI.dataPlayer.listWeapon.Add(mainUI.dataPlayer.rlHandWeapon[0]);
                    mainUI.dataPlayer.rlHandWeapon[0] = dataWeapon;
                    mainUI.dataPlayer.rlHandWeapon[1] = dataWeapon;
                    if (!dataWeapon.canTwoHand)
                    {
                        mainUI.dataPlayer.diceDamage = dataWeapon.damage;
                    }
                    else
                    {
                        mainUI.dataPlayer.diceDamage = dataWeapon.damageTwoHand;
                    }
                    ChangeItemInHand(0);
                }//right hand
                else if (mainUI.dataPlayer.rlHandWeapon[1] != null)
                {
                    mainUI.dataPlayer.listWeapon.Add(mainUI.dataPlayer.rlHandWeapon[1]);
                    mainUI.dataPlayer.rlHandWeapon[0] = dataWeapon;
                    mainUI.dataPlayer.rlHandWeapon[1] = dataWeapon;
                    if (!dataWeapon.canTwoHand)
                    {
                        mainUI.dataPlayer.diceDamage = dataWeapon.damage;
                    }
                    else
                    {
                        mainUI.dataPlayer.diceDamage = dataWeapon.damageTwoHand;
                    }
                    ChangeItemInHand(0);
                }//left hand
                else if (mainUI.dataPlayer.rlHandShield[0] != null || mainUI.dataPlayer.rlHandShield[1] != null)//have shield
                {
                    if (mainUI.dataPlayer.rlHandShield[0] != null)
                    {
                        mainUI.dataPlayer.listShield.Add(mainUI.dataPlayer.rlHandShield[0]);
                    }
                    if (mainUI.dataPlayer.rlHandShield[1] != null)
                    {
                        mainUI.dataPlayer.listShield.Add(mainUI.dataPlayer.rlHandShield[1]);
                    }
                    mainUI.dataPlayer.rlHandWeapon[0] = dataWeapon;
                    mainUI.dataPlayer.rlHandWeapon[1] = dataWeapon;
                    if (!dataWeapon.canTwoHand)
                    {
                        mainUI.dataPlayer.diceDamage = dataWeapon.damage;
                    }
                    else
                    {
                        mainUI.dataPlayer.diceDamage = dataWeapon.damageTwoHand;
                    }
                    ChangeItemInHand(0);
                }//shield any hand
            }//have item
            combat.rightAttack.interactable = false;
            combat.leftAttack.interactable = false;
            combat.bothAttack.interactable = false;
        }
        else
        {
            mainUI.warnText.text = "Action is not enough.";
        }
    }
    public void Versatile()
    {

    }
    private void ChangeItemInHand(int i)
    {
        mainUI.dataPlayer.UpdateImageWeapon(i, true);
        mainUI.OpenBagUI();//close
        mainUI.dataPlayer.listWeapon.Remove(dataWeapon);
        mainUI.dataPlayer.showPlayer.UpdateACText();
    }
    private void Start()
    {
        twoButton.SetActive(false);
        threeButton.SetActive(false);
        oneButton.SetActive(false);

        nameItemText.text = dataWeapon.nameWeapon;
        addOnText.text = "Damage D" + dataWeapon.damage.ToString();
        if (dataWeapon.finesse)
        {
            detailText.text = "Finesse";
            twoButton.SetActive(true);
        }
        else if (dataWeapon.twoHand)
        {
            detailText.text = "TwoHand";
            oneButton.SetActive(true);
        }
        else if (dataWeapon.canTwoHand)
        {
            detailText.text = "Versatile D" + dataWeapon.damageTwoHand.ToString();
            threeButton.SetActive(true);
        }
        else
        {
            detailText.text = " - ";
            twoButton.SetActive(true);
        }
    }
}
