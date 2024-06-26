using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class UIScript : MonoBehaviour
{
    public Animator blackScene;

    public int moneyPlayer;
    public static int addMoney;

    //Script
    [SerializeField] private GameObject anotherScript;
    [HideInInspector] public DataPlayerScript dataPlayer;
    private CombatScript combat;

    [Header ("UI")]
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private GameObject pauseUI;
    private bool pause;
    [HideInInspector] public bool nextScene;

    [Header ("Bag EquipMent")]
    [SerializeField] private GameObject bagUI;
    private bool openBag;
    [SerializeField] private GameObject[] itemCanva;//0 = weapon, 1 = shield, 2 = armor
    [SerializeField] private Transform createShowItem;
    [HideInInspector] public List<GameObject> showItemObj;
    [SerializeField] private Image itemImage;
    public TextMeshProUGUI warnText;

    [Header("Bag Dice")]
    [SerializeField] private GameObject bagDiceUI;
    private bool openDiceBag;
    [SerializeField] private TextMeshProUGUI[] numDiceText;
    [SerializeField] private Button[] useDiceButton;

    //Equipment
    public void OpenBagUI()
    {
        if (!openBag)
        {
            ShowEquipment();
            bagUI.SetActive(true);
            openBag = true;
        }
        else
        {
            bagUI.SetActive(false);
            openBag = false;
        }
    }
    private void ShowEquipment()
    {
        ClearItemShow();
        for (int i = 0; i < dataPlayer.listWeapon.Count; i++)
        {
            GameObject itemShow = Instantiate(itemCanva[0], createShowItem, false);
            itemShow.GetComponent<ItemWeaponUIScript>().dataWeapon = dataPlayer.listWeapon[i];
            itemShow.GetComponent<ItemWeaponUIScript>().mainUI = this;
            itemShow.GetComponent<ItemWeaponUIScript>().combat = combat;
            showItemObj.Add(itemShow);
            //Debug.Log(dataPlayer.listWeapon.Count);
        }
        for (int i = 0; i < dataPlayer.listShield.Count; i++)
        {
            GameObject itemShow = Instantiate(itemCanva[1], createShowItem, false);
            itemShow.GetComponent<ItemShieldUIScript>().dataShield = dataPlayer.listShield[i];
            itemShow.GetComponent<ItemShieldUIScript>().mainUI = this;
            itemShow.GetComponent<ItemShieldUIScript>().combat = combat;
            showItemObj.Add(itemShow);
        }
        for (int i = 0; i < dataPlayer.listArmor.Count; i++)
        {
            GameObject itemShow = Instantiate(itemCanva[2], createShowItem, false);
            itemShow.GetComponent<ItemArmorUIScript>().dataArmor = dataPlayer.listArmor[i];
            itemShow.GetComponent<ItemArmorUIScript>().mainUI = this;
            itemShow.GetComponent<ItemArmorUIScript>().combat = combat;
            showItemObj.Add(itemShow);
        }

        if (showItemObj.Count > 0)
        {
            if (dataPlayer.listWeapon.Count > 0)
            {
                ImageItemShow(showItemObj[0].GetComponent<ItemWeaponUIScript>().dataWeapon.image);
            }
            else if (dataPlayer.listShield.Count > 0)
            {
                ImageItemShow(showItemObj[0].GetComponent<ItemShieldUIScript>().dataShield.image);
            }
            else if (dataPlayer.listArmor.Count > 0)
            {
                ImageItemShow(showItemObj[0].GetComponent<ItemArmorUIScript>().dataArmor.image[1]);
            }
        }
    }
    public void ImageItemShow(Sprite i)
    {
        itemImage.sprite = i;
        itemImage.SetNativeSize();
        //float sizeX = itemImage.GetComponent<RectTransform>().sizeDelta.x;
        //float sizeY = itemImage.GetComponent<RectTransform>().sizeDelta.y;
        //itemImage.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeX * 1.5f, sizeY * 1.5f);
    }
    private void ClearItemShow()
    {
        foreach (var item in showItemObj)
        {
            Destroy(item);
        }
        showItemObj.Clear();
    }
    //Dice Bag
    public void OpenBagDiceUI()
    {
        if (!openDiceBag)
        {
            UpdateNumDice();
            UpdateButtonDice();
            bagDiceUI.SetActive(true);
            openDiceBag = true;
        }
        else
        {
            UpdateNumDice();
            UpdateButtonDice();
            bagDiceUI.SetActive(false);
            openDiceBag = false;
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
        OpenBagDiceUI();
    }
    //Pause UI
    public void PauseGame()
    {
        if (!pause)
        {
            pause = true;
            pauseUI.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pause = false;
            Time.timeScale = 1;
            pauseUI.SetActive(false);
        }
    }
    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void BackMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);//main menu
    }
    //Scene
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
        //SceneManager.LoadScene(0);//main menu test
        SceneManager.LoadScene(2);//go to map
    }
    private void Awake()
    {
        dataPlayer = anotherScript.GetComponent<DataPlayerScript>();
        combat = anotherScript.GetComponent<CombatScript>();
    }
    private void Start()
    {
        if (moneyText != null)
        {
            moneyText.text = moneyPlayer.ToString();
        }
        if (bagDiceUI != null)
        {
            bagDiceUI.SetActive(false);
        }
        if (bagUI != null)
        {
            bagUI.SetActive(false);
        }
        if (pauseUI != null)
        {
            pauseUI.SetActive(false);
        }

        openBag = false;
        openDiceBag = false;
        pause = false;
        nextScene = false;
        if (warnText != null)
        {
            warnText.text = null;
        }
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
}
