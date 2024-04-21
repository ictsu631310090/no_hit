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
    public float timeClose;
    [HideInInspector] public bool willAttack;
    private bool critical;
    [HideInInspector] public List<GameObject> allDice;

    [Header("Chack")]
    public int bonusTest;
    public int diceAddTest;
    public int typeTest;
    private bool Open;
    public void ButtonChack()
    {
        if (!willAttack)
        {
            if (!Open)
            {
                //RollToHit(bonusTest, diceAddTest, typeTest);
                RollDamage(4, bonusTest, diceAddTest, typeTest);

                Open = true;
            }
            else
            {
                Open = false;

            }
        }
    }
    public void RollToHit(int bonus,int diceAdd, int who)
    {
        willAttack = true;
        animationUI.SetBool("open", true);
        ClearAllDice();
        StopCoroutine((TimeDelayRoll(0,0,0,0)));
        StartCoroutine(TimeDelayRoll(20, bonus, diceAdd, 0));
        
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
    IEnumerator TimeDelayRoll(int max, int bonus, int diceAdd , int imageDice)
    {
        GameObject diceObj = Instantiate(diceUIPrefab, spwanDice, false);
        ChangeDiceImage(diceObj, max);
        allResult = Random.Range(1, max + 1);
        diceObj.transform.GetComponentInChildren<TextMeshProUGUI>().text = allResult.ToString();
        numberText.text = allResult.ToString();
        allDice.Add(diceObj);
        if (critical)
        {
            yield return new WaitForSeconds(timeClose);
            GameObject diceObj2 = Instantiate(diceUIPrefab, spwanDice, false);
            ChangeDiceImage(diceObj2, max);
            allResult = Random.Range(1, max + 1);
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
            allResult += bonus;
            objAdd.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "+ " + bonus.ToString() + " ";
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
        animationUI.SetBool("open", false);
        foreach (var item in allDice)
        {
            item.GetComponent<Animator>().SetBool("open", false);
        }
        Open = false;//don't forget remove
    }
    private void ChangeDiceImage(GameObject obj,int diceImage)
    {
        GameObject image = obj.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        switch (diceImage)
        {
            case 0:
                image.GetComponent<RawImage>().texture = diceTextur[6];
                break;
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
    public void RollDamage(int max, int bonus, int diceAdd, int who)
    {
        animationUI.SetBool("open", true);
        ClearAllDice();
        StopCoroutine((TimeDelayRoll(0, 0, 0, 0)));
        StartCoroutine(TimeDelayRoll(max, bonus, diceAdd, who));        

        switch (who)
        {
            case 0:
                whoUseText.text = "Player Damage";
                break;
            case 1:
                whoUseText.text = "Monster Damage";
                break;
            default:
                break;
        }
        willAttack = false;
    }
    public void RollDice(int max, int bonus, bool critical , int type)
    {
        //max => what dice roll.
        //bonus => add bonus.
        //critical => critical or not. 
        //type => 0 = player Attack, 1 = monster Attack , 2 = heal Player.
        willAttack = true;
        StopCoroutine(DelayClose(0, 0, false));
        //ChangeDiceImage();
        //switch (type)
        //{
        //    case 0:
        //        whoUseText.text = "Player";
        //        deplayDice.GetComponent<RawImage>().color = new Color(0, 1, 1, 0.5f);//blue sky
        //        break;
        //    case 1:
        //        whoUseText.text = "Monster";
        //        deplayDice.GetComponent<RawImage>().color = new Color(1, 0, 0, 0.5f);//red
        //        break;
        //    case 2:
        //        whoUseText.text = "Heal PLayer";
        //        deplayDice.GetComponent<RawImage>().color = new Color(0, 1, 0, 0.5f);//green
        //        break;
        //    default:
        //        break;
        //}
        if (bonus != 0)
        {
            whoUseText.text += "+" + bonus;
        }
        //deplayDice.GetComponent<Animator>().SetBool ("Open",true);
        allResult = Random.Range(1, max + 1);
        //Debug.Log("result : " + result);
        numberText.text = allResult.ToString();
        //Debug.Log("text : " + numberText.text);
        StartCoroutine(DelayClose(timeClose, bonus , critical));
    }//old
    IEnumerator DelayClose(float time,int bonus , bool critical)
    {
        if (critical)
        {
            yield return new WaitForSeconds(time * 0.5f);
            allResult += allResult;
            numberText.text = allResult.ToString();
            //Debug.Log("Damage critical : " + result);
        }
        yield return new WaitForSeconds(time * 0.5f);
        if (bonus != 0)
        {
            for (int i = 0; i < bonus; i++)
            {
                yield return new WaitForSeconds((time * 0.5f) / bonus);
                allResult += 1;
                numberText.text = allResult.ToString();
            }
        }//add bonus
        yield return new WaitForSeconds(time);
        //deplayDice.GetComponent<Animator>().SetBool("Open", false);
        yield return new WaitForSeconds(0.7f * time);
        willAttack = false;
    }//old
    private void Start()
    {
        willAttack = false;
        critical = false;

        Open = false;
    }
}
