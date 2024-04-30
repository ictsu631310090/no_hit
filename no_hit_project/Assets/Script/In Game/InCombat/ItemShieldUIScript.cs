using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemShieldUIScript : MonoBehaviour
{
    public CreateShieldScript dataShield;
    [SerializeField] private TextMeshProUGUI nameItemText;
    [SerializeField] private TextMeshProUGUI addOnText;
    [SerializeField] private TextMeshProUGUI detailText;
    [HideInInspector] public UIScript mainUI;
    [HideInInspector] public CombatScript combat;
    public void CilckLookImage()
    {
        mainUI.warnText.text = null;
        mainUI.ImageItemShow(dataShield.image);
    }
    public void UseShield(int right)//r = 0, L = 1
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
            mainUI.dataPlayer.rlHandShield[right] = dataShield;
            ChangeDataItemInHand(right);
        }//free hand
        else if (canChange)
        {
            if (mainUI.dataPlayer.rlHandWeapon[right] != null)//have weapon
            {
                mainUI.dataPlayer.listWeapon.Add(mainUI.dataPlayer.rlHandWeapon[right]);
                mainUI.dataPlayer.rlHandShield[right] = dataShield;
                ChangeDataItemInHand(right);
            }
            else if (mainUI.dataPlayer.rlHandShield[right] != null)//have shield
            {
                mainUI.warnText.text = "You're wearing a Shield.";
            }
        }//have something
    }
    private void ChangeDataItemInHand(int i)
    {
        mainUI.dataPlayer.armorClass += 2;
        mainUI.dataPlayer.UpdateImageWeapon(i , false);
        mainUI.OpenBagUI();//close
        mainUI.dataPlayer.listShield.Remove(dataShield);
    }
    private void Start()
    {
        nameItemText.text = dataShield.nameWeapon;
        detailText.text = " - ";
        addOnText.text = "+" + dataShield.addAC.ToString() + "AC";
    }
}
