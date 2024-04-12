using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiceRollScript : MonoBehaviour
{
    public int minRoll;
    public int maxRoll;
    public int result;
    [SerializeField] private GameObject deplayDice;
    [SerializeField] private TextMeshProUGUI numberText;
    [SerializeField] private float timeClose;
    public void RollDice()
    {
        deplayDice.SetActive(true);
        deplayDice.GetComponent<Animator>().SetBool ("Open",true);
        result = Random.Range(minRoll, maxRoll + 1);
        numberText.text = result.ToString();
        StartCoroutine(DelayClose(timeClose));
    }
    IEnumerator DelayClose(float time)
    {
        yield return new WaitForSeconds(time);
        deplayDice.GetComponent<Animator>().SetBool("Open", false);
        yield return new WaitForSeconds(2 * time / 3);
        deplayDice.SetActive(false);
    }
    private void Start()
    {
        deplayDice.SetActive(false);
    }
}
