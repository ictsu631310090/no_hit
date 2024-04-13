using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowPlayerScript : MonoBehaviour
{
    //[Header("Status")]
    [HideInInspector] public int hitPoint;
    private int hitPointMax;
    [HideInInspector] public int takeDamage;
    [HideInInspector] public int armorClass;

    [Header("link Obj")]
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI acText;
    [SerializeField] private Image hpbar;
    [SerializeField] private Animator animaMon;
    private UpLevelPlayerScript levelPlayer;

    public void UpdateHp()
    {
        hpText.text = hitPoint.ToString() + " / " + hitPointMax.ToString();
        float hp = hitPoint;
        float hpMax = hitPointMax;
        hpbar.fillAmount = hp / hpMax;
    }
    private void Start()
    {
        levelPlayer = GetComponent<UpLevelPlayerScript>();
        for (int i = 0; i < levelPlayer.level; i++)
        {
            hitPoint += Random.Range(1, 9);
        }
        hitPoint += (levelPlayer.con - 10) / 2;
        hitPointMax = hitPoint;
        armorClass = 10 + (levelPlayer.dex - 10) / 2;
        acText.text = armorClass.ToString();
        UpdateHp();
    }
    private void Update()
    {
        if (takeDamage != 0)
        {
            hitPoint -= takeDamage;
            takeDamage = 0;
            UpdateHp();
        }
    }
}
