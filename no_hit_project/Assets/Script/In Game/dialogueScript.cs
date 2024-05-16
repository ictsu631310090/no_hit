using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class dialogueScript : MonoBehaviour
{
    [Header("Link Script")]
    [SerializeField] private DataPlayerScript dataPlayer;
    [SerializeField] private CreateEventScript[] dataEvent;
    [SerializeField] private UIScript uiScript;
    [SerializeField] private SaveManagerScript saveManager;
    private CreateEventScript useEvent;

    [Header("Link Obj")]
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Image image;
    private int line;
    private bool endLine;
    [HideInInspector] public string[] text;

    [Header("Input data")]
    [SerializeField] private float timeAddNextChar;
    private void RandomEvent()
    {
        int i = Random.Range(0, dataEvent.Length);
        useEvent = dataEvent[i];
        image.sprite = useEvent.imageBG;
        text = new string[useEvent.textTalk.Length + 1];
        for (int x = 0; x < useEvent.textTalk.Length; x++)
        {
            text[x] = useEvent.textTalk[x];
        }
    }
    public void TalkDialogue()
    {
        if (line < text.Length)
        {
            if (endLine)
            {
                endLine = false;
                dialogueText.text = string.Empty;
                StartCoroutine(LineTalk());
            }
            else
            {
                endLine = true;
                StopAllCoroutines();
                dialogueText.text = text[line];
                line++;
            }
        }
        else
        {
            Debug.Log("next scene");
            saveManager.SaveGame();
            uiScript.nextScene = true;
        }
    }
    IEnumerator LineTalk()
    {
        foreach (char item in text[line].ToCharArray())
        {
            dialogueText.text += item;
            yield return new WaitForSeconds(timeAddNextChar);
        }
        endLine = true;
        line++;
    }
    private void Start()
    {
        line = 0;
        useEvent = null;
        endLine = true;
        RandomEvent();
        Debug.Log(text.Length);
        switch (useEvent.typeEvent)
        {
            case 0:
                switch (useEvent.details.x)
                {
                    case 4:
                        dataPlayer.diceHave[0] += useEvent.details.y;
                        break;
                    case 6:
                        dataPlayer.diceHave[1] += useEvent.details.y;
                        break;
                    case 8:
                        dataPlayer.diceHave[2] += useEvent.details.y;
                        break;
                    case 10:
                        dataPlayer.diceHave[3] += useEvent.details.y;
                        break;
                    case 12:
                        dataPlayer.diceHave[4] += useEvent.details.y;
                        break;
                    default:
                        break;
                }
                text[useEvent.textTalk.Length] = "You get " + useEvent.details.y + "D" + useEvent.details.x + ".";
                break;//get dice
            case 1:
                int h = Random.Range(useEvent.details.x, useEvent.details.y);
                dataPlayer.takeDamage = -1 * h;
                text[useEvent.textTalk.Length] = "You get " + h + " heat.";
                break;//heat
            case 2:
                int m = Random.Range(useEvent.details.x, useEvent.details.y);
                UIScript.addMoney = m;
                text[useEvent.textTalk.Length] = "You get " + m + " gold.";
                break;//get money
            case 3:
                Debug.Log("go fight scene");
                text[useEvent.textTalk.Length] = "Get fight!!";
                //go fight scene;
                break;//fight
            case 4:
                int td = Random.Range(useEvent.details.x, useEvent.details.y);
                dataPlayer.takeDamage = -1 * td;
                text[useEvent.textTalk.Length] = "You take " + td + " damage.";
                break;//take damage
            case 5:
                int mm = Random.Range(useEvent.details.x, useEvent.details.y);
                UIScript.addMoney = -1 * mm;
                text[useEvent.textTalk.Length] = "You lost " + mm + " glode.";
                break;//lost money
            default:
                break;
        }
        TalkDialogue();
    }
    private void Update()
    {
        
    }
}
