using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateMonsterScript : MonoBehaviour
{
    [SerializeField] private CreateTypeMonScript[] dataMon;
    [SerializeField] private GameObject[] monsterObj;
    [SerializeField] private Transform[] spawnPointMon;
    public void CreateMon()
    {
        GameObject mon = Instantiate(monsterObj[0], spawnPointMon[0], false);
        MonsterScript monScript = mon.GetComponent<MonsterScript>();
        monScript.id = 0;
        monScript.monName = dataMon[0].monName;
        monScript.hitPoint = Random.Range(dataMon[0].hitPoint.x, dataMon[0].hitPoint.y + 1);
        monScript.armorClass = dataMon[0].armorClass;
        monScript.moneyDrop = Random.Range(dataMon[0].moneyDrop.x, dataMon[0].moneyDrop.y + 1);
        monScript.xpDrop = dataMon[0].xpDrop;
        int r = Random.Range(dataMon[0].numDiceDrop.x, dataMon[0].numDiceDrop.y + 1);
        for (int i = 0; i < r; i++)
        {
            monScript.diceDrop.Add(Random.Range(0, dataMon[0].typeDiceDrop.Length));
        }
    }
    private void Start()
    {
        CreateMon();
    }
}
