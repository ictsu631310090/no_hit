using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MonsterScript : MonoBehaviour
{
    [Header("Read Only")]
    public int id;
    private int hitPointMax;
    public List<int> diceDrop;
    [HideInInspector] public string monName;
    [HideInInspector] public int hitPoint;
    [HideInInspector] public int armorClass;
    [HideInInspector] public int moneyDrop;
    [HideInInspector] public int xpDrop;
    [HideInInspector] public int toHitPlus;
    [HideInInspector] public Vector3Int damage;

    [Header("link Obj")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI acText;
    [SerializeField] private Image hpbar;
    private float scaleBar;
    [HideInInspector] public GameObject model;
    [HideInInspector] public Animator animaMon;
    [HideInInspector] public int takeDamage;
    [HideInInspector] public CombatScript combat;
    public void MonsterAttack()
    {
        combat.diceRoll.RollDice(20, toHitPlus , false);
        StartCoroutine(Damage(damage.y, damage.z));
    }
    IEnumerator Damage(int dice, int bonus)
    {
        yield return new WaitForSeconds((2 * combat.diceRoll.timeClose) + (0.7f * combat.diceRoll.timeClose) + (combat.diceRoll.timeClose * 0.5f));
        if (combat.diceRoll.result >= combat.player.GetComponent<ShowPlayerScript>().armorClass)
        {
            yield return new WaitForSeconds(combat.diceRoll.timeClose * 0.1f);
            combat.diceRoll.RollDice(dice, bonus , false);//not critical;
            yield return new WaitForSeconds(combat.diceRoll.timeClose + (0.7f * combat.diceRoll.timeClose) + (combat.diceRoll.timeClose * 0.5f));
            combat.player.GetComponent<ShowPlayerScript>().takeDamage = combat.diceRoll.result;
        }
    }
    public void UpdateHp()
    {
        hitPoint -= takeDamage;
        takeDamage = 0;
        hpText.text = hitPoint.ToString() + " / " + hitPointMax.ToString();
        float hitF = hitPoint;
        float hitFM = hitPointMax;
        scaleBar = hitF / hitFM;
        hpbar.fillAmount = scaleBar;
        CheckDie();
    }
    public void CheckDie()
    {
        if (hitPoint <= 0)
        {
            UIScript.addMoney = moneyDrop;
            combat.player.GetComponent<UpLevelPlayerScript>().addXp = xpDrop;
            combat.diceRoll.RollDice(4, 0, false);
            combat.CheckMonsterDie(id);
            StartCoroutine(HealHPPlayer());
        }
    }
    IEnumerator HealHPPlayer()
    {
        yield return new WaitForSeconds((2 * combat.diceRoll.timeClose) + (0.7f * combat.diceRoll.timeClose) + (combat.diceRoll.timeClose * 0.5f));
        combat.player.GetComponent<ShowPlayerScript>().healHitPoint = combat.diceRoll.result; // heal after kill
        Debug.Log("Heal : " + combat.diceRoll.result);
        combat.monsters[combat.findNum(id)] = null;
        //combat.monsters.Remove(this.GetComponent<MonsterScript>());
        if (combat.monsters.Count > 0)
        {
            combat.lightTarget.SetActive(true);
        }
        else
        {
            combat.lightTarget.SetActive(false);
        }
        Destroy(this.gameObject);

    }
    private void Start()
    {
        takeDamage = 0;
        hitPointMax = hitPoint;
        nameText.text = monName;
        acText.text = armorClass.ToString();
        UpdateHp();
    }
    private void Update()
    {
        if (takeDamage != 0)
        {
            UpdateHp();
        }
    }
}
