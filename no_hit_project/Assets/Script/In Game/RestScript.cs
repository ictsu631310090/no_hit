using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestScript : MonoBehaviour
{
    [SerializeField] private DataPlayerScript dataPlayer;
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
