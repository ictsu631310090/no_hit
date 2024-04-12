using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowPlayerScript : MonoBehaviour
{
    [Header("Just Look")]
    public int hitPoint;
    private int hitPointMax;
    public int armorClass;

    [Header("link Obj")]
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI acText;
    [SerializeField] private Image hpbar;
    [SerializeField] private Animator animaMon;

    public void UpdateHp()
    {
        hpText.text = hitPoint.ToString() + " / " + hitPointMax.ToString();
        hpbar.fillAmount = hitPoint / hitPointMax;
    }
    private void Start()
    {
        hitPointMax = hitPoint;
        acText.text = armorClass.ToString();
        UpdateHp();
    }
}
