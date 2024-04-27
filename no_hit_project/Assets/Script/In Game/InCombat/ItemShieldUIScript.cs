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
        if (mainUI.dataPlayer.rlHandWeapon[right] == null && mainUI.dataPlayer.rlHandShield[right] == null ) 
        {
            mainUI.dataPlayer.rlHandShield[right] = dataShield;
            ChangeItemInHand();
        }//free hand
        else
        {
            if (mainUI.dataPlayer.rlHandWeapon[right] != null)//have weapon
            {
                mainUI.dataPlayer.listWeapon.Add(mainUI.dataPlayer.rlHandWeapon[right]);
                ChangeItemInHand();
            }
            else if (mainUI.dataPlayer.rlHandShield[right] != null)//have shield
            {
                mainUI.warnText.text = "You're wearing a Shield.";
            }
        }
        switch (right)
        {
            case 0:
                combat.rightAttack.interactable = false;
                break;
            case 1:
                combat.leftAttack.interactable = false;
                break;
            default:
                break;
        }
    }
    private void ChangeItemInHand()
    {
        mainUI.dataPlayer.armorClass += 2;
        mainUI.dataPlayer.UpdateImageArmor();
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
