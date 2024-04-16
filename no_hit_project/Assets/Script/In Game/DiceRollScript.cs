using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiceRollScript : MonoBehaviour
{
    [HideInInspector] public int result;
    [SerializeField] private GameObject deplayDice;
    [SerializeField] private RawImage diceImage;
    [SerializeField] private Texture[] diceTextur;
    [SerializeField] private TextMeshProUGUI numberText;
    [SerializeField] private TextMeshProUGUI detailText;
    public float timeClose;
    [HideInInspector] public bool willAttack;
    public void RollDice(int max, int bonus, bool critical , int type)
    {
        //max => what dice roll.
        //bonus => add bonus.
        //critical => critical or not. 
        //type => 0 = player Attack, 1 = monster Attack , 2 = heal Player.
        willAttack = true;
        StopCoroutine(DelayClose(0, 0, false));
        switch (max)
        {
            case 4:
                diceImage.texture = diceTextur[0];
                break;
            case 6:
                diceImage.texture = diceTextur[1];
                break;
            case 8:
                diceImage.texture = diceTextur[2];
                break;
            case 10:
                diceImage.texture = diceTextur[3];
                break;
            case 12:
                diceImage.texture = diceTextur[4];
                break;
            case 20:
                diceImage.texture = diceTextur[5];
                break;
            default:
                break;
        }//dice image
        switch (type)
        {
            case 0:
                detailText.text = "Player \nd" + max;
                deplayDice.GetComponent<RawImage>().color = new Color(0, 1, 1, 0.5f);//blue sky
                break;
            case 1:
                detailText.text = "Monster \nd" + max;
                deplayDice.GetComponent<RawImage>().color = new Color(1, 0, 0, 0.5f);//red
                break;
            case 2:
                detailText.text = "Heal PLayer \nd" + max;
                deplayDice.GetComponent<RawImage>().color = new Color(0, 1, 0, 0.5f);//green
                break;
            default:
                break;
        }
        if (bonus != 0)
        {
            detailText.text += "+" + bonus;
        }
        deplayDice.SetActive(true);
        deplayDice.GetComponent<Animator>().SetBool ("Open",true);
        result = Random.Range(1, max + 1);
        //Debug.Log("result : " + result);
        numberText.text = result.ToString();
        //Debug.Log("text : " + numberText.text);
        StartCoroutine(DelayClose(timeClose, bonus , critical));
    }
    IEnumerator DelayClose(float time,int bonus , bool critical)
    {
        if (critical)
        {
            yield return new WaitForSeconds(time * 0.5f);
            result += result;
            numberText.text = result.ToString();
            Debug.Log("Damage critical : " + result);
        }
        yield return new WaitForSeconds(time * 0.5f);
        if (bonus != 0)
        {
            for (int i = 0; i < bonus; i++)
            {
                yield return new WaitForSeconds((time * 0.5f) / bonus);
                result += 1;
                numberText.text = result.ToString();
            }
        }//add bonus
        yield return new WaitForSeconds(time);
        deplayDice.GetComponent<Animator>().SetBool("Open", false);
        yield return new WaitForSeconds(0.7f * time);
        deplayDice.SetActive(false);
        willAttack = false;
    }
    private void Start()
    {
        willAttack = false;
        deplayDice.SetActive(false);
    }
}
