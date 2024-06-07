using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataPlayerScript : MonoBehaviour
{
    [Header("Status")]
    [HideInInspector] public int hitPoint;
    [HideInInspector] public int hitPointMax;
    [HideInInspector] public int takeDamage;
    [HideInInspector] public int healHitPoint;
    [HideInInspector] public int haveDamage;
    private int hitPointLevelOne;
    public int armorClass;
    public int str;//0
    public int dex;//1
    public int con;//2
    private int oldLevelPlayer;
    private int oldDexMoPlayer;
    private int oldConMoPlayer;
    [HideInInspector] public bool weaponTwoHand;

    [Header ("Level")]
    public int xp;
    public int addXp;//just test
    [HideInInspector] public int level;
    [HideInInspector] public int pointLevel;
    public int diceDamage;//wapon
    public int bonus;

    [Header("Bag")]
    public List<CreateArmorScript> listArmor;
    public List<CreateShieldScript> listShield;
    public List<CreateWeaponScript> listWeapon;
    public CreateWeaponScript[] rlHandWeapon = { null, null };//null,have
    public CreateShieldScript[] rlHandShield = { null, null };//null,have
    public CreateArmorScript armorUse;

    [Header("Bag Dice")]
    public int[] diceHave = { 0, 0, 0, 0, 0 };//4, 6, 8, 10, 12

    [Header("Other Data")]
    public int room;

    [Header("Link Obj")]
    public ShowPlayerScript showPlayer;
    [SerializeField] private GameObject upStatusButtomObj;
    [SerializeField] private NewDiceRollScript diceRoll;
    private CombatScript combat;
    //Level
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
        if (upStatusButtomObj != null)
        {
            if (pointLevel != 0)
            {
                upStatusButtomObj.gameObject.SetActive(true);
            }
            else
            {
                upStatusButtomObj.gameObject.SetActive(false);
            }
        }
    }
    //data
    private void UpLevelHp()
    {
        if (oldLevelPlayer != level)
        {
            haveDamage = hitPointMax - hitPoint;
            hitPointMax = hitPointLevelOne + (5 * (level - 1)) + (oldConMoPlayer * level);
            hitPoint = hitPointMax - haveDamage;
            haveDamage = 0;
            oldLevelPlayer = level;
        }

        if (showPlayer != null)
        {
            showPlayer.UpdateTextHp();
        }
    }
    private void ACUpdateFormArmor()
    {
        int newDexMoPlayer = (dex - 10) / 2;
        if (oldDexMoPlayer != newDexMoPlayer)
        {
            oldDexMoPlayer = newDexMoPlayer;
            armorClass = 10 + oldDexMoPlayer;
        }
        if (armorUse == null)
        {
            armorClass = 10 + oldDexMoPlayer;
        }
        else
        {
            if (armorUse.light)
            {
                armorClass = armorUse.setAC + oldDexMoPlayer;
            }
            else if (armorUse.heavy)
            {
                armorClass = armorUse.setAC;
            }
            else if (!armorUse.heavy && !armorUse.light)
            {
                if (oldDexMoPlayer >= 2)
                {
                    armorClass = armorUse.setAC + 2;
                }
                else
                {
                    armorClass = armorUse.setAC + oldDexMoPlayer;
                }
            }
        }
        for (int i = 0; i < rlHandShield.Length; i++)
        {
            if (rlHandShield[i] != null)
            {
                armorClass += 2;
            }
        }
        UpdateImageArmor();
    }
    //show
    public void PlayAnimation(int  i)
    {
        if (takeDamage != 0)
        {
            showPlayer.animaPlayer.SetInteger("step", i);
            StartCoroutine(DeleyAnimationTime());
        }
        else
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
        if (armorUse != null && showPlayer != null)
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
        //showPlayer.modelPlayer[12].color = new Color(1, 1, 1, 0);
        //showPlayer.modelPlayer[13].color = new Color(0.5f, 0.5f, 0.5f, 0);
        if (showPlayer != null)
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
                else
                {
                    if (combat.buttonAttack.Length > 0)
                    {
                        combat.buttonAttack[0].gameObject.SetActive(true);
                        combat.buttonAttack[1].gameObject.SetActive(true);
                        combat.buttonAttack[2].gameObject.SetActive(false);
                    }
                    showPlayer.animaPlayer.SetInteger("step", 0);

                }
                showPlayer.modelPlayer[right + 12].sprite = rlHandWeapon[right].image;
                if (right == 0)
                {
                    showPlayer.modelPlayer[14].color = new Color(1, 1, 1, 0);
                    showPlayer.modelPlayer[12].sortingOrder = 2;
                    showPlayer.modelPlayer[right + 12].color = new Color(1, 1, 1, 1);
                }
                else if (right == 1)
                {
                    showPlayer.modelPlayer[13].color = new Color(0.5f, 0.5f, 0.5f, 1);
                }
            }
            else//shield
            {
                if (right == 0)
                {
                    showPlayer.modelPlayer[12].sprite = rlHandShield[right].image;
                    showPlayer.modelPlayer[14].color = new Color(1, 1, 1, 1);
                }
                else//i == 1
                {
                    showPlayer.modelPlayer[13].sprite = rlHandShield[right].image;
                    showPlayer.modelPlayer[13].color = new Color(0.5f, 0.5f, 0.5f, 1);
                }
                showPlayer.UpdateACText();
            }
        }
    }
    private void Awake()
    {
        if (showPlayer != null)
        {
            showPlayer.dataPlayer = GetComponent<DataPlayerScript>();
        }
        combat = GetComponent<CombatScript>();
    }
    private void Start()
    {
        hitPointLevelOne = 10;
        oldLevelPlayer = level;
        oldDexMoPlayer = (dex - 10) / 2;
        oldConMoPlayer = (con - 10) / 2;
        if (level != 1)
        {
            hitPointMax = hitPointLevelOne + (5 * (level - 1)) + (oldConMoPlayer * level);
        }
        else
        {
            hitPointMax = hitPointLevelOne + oldConMoPlayer;
        }
        hitPoint = hitPointMax - haveDamage;
        haveDamage = 0;

        if (upStatusButtomObj != null)
        {
            upStatusButtomObj.gameObject.SetActive(false);
        }
        addXp = 1;
        if (showPlayer != null)
        {
            showPlayer.UpdateACText();
            if (rlHandWeapon[0] == null)
            {
                showPlayer.modelPlayer[12].color = new Color(1, 1, 1, 0);//wearpon Right
            }
            else if (rlHandWeapon[1] == null)
            {
                showPlayer.modelPlayer[13].color = new Color(0.5f, 0.5f, 0.5f, 0);//weapon Left
            }
        }
        ACUpdateFormArmor();
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
                haveDamage = 0;
            }
            showPlayer.UpdateTextHp();
        }//get heal
        if (takeDamage != 0)
        {
            if (takeDamage > 0)
            {
                PlayAnimation(5);
            }
            hitPoint -= takeDamage;
            takeDamage = 0;
            showPlayer.UpdateTextHp();
        }//take damage
    }
}
