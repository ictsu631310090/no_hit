using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "Create Obj/Event")]
public class CreateEventScript : ScriptableObject
{
    public string[] textTalk;
    public Sprite imageBG;
    public int typeEvent;//0 = get dice, 1 = get heat, 2 = get money, 3 = fight, 4 = take damage, 5 = lost money
    public Vector2Int details;
}
