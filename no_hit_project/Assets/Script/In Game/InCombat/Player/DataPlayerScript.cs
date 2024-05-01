using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [HideInInspector] public bool weaponTwoHand;

    [Header ("Level")]
    public int xp;
    public int addXp;//just test
    [HideInInspector] public int level;
    public int diceDamage;//wapon
    public int bonus;

    [Header("Bag")]
    public List<CreateWeaponScript> listWeapon;
    public List<CreateShieldScript> listShield;
    public List<CreateArmorScript> listArmor;
    [HideInInspector] public CreateWeaponScript[] rlHandWeapon = { null, null };//null,have
    [HideInInspector] public CreateShieldScript[] rlHandShield = { null, null };//null,have
    [HideInInspector] public CreateArmorScript armorUse;

    [Header("Bag Dice")]
    public int[] diceHave = { 0, 0, 0, 0, 0 };//4, 6, 8, 10, 12
    [HideInInspector] public int pointLevel;

    [Header("Link Obj")]
    public ShowPlayerScript showPlayer;
    [SerializeField] private GameObject UpStatusButtomObj;
    private NewDiceRollScript diceRoll;
    private CombatScript combat;

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
        showPlayer.UpdateStatus();
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
        showPlayer.UpdateTextHp();
    }
    public void PlayAnimation(int  i)
    {
        if (!diceRoll.attacking)
        {
            showPlayer.animaPlayer.SetInteger("step", i);
            StartCoroutine(DeleyAnimationTime());
        }
        else if (takeDamage != 0)
        {
            showPlayer.animaPlayer.SetInteger("step", i);
            StartCoroutine(DeleyAnimationTime());
        }
    }
    IEnumerator DeleyAnimationTime()
    {
        float time = diceRoll.timeClose / 2;
        yield return new WaitForSeconds(time);
        if (!weaponTwoHand)
        {
            showPlayer.animaPlayer.SetInteger("step", 0);
        }
        else
        {
            showPlayer.animaPlayer.SetInteger("step", 3);
        }
    }
    public void UpdateImageArmor()
    {
        if (armorUse != null)
        {
            for (int i = 0; i < armorUse.image.Length; i++)
            {
                showPlayer.modelPlayer[i].sprite = armorUse.image[i];
            }
            showPlayer.UpdateACText();
        }
    }
    public void UpdateImageWeapon(int right, bool weapon)
    {
        if (weapon)
        {
            combat.finess = rlHandWeapon[right].finesse;
            if (weaponTwoHand)
            {
                if (combat.buttonAttack.Length > 0)
                {
                    combat.buttonAttack[0].gameObject.SetActive(false);
                    combat.buttonAttack[1].gameObject.SetActive(false);
                    combat.buttonAttack[2].gameObject.SetActive(true);
                }
                showPlayer.animaPlayer.SetInteger("step", 3);
            }
            showPlayer.modelPlayer[right + 12].sprite = rlHandWeapon[right].image;
            if (right == 0)
            {
                showPlayer.modelPlayer[12].sortingOrder = 2;
            }
            showPlayer.modelPlayer[right + 12].color = new Color(1, 1, 1, 1);
        }
        else//shield
        {
            if (right == 0)
            {
                showPlayer.modelPlayer[12].sprite = rlHandShield[right].image;
                showPlayer.modelPlayer[12].sortingOrder = 4;
                showPlayer.modelPlayer[12].color = new Color(1, 1, 1, 1);
            }
            else//i == 1
            {
                showPlayer.modelPlayer[13].sprite = rlHandShield[right].image;
                showPlayer.modelPlayer[13].color = new Color(0.5f, 0.5f, 0.5f, 1);
            }
            showPlayer.UpdateACText();
        }        
    }
    private void Awake()
    {
        showPlayer.dataPlayer = GetComponent<DataPlayerScript>();
        diceRoll = GetComponent<NewDiceRollScript>();
        combat = GetComponent<CombatScript>();
    }
    private void Start()
    {
        oldLevelPlayer = level;
        oldDexMoPLayer = (dex - 10) / 2;
        oldConMoPLayer = (con - 10) / 2;

        hitPoint += oldConMoPLayer;
        hpLvOne = hitPoint;
        hitPointMax = hitPoint;
        weaponTwoHand = false;

        armorClass += oldDexMoPLayer;
        diceDamage = 1;//just hand
        bonus = 2;
        pointLevel = 0;

        UpStatusButtomObj.gameObject.SetActive(false);

        showPlayer.UpdateACText();
        showPlayer.modelPlayer[12].color = new Color(1, 1, 1, 0);
        showPlayer.modelPlayer[13].color = new Color(0.5f, 0.5f, 0.5f, 0);

        UpdateImageArmor();
    }
    private void Update()
    {
        LevelUp();
        UpStatusButtom();
        UpLevelHp();
        if (healHitPoint != 0)
        {
            hitPoint += healHitPoint;
            healHitPoint = 0;
            if (hitPoint >= hitPointMax)
            {
                hitPoint = hitPointMax;
            }
            showPlayer.UpdateTextHp();
        }
        if (takeDamage != 0)
        {
            PlayAnimation(5);
            hitPoint -= takeDamage;
            takeDamage = 0;
            showPlayer.UpdateTextHp();
        }
    }
}
