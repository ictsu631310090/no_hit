using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatScript : MonoBehaviour
{
    private int atkSTR;//modifier
    private int atkDEX;//modifier
    [HideInInspector] public GameObject player;
    [HideInInspector] public UpLevelPlayerScript levelPlayer;
    [HideInInspector] public DiceRollScript diceRoll;
    public List<MonsterScript> monsters;

    [SerializeField] private Button rightAttack;
    [SerializeField] private Button leftAttack;
    [SerializeField] private Button bothAttack;
    [SerializeField] private Button endTurn;
    public bool finess;
    public void AttackButtom(int i)// 0-2
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
        if (!diceRoll.willAttack)
        {
            if (!finess)
            {
                diceRoll.RollDice(20, atkSTR + levelPlayer.bonus);
                StartCoroutine(Damage(4, atkSTR));
            }
            else
            {
                diceRoll.RollDice(20, atkDEX + levelPlayer.bonus);
                StartCoroutine(Damage(4, atkDEX));
            }
        }
    }
    public void EndTurnButtom()
    {
        if (!diceRoll.willAttack)
        {
            endTurn.interactable = false;
            monsters[0].MonsterAttack();//Enemy Attack
            rightAttack.interactable = true;
            leftAttack.interactable = true;
            bothAttack.interactable = true;
            endTurn.interactable = true;
            diceRoll.willAttack = true;
        }
    }
    IEnumerator Damage(int dice, int bonus)
    {
        yield return new WaitForSeconds(diceRoll.timeClose + (0.7f * diceRoll.timeClose) + (diceRoll.timeClose * 0.5f));
        Debug.Log("result : " + diceRoll.result);
        if (diceRoll.result >= monsters[0].armorClass)
        {
            yield return new WaitForSeconds(diceRoll.timeClose * 0.1f);
            diceRoll.RollDice(dice, bonus);
            yield return new WaitForSeconds(diceRoll.timeClose + (0.7f * diceRoll.timeClose) + (diceRoll.timeClose * 0.5f));
            monsters[0].takeDamage = diceRoll.result;
        }
    }
    private void Start()
    {
        levelPlayer = player.GetComponent<UpLevelPlayerScript>();
        diceRoll = GetComponent<DiceRollScript>();
        atkSTR = (levelPlayer.str - 10) / 2;
        atkDEX = (levelPlayer.dex - 10) / 2;
    }
    private void Update()
    {
        
    }
}
