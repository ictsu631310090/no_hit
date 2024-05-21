using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemArmorUIScript : MonoBehaviour
{
    public CreateArmorScript dataArmor;
    [SerializeField] private TextMeshProUGUI nameItemText;
    [SerializeField] private TextMeshProUGUI addOnText;
    [SerializeField] private TextMeshProUGUI detailText;
    [HideInInspector] public UIScript mainUI;
    [HideInInspector] public CombatScript combat;
    public void CilckLookImage()
    {
        mainUI.warnText.text = null;
        mainUI.ImageItemShow(dataArmor.image[1]);
    }
    public void EquipArmor()
    {
        if (combat.monsters.Count > 0)
        {
            if (combat.buttonAttack[0].interactable && combat.buttonAttack[1].interactable && combat.buttonAttack[2].interactable)
            {
                ChangeArmor();
            }
            else
            {
                mainUI.warnText.text = "Action is not enough.";
            }
        }
        else
        {
            ChangeArmor();
        }
    }
    private void ChangeArmor()
    {
        int dexMo = ((mainUI.dataPlayer.dex - 10) / 2);
        if (mainUI.dataPlayer.armorUse == null)
        {
            mainUI.dataPlayer.armorUse = dataArmor;
            if (dataArmor.light)
            {
                mainUI.dataPlayer.armorClass = dataArmor.setAC + dexMo;
                ChangeDataArmor();
            }
            else if (dataArmor.heavy && mainUI.dataPlayer.str >= dataArmor.condition)
            {
                mainUI.dataPlayer.armorClass = dataArmor.setAC;
                ChangeDataArmor();
            }
            else if (dataArmor.heavy && mainUI.dataPlayer.str < dataArmor.condition)
            {
                mainUI.warnText.text = "Not enough STR.";
            }
            else if (!dataArmor.heavy && !dataArmor.light)
            {
                if (dexMo >= 2)
                {
                    mainUI.dataPlayer.armorClass = dataArmor.setAC + 2;
                }
                else
                {
                    mainUI.dataPlayer.armorClass = dataArmor.setAC + dexMo;
                }
                ChangeDataArmor();
            }
        }
        else
        {
            if (dataArmor.light)
            {
                mainUI.dataPlayer.armorClass = dataArmor.setAC + dexMo;
                mainUI.dataPlayer.listArmor.Add(mainUI.dataPlayer.armorUse);
                ChangeDataArmor();
            }
            else if (dataArmor.heavy && mainUI.dataPlayer.str >= dataArmor.condition)
            {
                mainUI.dataPlayer.armorClass = dataArmor.setAC;
                mainUI.dataPlayer.listArmor.Add(mainUI.dataPlayer.armorUse);
                ChangeDataArmor();
            }
            else if (dataArmor.heavy && mainUI.dataPlayer.str < dataArmor.condition)
            {
                mainUI.warnText.text = "Not enough STR.";
            }//can not use
            else if (!dataArmor.heavy && !dataArmor.light)
            {
                if (dexMo >= 2)
                {
                    mainUI.dataPlayer.armorClass = dataArmor.setAC + 2;
                }
                else
                {
                    mainUI.dataPlayer.armorClass = dataArmor.setAC + dexMo;
                }
                mainUI.dataPlayer.listArmor.Add(mainUI.dataPlayer.armorUse);
                ChangeDataArmor();
            }
        }//update AC
    }
    private void ChangeDataArmor()
    {
        mainUI.dataPlayer.armorUse = dataArmor;
        mainUI.dataPlayer.UpdateImageArmor();
        mainUI.OpenBagUI();
        mainUI.dataPlayer.listArmor.Remove(dataArmor);
    }
    private void Start()
    {
        nameItemText.text = dataArmor.nameArmor;
        detailText.text = " - ";
        if (dataArmor.light)
        {
            addOnText.text = "AC " + dataArmor.setAC.ToString() + "\n + Dex MO";
        }
        else if (dataArmor.heavy)
        {
            addOnText.text = "AC " + dataArmor.setAC.ToString();
            detailText.text = "need STR " + dataArmor.condition;
        }
        else
        {
            addOnText.text = "AC " + dataArmor.setAC.ToString() + "\n + Dex MO (max +2)";
        }
    }
}
