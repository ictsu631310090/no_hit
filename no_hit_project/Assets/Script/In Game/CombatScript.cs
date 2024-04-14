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
    public List<MonsterScript> monsters;
    [HideInInspector] public bool monAttack;

    [SerializeField] private Button rightAttack;
    [SerializeField] private Button leftAttack;
    [SerializeField] private Button bothAttack;
    [SerializeField] private Button endTurn;
    public bool finess;
    private bool critical;
    private int atkBonus;
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
            monsters[0].MonsterAttack();//Enemy Attack
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
        if (diceRoll.result >= monsters[0].armorClass)
        {
            yield return new WaitForSeconds(diceRoll.timeClose * 0.1f);
            //diceRoll.RollDice(dice, bonus, true);
            diceRoll.RollDice(dice, bonus, critical);
            Debug.Log("Damage : " + diceRoll.result + " bonus :" + bonus);
            critical = false;
            yield return new WaitForSeconds(timeUse);
            monsters[0].takeDamage = diceRoll.result;
        }
    }
    private void Start()
    {
        levelPlayer = player.GetComponent<UpLevelPlayerScript>();
        diceRoll = GetComponent<DiceRollScript>();
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
