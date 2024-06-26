using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        if (combat.monsters.Count > 0)
        {
            if (combat.buttonAttack[right].interactable)
            {
                combat.buttonAttack[right].interactable = false;
                canChange = true;
            }
            else
            {
                mainUI.warnText.text = "Action is not enough.";
            }
        }
        else
        {
            canChange = true;
        }
        if (mainUI.dataPlayer.rlHandWeapon[right] == null && mainUI.dataPlayer.rlHandShield[right] == null && canChange)
        {
            ChangeDataItemInHand(right);
        }//free hand
        else if (canChange)
        {
            if (mainUI.dataPlayer.rlHandWeapon[right] == dataWeapon)
            {
                mainUI.warnText.text = "You already equip that weapon.";
                combat.buttonAttack[right].interactable = true;
            }//same
            else if (mainUI.dataPlayer.rlHandWeapon[right] != null)
            {
                if (mainUI.dataPlayer.rlHandWeapon[right].twoHand)
                {
                    mainUI.dataPlayer.listWeapon.Add(mainUI.dataPlayer.rlHandWeapon[right]);
                    mainUI.dataPlayer.rlHandWeapon[0] = null;
                    mainUI.dataPlayer.rlHandWeapon[1] = null;
                    mainUI.dataPlayer.weaponTwoHand = false;
                    mainUI.dataPlayer.showPlayer.modelPlayer[12].color = new Color(1, 1, 1, 0);
                    mainUI.dataPlayer.showPlayer.modelPlayer[13].color = new Color(0.5f, 0.5f, 0.5f, 0);
                    ChangeDataItemInHand(right);
                }
                else
                {
                    mainUI.dataPlayer.listWeapon.Add(mainUI.dataPlayer.rlHandWeapon[right]);
                    ChangeDataItemInHand(right);
                }
            }//have weapon
            else if (mainUI.dataPlayer.rlHandShield[right] != null)//have shield
            {
                mainUI.dataPlayer.listShield.Add(mainUI.dataPlayer.rlHandShield[right]);
                mainUI.dataPlayer.rlHandShield[right] = null;
                ChangeDataItemInHand(right);
            }
        }//have something
    }
    public void UseBothHandWeapon()
    {
        if (combat.monsters.Count > 0)
        {
            if (combat.buttonAttack[0].interactable && combat.buttonAttack[1].interactable && combat.buttonAttack[2].interactable)
            {
                mainUI.dataPlayer.weaponTwoHand = false;
                mainUI.dataPlayer.showPlayer.modelPlayer[12].color = new Color(1, 1, 1, 0);
                mainUI.dataPlayer.showPlayer.modelPlayer[13].color = new Color(0.5f, 0.5f, 0.5f, 0);
                ChangeItem();
            }
            else
            {
                mainUI.warnText.text = "Action is not enough.";
            }
        }
        else
        {
            ChangeItem();
        }
    }
    public void ChangeItem()
    {
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
            ChangeDataItemInHand(0);
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
                if (mainUI.dataPlayer.rlHandWeapon[1] != null && !mainUI.dataPlayer.rlHandWeapon[0].twoHand)
                {
                    mainUI.dataPlayer.listWeapon.Add(mainUI.dataPlayer.rlHandWeapon[1]);
                }//two Hand Weapon
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
                ChangeDataItemInHand(0);
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
                ChangeDataItemInHand(0);
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
                ChangeDataItemInHand(0);
            }//shield any hand
        }//have item
        mainUI.dataPlayer.weaponTwoHand = true;
        foreach (Button item in combat.buttonAttack)
        {
            item.interactable = false;
        }
    }
    private void ChangeDataItemInHand(int i)
    {
        mainUI.dataPlayer.rlHandWeapon[i] = dataWeapon;
        mainUI.dataPlayer.diceDamage = dataWeapon.damage;
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
