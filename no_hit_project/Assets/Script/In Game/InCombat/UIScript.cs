using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class UIScript : MonoBehaviour
{
    public Animator blackScene;
    private int moneyPlayer;
    public static int addMoney;
    [SerializeField] private DataPlayerScript dataPlayer;
    [SerializeField] private CombatScript combat;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private GameObject mapUI;
    private bool openMap;
    [SerializeField] private GameObject bagUI;
    private bool openBag;
    [SerializeField] private TextMeshProUGUI[] numDiceText;
    [SerializeField] private Button[] useDiceButton;
    [HideInInspector] public bool nextScene;
    public void OpenMapUI()
    {
        if (!openMap)
        {
            mapUI.SetActive(true);
            openMap = true;
        }
        else
        {
            mapUI.SetActive(false);
            openMap = false;
        }
    }
    public void OpenBagUI()
    {
        if (!openBag)
        {
            bagUI.SetActive(true);
            openBag = true;
        }
        else
        {
            bagUI.SetActive(false);
            openBag = false;
        }
    }
    private void UpdateNumDice()
    {
        for (int i = 0; i < numDiceText.Length; i++)
        {
            numDiceText[i].text = dataPlayer.diceHave[i].ToString() + " D" + ((i * 2) + 4).ToString();
        }
    }
    private void UpdateButtonDice()
    {
        for (int i = 0; i < useDiceButton.Length; i++)
        {
            if (dataPlayer.diceHave[i] == 0)
            {
                useDiceButton[i].interactable = false;
            }
            else
            {
                useDiceButton[i].interactable = true;
            }
        }
    }
    public void AddDice(int dice)
    {
        switch (dice)
        {
            case 0:
                if (dataPlayer.diceHave[dice] > 0)
                {
                    if (combat.addDice == 0)
                    {
                        combat.addDice = 4;
                        dataPlayer.diceHave[dice]--;
                    }
                    else
                    {
                        switch (combat.addDice)
                        {
                            case 6:
                                dataPlayer.diceHave[1]++;
                                break;
                            case 8:
                                dataPlayer.diceHave[2]++;
                                break;
                            case 10:
                                dataPlayer.diceHave[3]++;
                                break;
                            case 12:
                                dataPlayer.diceHave[4]++;
                                break;
                            default:
                                break;
                        }
                        combat.addDice = 4;
                    }
                }
                break;
            case 1:
                if (dataPlayer.diceHave[dice] > 0)
                {
                    if (combat.addDice == 0)
                    {
                        combat.addDice = 6;
                        dataPlayer.diceHave[dice]--;
                    }
                    else
                    {
                        switch (combat.addDice)
                        {
                            case 4:
                                dataPlayer.diceHave[0]++;
                                break;
                            case 8:
                                dataPlayer.diceHave[2]++;
                                break;
                            case 10:
                                dataPlayer.diceHave[3]++;
                                break;
                            case 12:
                                dataPlayer.diceHave[4]++;
                                break;
                            default:
                                break;
                        }
                        combat.addDice = 6;
                    }
                }
                break;
            case 2:
                if (dataPlayer.diceHave[dice] > 0)
                {
                    if (combat.addDice == 0)
                    {
                        combat.addDice = 8;
                        dataPlayer.diceHave[dice]--;
                    }
                    else
                    {
                        switch (combat.addDice)
                        {
                            case 4:
                                dataPlayer.diceHave[0]++;
                                break;
                            case 6:
                                dataPlayer.diceHave[1]++;
                                break;
                            case 10:
                                dataPlayer.diceHave[3]++;
                                break;
                            case 12:
                                dataPlayer.diceHave[4]++;
                                break;
                            default:
                                break;
                        }
                        combat.addDice = 8;
                    }
                }
                break;
            case 3:
                if (dataPlayer.diceHave[dice] > 0)
                {
                    if (combat.addDice == 0)
                    {
                        combat.addDice = 10;
                        dataPlayer.diceHave[dice]--;
                    }
                    else
                    {
                        switch (combat.addDice)
                        {
                            case 4:
                                dataPlayer.diceHave[0]++;
                                break;
                            case 6:
                                dataPlayer.diceHave[1]++;
                                break;
                            case 8:
                                dataPlayer.diceHave[2]++;
                                break;
                            case 12:
                                dataPlayer.diceHave[4]++;
                                break;
                            default:
                                break;
                        }
                        combat.addDice = 10;
                    }
                }
                break;
            case 4:
                if (dataPlayer.diceHave[dice] > 0)
                {
                    if (combat.addDice == 0)
                    {
                        combat.addDice = 12;
                        dataPlayer.diceHave[dice]--;
                    }
                    else
                    {
                        switch (combat.addDice)
                        {
                            case 4:
                                dataPlayer.diceHave[0]++;
                                break;
                            case 6:
                                dataPlayer.diceHave[1]++;
                                break;
                            case 8:
                                dataPlayer.diceHave[2]++;
                                break;
                            case 10:
                                dataPlayer.diceHave[3]++;
                                break;
                            default:
                                break;
                        }
                        combat.addDice = 12;
                    }
                }
                break;
            default:
                break;
        }//0 = 4, 1 = 6, 2 = 8, 3 = 10, 4 = 12
        OpenBagUI();
    }
    private void CloseScene()
    {
        if (nextScene)
        {
            blackScene.SetBool("close", true);
            StartCoroutine(DelayChangeScene());
        }
    }
    IEnumerator DelayChangeScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);//main menu test
    }
    private void Start()
    {
        moneyPlayer = 0;

        moneyText.text = moneyPlayer.ToString();
        mapUI.SetActive(false);
        openMap = false;
        bagUI.SetActive(false);
        openBag = false;

        nextScene = false;
    }
    void Update()
    {
        if (addMoney != 0)
        {
            moneyPlayer += addMoney;
            addMoney = 0;
            moneyText.text = moneyPlayer.ToString();
        }
        CloseScene();
        UpdateNumDice();
        UpdateButtonDice();
    }
}
