using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Create Obj/Weapon")]
public class CreateWeaponScript : ScriptableObject
{
    public int id;
    public string nameWeapon;
    public int price;
    public bool finesse;
    public bool twoHand;
    public int damage;
    public bool canTwoHand;
    public int damageTwoHand;
    public Sprite image;
}
