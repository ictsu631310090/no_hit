using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonsterScript : MonoBehaviour
{
    [Header("Just Look")]
    public int id;
    [HideInInspector] public string monName;
    [HideInInspector] public int hitPoint;
    private int hitPointMax;
    [HideInInspector] public int armorClass;
    [HideInInspector] public int moneyDrop;
    [HideInInspector] public int xpDrop;
    public List<int> diceDrop;
    public int toHitPlus;
    public Vector3Int damage;

    [Header("link Obj")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI acText;
    [SerializeField] private Image hpbar;
    private float scaleBar;
    [SerializeField] private Animator animaMon;
    [HideInInspector] public int takeDamage;
    [HideInInspector] public CombatScript combat;
    public void MonsterAttack()
    {
        combat.diceRoll.RollDice(20, toHitPlus);
        StartCoroutine(Damage(damage.y, damage.z));
    }
    IEnumerator Damage(int dice, int bonus)
    {
        yield return new WaitForSeconds(combat.diceRoll.timeClose + (0.7f * combat.diceRoll.timeClose) + (combat.diceRoll.timeClose * 0.5f));
        if (combat.diceRoll.result >= combat.player.GetComponent<ShowPlayerScript>().armorClass)
        {
            yield return new WaitForSeconds(combat.diceRoll.timeClose * 0.1f);
            combat.diceRoll.RollDice(dice, bonus);
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
            Destroy(this.gameObject);
        }
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
