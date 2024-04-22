using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPlayerScript : MonoBehaviour
{
    [Header("Status")]
    public int hitPoint;
    private int hpLvOne;
    [HideInInspector] public int hitPointMax;
    [HideInInspector] public int takeDamage;
    [HideInInspector] public int healHitPoint;
    public int armorClass;
    public int str;//0
    public int dex;//1
    public int con;//2
    private int oldLevelPlayer;
    private int oldDexMoPLayer;
    private int oldConMoPLayer;
    public int xp;
    public int addXp;
    public int level;
    public int bonus;
    [HideInInspector] public int pointLevel;

    [Header("Link Obj")]
    public ShowPlayerScript player;
    [SerializeField] private GameObject UpStatusButtomObj;
    private NewDiceRollScript diceRoll;

    private void LevelUp()
    {
        if (addXp != 0)
        {
            xp += addXp;
            addXp = 0;
            int oldLevel = level;
            switch (xp)
            {
                case < 300:
                    level = 1;
                    break;
                case < 900:
                    level = 2;
                    break;
                case < 2700:
                    level = 3;
                    break;
                case < 6500:
                    level = 4;
                    break;
                case < 14000:
                    level = 5;
                    bonus += 1;
                    break;
                case < 23000:
                    level = 6;
                    break;
                case < 34000:
                    level = 7;
                    break;
                case < 48000:
                    level = 8;
                    break;
                case < 64000:
                    level = 9;
                    bonus += 1;
                    break;
                case < 85000:
                    level = 10;
                    break;
                case < 100000:
                    level = 11;
                    break;
                case < 120000:
                    level = 12;
                    break;
                case < 140000:
                    level = 13;
                    bonus += 1;
                    break;
                case < 165000:
                    level = 14;
                    break;
                case < 195000:
                    level = 15;
                    break;
                case < 225000:
                    level = 16;
                    break;
                case < 265000:
                    level = 17;
                    bonus += 1;
                    break;
                case < 305000:
                    level = 18;
                    break;
                case < 355000:
                    level = 19;
                    break;
                case > 355000:
                    level = 20;
                    break;
                default:
                    Debug.LogError("Level Error");
                    break;
            }
            if (oldLevel != level)
            {
                for (int i = 0; i < level - oldLevel; i++)
                {
                    pointLevel += 2;
                }
            }
        }
    }
    public void UpStatusButtom(int i)
    {
        switch (i)
        {
            case 0:
                str += 1;
                break;
            case 1:
                dex += 1;
                break;
            case 2:
                con += 1;
                break;
            default:
                break;
        }
        pointLevel -= 1;
        player.UpdateStatus();
    }
    private void UpStatusButtom()
    {
        if (pointLevel != 0)
        {
            UpStatusButtomObj.gameObject.SetActive(true);
        }
        else
        {
            UpStatusButtomObj.gameObject.SetActive(false);
        }
    }
    private void UpLevelHp()
    {
        int conMo = (con - 10) / 2;
        int damageHave = hitPointMax - hitPoint;
        if (oldLevelPlayer != level)
        {
            hitPointMax = hpLvOne;
            for (int i = 0; i < level - oldLevelPlayer; i++)
            {
                hitPointMax += 4;
            }
            hitPointMax += conMo;
            hitPoint = hitPointMax - damageHave;
            oldLevelPlayer = level;
        }
        if (oldConMoPLayer != conMo)
        {
            hitPointMax += conMo - oldConMoPLayer;
            hitPoint += conMo - oldConMoPLayer;
            oldConMoPLayer = conMo;
        }
        player.UpdateTextHp();
    }
    private void UpdateAC()
    {
        int dexMo = (dex - 10) / 2;
        if (dexMo != oldDexMoPLayer)
        {
            armorClass = 10 + dexMo;
            player.UpdateACText();
        }
    }
    public void PlayAnimation(int  i)
    {
        player.animaMon.SetInteger("step", i);
        StartCoroutine(DeleyAnimationTime());
    }
    IEnumerator DeleyAnimationTime()
    {
        float time = diceRoll.timeClose;
        yield return new WaitForSeconds(time);
        player.animaMon.SetInteger("step", 0);
    }
    private void Awake()
    {
        player.dataPlayer = this;
        diceRoll = GetComponent<NewDiceRollScript>();
    }
    private void Start()
    {
        oldLevelPlayer = level;
        oldDexMoPLayer = (dex - 10) / 2;
        oldConMoPLayer = (con - 10) / 2;

        hitPoint += oldConMoPLayer;
        hpLvOne = hitPoint;
        hitPointMax = hitPoint;

        armorClass += oldDexMoPLayer;

        bonus = 2;
        pointLevel = 0;

        UpStatusButtomObj.gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        LevelUp();
        UpStatusButtom();
        UpLevelHp();
        UpdateAC();
        if (healHitPoint != 0)
        {
            hitPoint += healHitPoint;
            healHitPoint = 0;
            if (hitPoint >= hitPointMax)
            {
                hitPoint = hitPointMax;
            }
            player.UpdateTextHp();
        }
        if (takeDamage != 0)
        {
            PlayAnimation(5);
            hitPoint -= takeDamage;
            takeDamage = 0;
            player.UpdateTextHp();
        }
    }
}
