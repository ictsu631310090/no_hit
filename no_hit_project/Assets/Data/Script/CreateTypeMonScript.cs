using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Monter",menuName = "Create Obj/Monter")]
public class CreateTypeMonScript : ScriptableObject
{
    [Header("Data Mon")]
    public string monName;
    public Vector3Int hitPoint;
    public int armorClass;
    public int toHitPlus;
    public Vector3Int damage;
    public GameObject model;

    [Header("Item Drop")]
    public Vector2Int moneyDrop;
    public int xpDrop;
    public Vector2Int numDiceDrop;
    public int[] typeDiceDrop;
}
