using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMonsterScript : MonoBehaviour
{
    [Header("Link Script")]
    [SerializeField] private CombatScript combat;

    [Header("set data")]
    [SerializeField] private SpriteRenderer bgImage;
    [Range(1, 3)]
    [SerializeField] private int numOfMon;
    [SerializeField] private CreateTypeMonScript[] dataMon;
    [SerializeField] private GameObject monsterObj;
    public Transform[] spawnPointMon;
    public GameObject[] buttomTarget;
    public void CreateMon(int numOfMon)
    {
        for (int i = 0; i < numOfMon; i++)
        {
            GameObject mon = Instantiate(monsterObj, spawnPointMon[i], false);
            MonsterScript monScript = mon.GetComponent<MonsterScript>();
            int typeMon = Random.Range(0, dataMon.Length);
            monScript.id = i;
            monScript.monName = dataMon[typeMon].monName;
            monScript.hitPoint = SetHPMons(typeMon);
            monScript.armorClass = dataMon[typeMon].armorClass;
            monScript.toHitPlus = dataMon[typeMon].toHitPlus;
            monScript.damage = dataMon[typeMon].damage;
            monScript.moneyDrop = Random.Range(dataMon[typeMon].moneyDrop.x, dataMon[typeMon].moneyDrop.y + 1);
            monScript.xpDrop = dataMon[typeMon].xpDrop;
            monScript.combat = combat;
            GameObject modelMon = Instantiate(dataMon[typeMon].model, monScript.GetComponent<Transform>(), false);
            monScript.model = modelMon;
            int r = Random.Range(dataMon[typeMon].numDiceDrop.x, dataMon[typeMon].numDiceDrop.y + 1);//num dice
            for (int j = 0; j < r; j++)
            {
                int rDice = Random.Range(0, dataMon[typeMon].typeDiceDrop.Length);
                monScript.diceDrop.Add(dataMon[typeMon].typeDiceDrop[rDice]);
            }
            combat.monsters.Add(monScript);
        }
        combat.lightTarget.transform.parent = combat.monsters[numOfMon - 1].transform.GetChild(1);
        combat.lightTarget.transform.position = combat.monsters[numOfMon - 1].transform.GetChild(1).position;
        combat.targetMons = numOfMon - 1;//front
    }
    private int SetHPMons(int typeMon)
    {
        int hpMon = 0;
        if (dataMon[typeMon].hitPoint.x != 1)
        {
            for (int j = 0; j < dataMon[typeMon].hitPoint.x; j++)
            {
                hpMon += Random.Range(1, dataMon[typeMon].hitPoint.y + 1);
            }
            hpMon += dataMon[typeMon].hitPoint.z;
        }
        else
        {
            hpMon = Random.Range(1, dataMon[typeMon].hitPoint.y + 1) + dataMon[typeMon].hitPoint.z;
        }
        if (hpMon <= 0)
        {
            hpMon = 1;
        }
        return hpMon;
    }
    private void Awake()
    {
        numOfMon = MapScript.useNow.numOfMon;
        dataMon = MapScript.useNow.dataMon;
        bgImage.sprite = MapScript.useNow.bgImage;
    }
    private void Start()
    {
        CreateMon(numOfMon);
        for (int i = numOfMon; i < buttomTarget.Length; i++)
        {
            buttomTarget[i].SetActive(false);
        }
    }
}
