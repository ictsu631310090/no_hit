using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowPlayerScript : MonoBehaviour
{
    [Header("link Obj")]
    [HideInInspector] public DataPlayerScript dataPlayer;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI acText;
    [SerializeField] private Image hpbar;
    public Animator animaPlayer;
    [HideInInspector] public bool showMiss;
    [SerializeField] private Animator missPlayer;
    public SpriteRenderer[] modelPlayer;

    [SerializeField] private GameObject statusText;
    private TextMeshProUGUI strText;
    private TextMeshProUGUI strMoText;
    private TextMeshProUGUI dexText;
    private TextMeshProUGUI dexMoText;
    private TextMeshProUGUI conText;
    private TextMeshProUGUI conMoText;
    [SerializeField] private TextMeshProUGUI levelText;
    public void UpdateTextHp()
    {
        hpText.text = dataPlayer.hitPoint.ToString() + " / " + dataPlayer.hitPointMax.ToString();
        float hp = dataPlayer.hitPoint;
        float hpMax = dataPlayer.hitPointMax;
        hpbar.fillAmount = hp / hpMax;
    }
    private void UpStatus()
    {
        if (dataPlayer.pointLevel != 0)
        {
            levelText.text = "Lv." + dataPlayer.level + " Point : " + dataPlayer.pointLevel;
        }
        else
        {
            levelText.text = "Lv." + dataPlayer.level;
        }
    }
    public void UpdateStatus()
    {
        strText.text = dataPlayer.str.ToString();
        if ((dataPlayer.str - 10) / 2 > 0)
        {
            strMoText.text = "+" + ((dataPlayer.str - 10) / 2).ToString();
        }
        else
        {
            strMoText.text =((dataPlayer.str - 10) / 2).ToString();
        }

        dexText.text = dataPlayer.dex.ToString();
        if ((dataPlayer.dex - 10) / 2 > 0)
        {
            dexMoText.text = "+" + ((dataPlayer.dex - 10) / 2).ToString();
        }
        else
        {
            dexMoText.text = ((dataPlayer.dex - 10) / 2).ToString();
        }

        conText.text = dataPlayer.con.ToString();
        if ((dataPlayer.con - 10) / 2 > 0)
        {
            conMoText.text = "+" + ((dataPlayer.con - 10) / 2).ToString();
        }
        else
        {
            conMoText.text = ((dataPlayer.con - 10) / 2).ToString();
        }
    }
    public void UpdateACText()
    {
        acText.text = dataPlayer.armorClass.ToString();
    }
    private void CheckMiss()
    {
        if (showMiss)
        {
            missPlayer.SetBool("show", true);
            StartCoroutine(DelayCloseMiss());
        }
    }
    IEnumerator DelayCloseMiss()
    {
        showMiss = false;
        yield return new WaitForSeconds(0.5f);
        missPlayer.SetBool("show", false);
    }
    private void Awake()
    {
        strText = statusText.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        strMoText = statusText.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        dexText = statusText.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        dexMoText = statusText.transform.GetChild(1).gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        conText = statusText.transform.GetChild(2).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        conMoText = statusText.transform.GetChild(2).gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        showMiss = false;

        acText.text = dataPlayer.armorClass.ToString();

        UpdateTextHp();

        levelText.text = "Lv." + dataPlayer.level;

        UpdateStatus();
        UpdateACText();
    }
    private void Update()
    {
        UpStatus();
        CheckMiss();
    }
}
