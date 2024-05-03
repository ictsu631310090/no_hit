using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shield", menuName = "Create Obj/Shield")]
public class CreateShieldScript : ScriptableObject
{
    public int id;
    public string nameWeapon;
    public int price;
    public int addAC;
    public Sprite image;
}
