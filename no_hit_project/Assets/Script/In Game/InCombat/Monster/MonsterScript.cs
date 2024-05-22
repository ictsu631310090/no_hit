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
    private Animator animaMon;
    [HideInInspector] public bool showMissImage;
    [SerializeField] private Animator showMissAnimator;
    [HideInInspector] public int takeDamage;
    [HideInInspector] public CombatScript combat;
    [HideInInspector] public bool canAttack;

    [Header("In put data")]
    [SerializeField] private float speedMove;
    [SerializeField] private float distancePlayer;
    private bool moveMons;
    public void MonsterAttack()
    {
        combat.diceRoll.RollToHit(toHitPlus, 0, 1);
        animaMon.SetInteger("step", 1);
        moveMons = true;
        StartCoroutine(Damage(damage.y, damage.z));
    }
    IEnumerator Damage(int dice, int bonus)
    {
        float time = 3 * combat.diceRoll.timeClose;
        yield return new WaitForSeconds((time /3));
        moveMons = false;
        animaMon.SetInteger("step", 0);
        yield return new WaitForSeconds((time * 1 / 3));
        if (bonus != 0)
        {
            yield return new WaitForSeconds(combat.diceRoll.timeClose);
        }
        if (combat.diceRoll.allResult >= combat.dataPlayer.armorClass)
        {
            canAttack = true;
            combat.diceRoll.RollDamage(dice, bonus, 0, 1);
            if (combat.diceRoll.critical)
            {
                yield return new WaitForSeconds(combat.diceRoll.timeClose);
            }
            if (bonus != 0)
            {
                yield return new WaitForSeconds(combat.diceRoll.timeClose);
            }
            yield return new WaitForSeconds((time * 2 / 3));
            combat.dataPlayer.takeDamage = combat.diceRoll.allResult;
            yield return new WaitForSeconds((time / 3));
        }
        else
        {
            combat.dataPlayer.showPlayer.showMiss = true;
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
            combat.dataPlayer.addXp = xpDrop;
            combat.diceRoll.RollDamage(4, 0, 0, 2);//heal
            foreach (var item in diceDrop)
            {
                switch (item)
                {
                    case 4:
                        combat.dataPlayer.diceHave[0] += 1;
                        break;
                    case 6:
                        combat.dataPlayer.diceHave[1] += 1;
                        break;
                    case 8:
                        combat.dataPlayer.diceHave[2] += 1;
                        break;
                    case 10:
                        combat.dataPlayer.diceHave[3] += 1;
                        break;
                    case 12:
                        combat.dataPlayer.diceHave[4] += 1;
                        break;
                    default:
                        break;
                }
            }//Add Dice
            combat.monsters[id] = null;
            combat.CheckMonsterDie(id);
            StopAllCoroutines();
            StartCoroutine(HealHPPlayer());
        }
        else
        {
            animaMon.SetInteger("step", 0);
        }
    }
    IEnumerator HealHPPlayer()
    {
        yield return new WaitForSeconds(combat.diceRoll.timeClose / 2);
        animaMon.SetInteger("step", 3);
        yield return new WaitForSeconds((combat.diceRoll.timeClose * 3/4) + (0.7f * combat.diceRoll.timeClose) + (combat.diceRoll.timeClose * 0.5f));
        combat.dataPlayer.healHitPoint = combat.diceRoll.allResult; // heal after kill
        Debug.Log("Heal : " + combat.diceRoll.allResult);
        if (combat.monsters.Count > 0)
        {
            combat.lightTarget.SetActive(true);
        }
        else
        {
            combat.lightTarget.SetActive(false);
        }
        yield return new WaitForSeconds(combat.diceRoll.timeClose/2);
        Destroy(this.gameObject);
    }
    private void MoveMonster()
    {
        if (moveMons)
        {
            Vector3 positionPlayer = combat.dataPlayer.showPlayer.gameObject.transform.position;
            Debug.Log(positionPlayer);
            positionPlayer.x -= distancePlayer;
            model.transform.position = Vector3.MoveTowards(model.transform.position, positionPlayer, Time.deltaTime * speedMove);
        }
        else
        {
            Vector3 oldPosition = this.gameObject.transform.position;
            model.transform.position = Vector3.MoveTowards(model.transform.position, oldPosition, Time.deltaTime * speedMove);
        }
    }
    private void ShowMissImage()
    {
        if (showMissImage)
        {
            showMissAnimator.SetBool("show", true);
            StartCoroutine(DelayCloseMiss());
        }
    }
    IEnumerator DelayCloseMiss()
    {
        showMissImage = false;
        yield return new WaitForSeconds(0.5f);
        showMissAnimator.SetBool("show", false);
    }
    private void Start()
    {
        animaMon = model.GetComponent<Animator>();
        canAttack = false;
        takeDamage = 0;
        hitPointMax = hitPoint;
        nameText.text = monName;
        acText.text = armorClass.ToString();
        moveMons = false;
        UpdateHp();
    }
    private void Update()
    {
        if (takeDamage != 0)
        {
            animaMon.SetInteger("step", 2);
            UpdateHp();
        }
        MoveMonster();
        ShowMissImage();
    }
}
