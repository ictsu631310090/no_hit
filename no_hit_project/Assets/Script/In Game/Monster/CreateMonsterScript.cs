using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMonsterScript : MonoBehaviour
{
    [SerializeField] private CreateTypeMonScript[] dataMon;
    [SerializeField] private GameObject monsterObj;
    public Transform[] spawnPointMon;
    private CombatScript combat;
    public void CreateMon(int numOfMon)
    {
        for (int i = 0; i < numOfMon; i++)
        {
            GameObject mon = Instantiate(monsterObj, spawnPointMon[i], false);
            MonsterScript monScript = mon.GetComponent<MonsterScript>();
            int typeMon = Random.Range(0, dataMon.Length);
            monScript.id = i;
            monScript.monName = dataMon[typeMon].monName;
            monScript.hitPoint = Random.Range(dataMon[typeMon].hitPoint.x, dataMon[typeMon].hitPoint.y + 1) + dataMon[typeMon].hitPoint.z;
            monScript.armorClass = dataMon[typeMon].armorClass;
            monScript.toHitPlus = dataMon[typeMon].toHitPlus;
            monScript.damage = dataMon[typeMon].damage;
            monScript.moneyDrop = Random.Range(dataMon[typeMon].moneyDrop.x, dataMon[typeMon].moneyDrop.y + 1);
            monScript.xpDrop = dataMon[typeMon].xpDrop;
            monScript.combat = combat;
            GameObject modelMon = Instantiate(dataMon[typeMon].model, monScript.GetComponent<Transform>(), false);
            monScript.animaMon = modelMon.GetComponent<Animator>();
            int r = Random.Range(dataMon[typeMon].numDiceDrop.x, dataMon[typeMon].numDiceDrop.y + 1);//dice
            for (int j = 0; j < r; j++)
            {
                monScript.diceDrop.Add(Random.Range(0, dataMon[typeMon].typeDiceDrop.Length));
            }
            combat.monsters.Add(monScript);
        }
        combat.lightTarget.transform.parent = combat.monsters[numOfMon - 1].transform.GetChild(1);
        combat.lightTarget.transform.position = combat.monsters[numOfMon - 1].transform.GetChild(1).position;
        combat.targetMons = numOfMon - 1;//front
    }
    private void Awake()
    {
        combat = GetComponent<CombatScript>();
    }
}
