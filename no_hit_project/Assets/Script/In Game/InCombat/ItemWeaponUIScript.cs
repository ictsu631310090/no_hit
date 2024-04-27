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
