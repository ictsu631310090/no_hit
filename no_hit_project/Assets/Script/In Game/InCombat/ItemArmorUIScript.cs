using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemArmorUIScript : MonoBehaviour
{
    public CreateArmorScript dataArmor;
    [SerializeField] private TextMeshProUGUI nameItemText;
    [SerializeField] private TextMeshProUGUI addOnText;
    [SerializeField] private TextMeshProUGUI detailText;
    [HideInInspector] public UIScript mainUI;
    public void CilckLookImage()
    {
        mainUI.ImageItemShow(dataArmor.image[1]);
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
