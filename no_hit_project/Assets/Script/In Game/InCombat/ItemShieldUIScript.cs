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
    public void CilckLookImage()
    {
        mainUI.ImageItemShow(dataShield.image);
    }
    private void Start()
    {
        nameItemText.text = dataShield.nameWeapon;
        detailText.text = " - ";
        addOnText.text = "+" + dataShield.addAC.ToString() + "AC";
    }
}
