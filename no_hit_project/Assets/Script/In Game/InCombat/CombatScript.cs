using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatScript : MonoBehaviour
{
    private int atkSTR;//modifier
    private int atkDEX;//modifier
    [HideInInspector] public DiceRollScript diceRoll;
    [HideInInspector] public DataPlayerScript dataPlayer;
    private SetMonsterScript setMon;
    public List<MonsterScript> monsters;
    [HideInInspector] public bool monAttack;
    [HideInInspector] public int targetMons;
    public GameObject lightTarget;

    [SerializeField] private Button rightAttack;
    [SerializeField] private Button leftAttack;
    [SerializeField] private Button bothAttack;
    [SerializeField] private Button endTurn;
    public bool finess;
    private bool critical;
    private int atkBonus;
    private bool movePlayer;
    [SerializeField] private float distanceMon;
    [SerializeField] private float speedMove;
    public void AttackButtom(int i)// 0-2 //player
    {
        if (!diceRoll.willAttack && monsters.Count > 0)
        {
            switch (i)
            {
                case 0:
                    rightAttack.interactable = false;
                    break;
                case 1:
                    leftAttack.interactable = false;
                    break;
                case 2:
                    bothAttack.interactable = false;
                    break;
                default:
                    Debug.LogError("Enter the attack button number.");
                    break;
            }//ปิดปุ่ม
            movePlayer = true;
            diceRoll.RollDice(20, atkBonus + dataPlayer.bonus, false, 0);
            StartCoroutine(Damage(4, atkBonus));
        }
        else
        {
            Debug.Log("No Monster");
        }
    }
    private void MovePlayerObj()
    {
        if (movePlayer)
        {
            Vector3 positionMon = monsters[targetMons].gameObject.transform.position;
            positionMon.x += distanceMon;
            dataPlayer.player.animaMon.gameObject.transform.position = Vector3.MoveTowards(dataPlayer.player.animaMon.gameObject.transform.position, positionMon, Time.deltaTime * speedMove);
        }
        else
        {
            Vector3 oldPosition = dataPlayer.player.gameObject.transform.position;
            dataPlayer.player.animaMon.gameObject.transform.position = Vector3.MoveTowards(dataPlayer.player.animaMon.gameObject.transform.position, oldPosition, Time.deltaTime * speedMove);
        }
    }
    IEnumerator Damage(int dice, int bonus)
    {
        float timeUse = (2 * diceRoll.timeClose) + (0.7f * diceRoll.timeClose) + (diceRoll.timeClose * 0.5f);
        yield return new WaitForSeconds(timeUse / 4);
        movePlayer = false;
        yield return new WaitForSeconds(timeUse * 3 / 4);
        Debug.Log("result : " + diceRoll.result);
        if (diceRoll.result - (atkBonus + dataPlayer.bonus) == 20)
        {
            critical = true;
            Debug.Log("Critical!!");
        }
        if (diceRoll.result >= monsters[targetMons].armorClass)
        {
            yield return new WaitForSeconds(diceRoll.timeClose * 0.1f);
            //diceRoll.RollDice(dice, bonus, true);//critical 100%
            diceRoll.RollDice(dice, bonus, critical , 0);
            Debug.Log("Damage : " + diceRoll.result + " bonus :" + bonus);
            critical = false;
            yield return new WaitForSeconds(timeUse);
            monsters[targetMons].takeDamage = diceRoll.result;
        }
    }
    public void CheckMonsterDie(int idMonDie)
    {
        int numMon = 0;
        for (int i = 0; i < monsters.Count; i++)
        {
            if (monsters[i] != null)
            {
                targetMons = monsters[i].id;
                lightTarget.transform.parent = monsters[i].transform.GetChild(1);
                lightTarget.transform.position = monsters[i].transform.GetChild(1).position;
            }
            else
            {
                numMon++;
            }
        }
        if (numMon == monsters.Count)//die all
        {
            monsters.Clear();
            lightTarget.SetActive(false);
        }
        else if (numMon == 2)//have 1 mon
        {
            foreach (var item in setMon.buttomTarget)
            {
                item.SetActive(false);
            }
        }
        setMon.buttomTarget[idMonDie].SetActive(false);
        Debug.Log("targetMons : " + targetMons);
    } // plater attack
    private void UpdateATKBonus()
    {
        int newStrMo = (dataPlayer.str - 10) / 2;
        int newDexMo = (dataPlayer.dex - 10) / 2;
        if (newStrMo != atkSTR || newDexMo != atkDEX)
        {
            atkSTR = newStrMo;
            atkDEX = newDexMo;
            if (finess)
            {
                if (atkSTR < atkDEX)
                {
                    atkBonus = atkDEX;
                }
                else
                {
                    atkBonus = atkSTR;
                }
            }
            else
            {
                atkBonus = atkSTR;
            }
        }
        int numText = atkBonus + dataPlayer.bonus;
        rightAttack.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "right hand \n d20 + " + numText;
        leftAttack.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "left hand \n d20 + " + numText;
        bothAttack.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "both hand \n d20 + " + numText;
    }  //finess str or dex
    public void TargetMonster(int i)
    {
        if (i != targetMons)
        {
            targetMons = i;
            if (monsters.Count != 1)
            {
                lightTarget.transform.parent = monsters[targetMons].transform.GetChild(1);
                lightTarget.transform.position = monsters[targetMons].transform.GetChild(1).position;
            }
        }
    }//buttom
    public void EndTurnButtom()
    {
        if (!diceRoll.willAttack && monsters.Count > 0)
        {
            StopAllCoroutines();
            StartCoroutine(DelayMonsterAttack(diceRoll.timeClose));
        }
        else if (!diceRoll.willAttack && monsters.Count == 0)
        {
            Debug.Log("Next");
        }
    }
    IEnumerator DelayMonsterAttack(float time)
    {
        endTurn.interactable = false;
        float timeUse = (2 * time) + (0.7f * time) + (time * 0.5f);
        yield return new WaitForSeconds(time * 0.2f);

        for (int i = 0; i < monsters.Count; i++)
        {
            if (monsters[i] != null)
            {
                monsters[i].MonsterAttack();
                yield return new WaitForSeconds(timeUse);
                if (monsters[i].canAttack)//to hit
                {
                    yield return new WaitForSeconds(timeUse);
                }
            }
        }
        monAttack = true;
    }
    private void ReadyToCombat()
    {
        rightAttack.interactable = true;
        leftAttack.interactable = true;
        bothAttack.interactable = true;
        diceRoll.willAttack = false;
        endTurn.interactable = true;
        monAttack = false;
    }
    private void Awake()
    {
        diceRoll = GetComponent<DiceRollScript>();
        setMon = GetComponent<SetMonsterScript>();
        dataPlayer = GetComponent<DataPlayerScript>();
    }
    private void Start()
    {
        atkSTR = (dataPlayer.str - 10) / 2;
        atkDEX = (dataPlayer.dex - 10) / 2;
        atkBonus = atkSTR;
        critical = false;
        monAttack = false;
        movePlayer = false;
        UpdateATKBonus();
    }
    private void Update()
    {
        if (monsters.Count > 0)
        {
            if (monAttack)//มอนตีเสร็จยัง
            {
                ReadyToCombat();
            }
        }
        UpdateATKBonus();
        MovePlayerObj();
    }
}
