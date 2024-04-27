using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatScript : MonoBehaviour
{
    private int atkSTR;//modifier
    private int atkDEX;//modifier
    [SerializeField] private UIScript uiScript;
    [HideInInspector] public NewDiceRollScript diceRoll;
    [HideInInspector] public DataPlayerScript dataPlayer;
    private SetMonsterScript setMon;
    public List<MonsterScript> monsters;
    [HideInInspector] public bool monAttack;
    [HideInInspector] public int targetMons;
    public GameObject lightTarget;

    public Button rightAttack;
    public Button leftAttack;
    public Button bothAttack;
    [SerializeField] private Button endTurn;
    public bool finess;
    public int addDice;

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
            diceRoll.RollToHit(atkBonus + dataPlayer.bonus, addDice, 0);//0 = player, 1 = enemy
            StartCoroutine(Damage(dataPlayer.diceDamage, atkBonus, addDice));
        }
        else
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
            Debug.Log("No Monster");
        }
    }
    private void MovePlayerObj()
    {
        if (movePlayer)
        {
            Vector3 positionMon = monsters[targetMons].gameObject.transform.position;
            positionMon.x += distanceMon;
            dataPlayer.showPlayer.animaPlayer.gameObject.transform.position = Vector3.MoveTowards(dataPlayer.showPlayer.animaPlayer.gameObject.transform.position, positionMon, Time.deltaTime * speedMove);
        }
        else
        {
            Vector3 oldPosition = dataPlayer.showPlayer.gameObject.transform.position;
            dataPlayer.showPlayer.animaPlayer.gameObject.transform.position = Vector3.MoveTowards(dataPlayer.showPlayer.animaPlayer.gameObject.transform.position, oldPosition, Time.deltaTime * speedMove);
        }
    }
    IEnumerator Damage(int dice, int bonus, int addDiceInMethod)
    {
        float timeUse = (3 * diceRoll.timeClose);
        yield return new WaitForSeconds(timeUse / 2);
        movePlayer = false;
        yield return new WaitForSeconds(timeUse / 3);
        if (addDiceInMethod != 0)
        {
            yield return new WaitForSeconds(timeUse / 3);
        }
        Debug.Log("result : " + diceRoll.allResult);
        if (diceRoll.allResult >= monsters[targetMons].armorClass)
        {
            //yield return new WaitForSeconds(diceRoll.timeClose * 0.1f);
            diceRoll.RollDamage(dice, bonus, addDiceInMethod, 0);
            yield return new WaitForSeconds(timeUse);
            if (addDiceInMethod != 0)
            {
                yield return new WaitForSeconds(timeUse / 3);
            }
            monsters[targetMons].takeDamage = diceRoll.allResult;
        }
        else
        {
            monsters[targetMons].showMissImage = true;
            diceRoll.willAttack = false;
        }
        addDice = 0;
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
    } // player attack
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
        endTurn.interactable = false;
        if (!diceRoll.willAttack && monsters.Count > 0)
        {
            StopAllCoroutines();
            StartCoroutine(DelayMonsterAttack());
        }
        else if (!diceRoll.willAttack && monsters.Count == 0)
        {
            Debug.Log("Next");
            uiScript.nextScene = true;
        }
    }
    IEnumerator DelayMonsterAttack()
    {
        float timeUse = (3 * diceRoll.timeClose);
        yield return new WaitForSeconds(timeUse * 0.1f);

        for (int i = 0; i < monsters.Count; i++)
        {
            if (monsters[i] != null)
            {
                monsters[i].MonsterAttack();
                yield return new WaitForSeconds(timeUse);
                if (monsters[i].canAttack)//to hit
                {
                    yield return new WaitForSeconds(timeUse / 2);
                    monsters[i].canAttack = false;
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
        StopAllCoroutines();
    }
    private void Awake()
    {
        diceRoll = GetComponent<NewDiceRollScript>();
        setMon = GetComponent<SetMonsterScript>();
        dataPlayer = GetComponent<DataPlayerScript>();
    }
    private void Start()
    {
        addDice = 0;
        atkSTR = (dataPlayer.str - 10) / 2;
        atkDEX = (dataPlayer.dex - 10) / 2;
        atkBonus = atkSTR;
        monAttack = false;
        movePlayer = false;
        UpdateATKBonus();
        ReadyToCombat();
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
