using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    [HideInInspector] public bool canAttack;
    public void MonsterAttack()
    {
        combat.diceRoll.RollDice(20, toHitPlus , false , 1);
        StartCoroutine(Damage(damage.y, damage.z));
    }
    IEnumerator Damage(int dice, int bonus)
    {
        yield return new WaitForSeconds((2 * combat.diceRoll.timeClose) + (0.7f * combat.diceRoll.timeClose) + (combat.diceRoll.timeClose * 0.5f));
        if (combat.diceRoll.result >= combat.dataPlayer.armorClass)
        {
            canAttack = true;
            yield return new WaitForSeconds(combat.diceRoll.timeClose * 0.1f);
            combat.diceRoll.RollDice(dice, bonus , false , 1);//not critical;
            yield return new WaitForSeconds(combat.diceRoll.timeClose + (0.7f * combat.diceRoll.timeClose) + (combat.diceRoll.timeClose * 0.5f));
            combat.dataPlayer.takeDamage = combat.diceRoll.result;
        }
        canAttack = false;
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
            combat.dataPlayer.addXp = xpDrop;
            combat.diceRoll.RollDice(4, 0, false , 2); // heal
            combat.monsters[id] = null;
            combat.CheckMonsterDie(id);
            StopAllCoroutines();
            StartCoroutine(HealHPPlayer());
        }
    }
    IEnumerator HealHPPlayer()
    {
        yield return new WaitForSeconds((2 * combat.diceRoll.timeClose) + (0.7f * combat.diceRoll.timeClose) + (combat.diceRoll.timeClose * 0.5f));
        combat.dataPlayer.healHitPoint = combat.diceRoll.result; // heal after kill
        Debug.Log("Heal : " + combat.diceRoll.result);
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
        canAttack = false;
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
