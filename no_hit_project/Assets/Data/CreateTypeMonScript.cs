using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Monter",menuName = "Create Obj/Monter")]
public class CreateTypeMonScript : ScriptableObject
{
    public int id;
    public string monName;
    public Vector2Int hitPoint;
    public int armorClass;
    //public GameObject model;
    public Vector2Int moneyDrop;
    public int xpDrop;
    public Vector2Int numDiceDrop;
    public int[] typeDiceDrop;
}
