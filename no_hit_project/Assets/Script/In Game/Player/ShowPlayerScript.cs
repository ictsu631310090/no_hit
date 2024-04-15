using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowPlayerScript : MonoBehaviour
{
    [Header("Status")]
    public int hitPoint;
    private int hpLvOne;
    [HideInInspector] public int hitPointMax;
    [HideInInspector] public int healHitPoint;
    [HideInInspector] public int takeDamage;
    public int armorClass;
    private int oldLevelPlayer;
    private int oldDexMoPLayer;
    private int oldConMoPLayer;

    [Header("link Obj")]
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI acText;
    [SerializeField] private Image hpbar;
    [SerializeField] private Animator animaMon;
    private UpLevelPlayerScript levelPlayer;

    public void UpdateTextHp()
    {
        hpText.text = hitPoint.ToString() + " / " + hitPointMax.ToString();
        float hp = hitPoint;
        float hpMax = hitPointMax;
        hpbar.fillAmount = hp / hpMax;
    }
    public void UpLevelHp()
    {
        int conMo = (levelPlayer.con - 10) / 2;
        int damageHave = hitPointMax - hitPoint;
        if (oldLevelPlayer != levelPlayer.level)
        {
            hitPointMax = hpLvOne;
            for (int i = 0; i < levelPlayer.level - oldLevelPlayer; i++)
            {
                hitPointMax += 4;
            }
            hitPointMax += conMo;
            hitPoint = hitPointMax - damageHave;
            oldLevelPlayer = levelPlayer.level;
        }
        if (oldConMoPLayer != conMo)
        {
            hitPointMax += conMo - oldConMoPLayer;
            hitPoint += conMo - oldConMoPLayer;
            oldConMoPLayer = conMo;
        }
        UpdateTextHp();
    }
    public void UpdateAC()
    {
        int dexMo = (levelPlayer.dex - 10) / 2;
        if (dexMo != oldDexMoPLayer)
        {
            armorClass = 10 + dexMo;
            acText.text = armorClass.ToString();
        }
    }
    private void Awake()
    {
        levelPlayer = GetComponent<UpLevelPlayerScript>();
    }
    private void Start()
    {
        oldLevelPlayer = levelPlayer.level;
        oldDexMoPLayer = (levelPlayer.dex - 10) / 2;
        oldConMoPLayer = (levelPlayer.con - 10) / 2;

        healHitPoint = 0;
        hitPoint += oldConMoPLayer;
        hpLvOne = hitPoint;
        hitPointMax = hitPoint;

        armorClass = 10 + oldDexMoPLayer;
        acText.text = armorClass.ToString();

        UpdateTextHp();
    }
    private void Update()
    {
        if (takeDamage != 0)
        {
            hitPoint -= takeDamage;
            takeDamage = 0;
            UpdateTextHp();
        }
        if (healHitPoint != 0)
        {
            hitPoint += healHitPoint;
            healHitPoint = 0;
            if (hitPoint > hitPointMax)
            {
                hitPoint = hitPointMax;
            }
            UpdateTextHp();
        }
        UpLevelHp();
        UpdateAC();
    }
}
