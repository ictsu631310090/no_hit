using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewDiceRollScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberText;
    [HideInInspector] public int allResult;
    [SerializeField] private Animator animationUI;
    [SerializeField] private GameObject diceUIPrefab;
    [SerializeField] private Transform spwanDice;
    [SerializeField] private Texture[] diceTextur;
    [SerializeField] private TextMeshProUGUI whoUseText;
    [Range (0.0f,1.0f)]
    public float timeClose;
    [HideInInspector] public bool monsAttacking;
    [HideInInspector] public bool critical;
    [HideInInspector] public List<GameObject> allDice;
    public void RollToHit(int bonus,int diceAdd, int who)
    {
        animationUI.SetBool("open", true);
        ClearAllDice();
        StopCoroutine((TimeDelayRoll(0, 0, 0, 0, 0)));
        StartCoroutine(TimeDelayRoll(1, 20, bonus, diceAdd, 0));
        
        switch (who)
        {
            case 0:
                whoUseText.text = "Player To Hit";
                break;
            case 1:
                whoUseText.text = "Monster To Hit";
                break;
            default:
                break;
        }
    }
    IEnumerator TimeDelayRoll(int numDice, int max, int bonus, int diceAdd , int imageDice)
    {
        allResult = 0;
        for (int j = 0; j < numDice; j++)
        {
            GameObject diceObj = Instantiate(diceUIPrefab, spwanDice, false);
            ChangeDiceImage(diceObj, max);
            allResult += Random.Range(1, max + 1);
            diceObj.transform.GetComponentInChildren<TextMeshProUGUI>().text = allResult.ToString();
            numberText.text = allResult.ToString();
            allDice.Add(diceObj);
        }
        if (critical)
        {
            yield return new WaitForSeconds(timeClose);
            GameObject diceObj2 = Instantiate(diceUIPrefab, spwanDice, false);
            ChangeDiceImage(diceObj2, max);
            allResult += Random.Range(1, max + 1);
            diceObj2.transform.GetComponentInChildren<TextMeshProUGUI>().text = allResult.ToString();
            numberText.text = allResult.ToString();
            allDice.Add(diceObj2);
            critical = false;
        }
        if (allResult == 20)
        {
            critical = true;
        }
        if (bonus != 0)
        {
            yield return new WaitForSeconds(timeClose);
            GameObject objAdd = Instantiate(diceUIPrefab, spwanDice, false);
            ChangeDiceImage(objAdd, imageDice);
            if (bonus > 0)
            {
                objAdd.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "+ " + bonus.ToString() + " ";
            }
            else
            {
                objAdd.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = bonus.ToString() + " ";
            }
            allResult += bonus;
            if (allResult <= 0)
            {
                allResult = 1;//fix
            }
            numberText.text = allResult.ToString();
            allDice.Add(objAdd);
        }
        if (diceAdd != 0)
        {
            int result = Random.Range(1, diceAdd + 1);
            yield return new WaitForSeconds(timeClose);
            GameObject objAdd = Instantiate(diceUIPrefab, spwanDice, false);
            ChangeDiceImage(objAdd, diceAdd);
            allResult += result;
            objAdd.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "+ " + result.ToString() + " ";
            numberText.text = allResult.ToString();
            allDice.Add(objAdd);
        }
        yield return new WaitForSeconds(timeClose);
        foreach (var item in allDice)
        {
            item.GetComponent<Animator>().SetBool("open", false);
        }
        animationUI.SetBool("open", false);
        yield return new WaitForSeconds(timeClose);
    }
    private void ChangeDiceImage(GameObject obj,int diceImage)
    {
        GameObject image = obj.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        switch (diceImage)
        {
            case 4:
                image.GetComponent<RawImage>().texture = diceTextur[0];
                break;
            case 6:
                image.GetComponent<RawImage>().texture = diceTextur[1];
                break;
            case 8:
                image.GetComponent<RawImage>().texture = diceTextur[2];
                break;
            case 10:
                image.GetComponent<RawImage>().texture = diceTextur[3];
                break;
            case 12:
                image.GetComponent<RawImage>().texture = diceTextur[4];
                break;
            case 20:
                image.GetComponent<RawImage>().texture = diceTextur[5];
                break;
            default:
                image.GetComponent<RawImage>().texture = diceTextur[6];
                break;
        }//dice image
    }
    private void ClearAllDice()
    {
        foreach (var item in allDice)
        {
            Destroy(item);
        }
        allDice.Clear();
    }
    public void RollDamage(int numDice,int max, int bonus, int diceAdd, int who)
    {
        animationUI.SetBool("open", true);
        ClearAllDice();
        StopCoroutine((TimeDelayRoll(0, 0, 0, 0, 0)));
        StartCoroutine(TimeDelayRoll(numDice, max, bonus, diceAdd, who));        

        switch (who)
        {
            case 0:
                whoUseText.text = "Player Damage";
                break;
            case 1:
                whoUseText.text = "Monster Damage";
                break;
            case 2:
                whoUseText.text = "Player Heal";
                break;
            default:
                break;
        }
    }
    private void Start()
    {
        monsAttacking = false;
        critical = false;
    }
}
