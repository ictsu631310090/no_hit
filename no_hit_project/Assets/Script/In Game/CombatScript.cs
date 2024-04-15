using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatScript : MonoBehaviour
{
    private int atkSTR;//modifier
    private int atkDEX;//modifier
    public GameObject player;
    [HideInInspector] public UpLevelPlayerScript levelPlayer;
    [HideInInspector] public DiceRollScript diceRoll;
    private CreateMonsterScript createMonster;
    private SetMonsterScript setMon;
    public List<MonsterScript> monsters;
    [HideInInspector] public bool monAttack;
    [SerializeField] private int targetMons;
    public GameObject lightTarget;

    [SerializeField] private Button rightAttack;
    [SerializeField] private Button leftAttack;
    [SerializeField] private Button bothAttack;
    [SerializeField] private Button endTurn;
    public bool finess;
    private bool critical;
    private int atkBonus;
    public int findNum(int id)
    {
        int numMon = 0;
        for (int i = 0; i < monsters.Count; i++)
        {
            if (monsters[i] != null)
            {
                if (monsters[i].id == id)
                {
                    numMon = i;
                    break;
                }
            }
        }
        return numMon;
    }
    public void TargetMonster(int i)
    {
        if (i != targetMons)
        {
            targetMons = i;
            if (monsters.Count != 1)
            {
                lightTarget.transform.position = createMonster.spawnPointMon[i].transform.position;
            }
        }
    }
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
            diceRoll.RollDice(20, atkBonus + levelPlayer.bonus, false);
            StartCoroutine(Damage(4, atkBonus));
        }
    }
    IEnumerator Damage(int dice, int bonus)
    {
        float timeUse = (2 * diceRoll.timeClose) + (0.7f * diceRoll.timeClose) + (diceRoll.timeClose * 0.5f);
        yield return new WaitForSeconds(timeUse);
        Debug.Log("result : " + diceRoll.result);
        if (diceRoll.result - (atkBonus + levelPlayer.bonus) == 20)
        {
            critical = true;
        }
        if (diceRoll.result >= monsters[findNum(targetMons)].armorClass)
        {
            yield return new WaitForSeconds(diceRoll.timeClose * 0.1f);
            //diceRoll.RollDice(dice, bonus, true);
            diceRoll.RollDice(dice, bonus, critical);
            Debug.Log("Damage : " + diceRoll.result + " bonus :" + bonus);
            critical = false;
            yield return new WaitForSeconds(timeUse);
            monsters[findNum(targetMons)].takeDamage = diceRoll.result;
        }
    }
    public void CheckMonsterDie(int monDie)
    {
        setMon.buttomTarget[monDie].SetActive(false);
        if (monDie != 0)
        {
            TargetMonster(0);
        }
        else
        {
            TargetMonster(1);
        }
        Debug.Log("targetMons : " + targetMons);

    }
    private void UpdateATKBonus()
    {
        int newStrMo = (levelPlayer.str - 10) / 2;
        int newDexMo = (levelPlayer.dex - 10) / 2;
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
        int numText = atkBonus + levelPlayer.bonus;
        rightAttack.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "right hand \n d20 + " + numText;
        leftAttack.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "left hand \n d20 + " + numText;
        bothAttack.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "both hand \n d20 + " + numText;
    }
    public void EndTurnButtom()
    {
        if (!diceRoll.willAttack)
        {
            endTurn.interactable = false;
            monAttack = true;
            monsters[findNum(targetMons)].MonsterAttack();//Enemy Attack
        }
    }
    private void Awake()
    {
        levelPlayer = player.GetComponent<UpLevelPlayerScript>();
        diceRoll = GetComponent<DiceRollScript>();
        createMonster = GetComponent<CreateMonsterScript>();
        setMon = GetComponent<SetMonsterScript>();
    }
    private void Start()
    {
        targetMons = 0;

        atkSTR = (levelPlayer.str - 10) / 2;
        atkDEX = (levelPlayer.dex - 10) / 2;
        atkBonus = atkSTR;
        critical = false;
        monAttack = false;
        UpdateATKBonus();
    }
    private void Update()
    {
        if (monsters.Count > 0)
        {
            if (monAttack)//มอนตีเสร็จยัง
            {
                rightAttack.interactable = true;
                leftAttack.interactable = true;
                bothAttack.interactable = true;
                diceRoll.willAttack = false;
                endTurn.interactable = true;
                monAttack = false;
            }
        }
        UpdateATKBonus();
    }
}
