using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonsterScript : MonoBehaviour
{
    [Header("Just Look")]
    public int id;
    public string monName;
    public int hitPoint;
    private int hitPointMax;
    public int armorClass;
    public int moneyDrop;
    public int xpDrop;
    public List<int> diceDrop;

    [Header("link Obj")]
    [SerializeField] private TextMeshProUGUI nameText;
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
        nameText.text = monName;
        acText.text = armorClass.ToString();
        UpdateHp();
    }
}
