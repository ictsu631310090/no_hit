using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMonsterScript : MonoBehaviour
{
    [Range(1, 3)]
    public int numOfMon;

    private CreateMonsterScript createMonster;
    public GameObject[] buttomTarget;
    private void Awake()
    {
        createMonster = GetComponent<CreateMonsterScript>();
    }
    private void Start()
    {
        createMonster.CreateMon(numOfMon);
        for (int i = numOfMon; i < buttomTarget.Length; i++)
        {
            buttomTarget[i].SetActive(false);
        }
    }
}
