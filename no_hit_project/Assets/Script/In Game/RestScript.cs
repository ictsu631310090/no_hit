using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestScript : MonoBehaviour
{
    private DataPlayerScript dataPlayer;
    //private CombatScript combat;
    private void Awake()
    {
        dataPlayer = GetComponent<DataPlayerScript>();
        //combat = GetComponent<CombatScript>();
    }
    private void Start()
    {
        dataPlayer.healHitPoint = dataPlayer.hitPointMax;
    }
    private void Update()
    {
        if (dataPlayer.weaponTwoHand)
        {
            dataPlayer.showPlayer.animaPlayer.SetInteger("step", -2);
        }
        else
        {
            dataPlayer.showPlayer.animaPlayer.SetInteger("step", -1);
        }
    }
}
