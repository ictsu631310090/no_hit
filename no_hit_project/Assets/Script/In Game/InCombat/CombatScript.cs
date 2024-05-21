using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatScript : MonoBehaviour
{
    [Header("Link Script")]
    [SerializeField] private UIScript uiScript;
    [SerializeField] private SaveManagerScript saveManager;
    [HideInInspector] public DataPlayerScript dataPlayer;
    [SerializeField] private CreateMonsterScript createMonster;
    public NewDiceRollScript diceRoll;

    private int atkSTR;//modifier
    private int atkDEX;//modifier
    public List<MonsterScript> monsters;
    [HideInInspector] public bool monAttack;
    [HideInInspector] public int targetMons;
    public GameObject lightTarget;

    public Button[] buttonAttack;//r = 0, l = 1, b = 2
    [SerializeField] private Button endTurn;
    public bool finess;
    public int addDice;

    private int atkBonus;
    private bool movePlayer;
    [SerializeField] private float distanceMon;
    [SerializeField] private float speedMove;
    public void AttackButtom(int i)// 0-2 //player
    {
        if (!diceRoll.attacking && monsters.Count > 0)
        {
            buttonAttack[i].interactable = false;//ปิดปุ่ม
            diceRoll.attacking = true;
            if (dataPlayer.weaponTwoHand)
            {
                dataPlayer.PlayAnimation(i + 2);
            }
            else
            {
                dataPlayer.PlayAnimation(i + 1);
            }
            movePlayer = true;
            diceRoll.RollToHit(atkBonus + dataPlayer.bonus, addDice, 0);//0 = player, 1 = enemy
            StartCoroutine(Damage(dataPlayer.diceDamage, atkBonus, addDice));
        }
        else if (!diceRoll.attacking)
        {
            buttonAttack[i].interactable = false;//ปิดปุ่ม
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
        yield return new WaitForSeconds(diceRoll.timeClose * 1.5f);
        movePlayer = false;
        yield return new WaitForSeconds(diceRoll.timeClose);
        if (dice != 0)
        {
            yield return new WaitForSeconds(diceRoll.timeClose);
        }
        if (diceRoll.critical)
        {
            yield return new WaitForSeconds(diceRoll.timeClose);
        }
        if (addDiceInMethod != 0)
        {
            yield return new WaitForSeconds(diceRoll.timeClose);
        }
        Debug.Log("result : " + diceRoll.allResult);
        if (diceRoll.allResult >= monsters[targetMons].armorClass)
        {
            diceRoll.RollDamage(dice, bonus, addDiceInMethod, 0);
            yield return new WaitForSeconds(2 * diceRoll.timeClose);
            if (dice != 0)
            {
                yield return new WaitForSeconds(diceRoll.timeClose);
            }
            if (diceRoll.critical)
            {
                yield return new WaitForSeconds(diceRoll.timeClose);
            }
            if (addDiceInMethod != 0)
            {
                yield return new WaitForSeconds(diceRoll.timeClose);
            }
            monsters[targetMons].takeDamage = diceRoll.allResult;
            yield return new WaitForSeconds(diceRoll.timeClose);
        }
        else
        {
            monsters[targetMons].showMissImage = true;
            diceRoll.attacking = false;
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
        else if (numMon == monsters.Count - 1)//have 1 mon
        {
            foreach (var item in createMonster.buttomTarget)
            {
                item.SetActive(false);
            }
        }
        createMonster.buttomTarget[idMonDie].SetActive(false);
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
            else//if str
            {
                atkBonus = atkSTR;
            }
        }
        int numText = atkBonus + dataPlayer.bonus;
        if (buttonAttack.Length > 0)
        {
            buttonAttack[0].gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "left hand \n d20 + " + numText;
            buttonAttack[1].gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "right hand \n d20 + " + numText;
            buttonAttack[2].gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "both hand \n d20 + " + numText;
        }
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
        if (diceRoll != null)
        {
            if (!diceRoll.attacking && monsters.Count > 0)
            {
                StopAllCoroutines();
                StartCoroutine(DelayMonsterAttack());
            }
            else if (!diceRoll.attacking && monsters.Count == 0)
            {
                Debug.Log("Next");
                saveManager.SaveGame();
                uiScript.nextScene = true;
            }
        }
        else
        {
            saveManager.SaveGame();
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
        foreach (Button item in buttonAttack)
        {
            item.interactable = true;
        }
        if (diceRoll != null)
        {
            diceRoll.attacking = false;
            endTurn.interactable = true;
        }
        monAttack = false;
        StopAllCoroutines();
    }
    private void Awake()
    {
        dataPlayer = GetComponent<DataPlayerScript>();
    }
    private void Start()
    {
        saveManager.LoadGameButtom();

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
