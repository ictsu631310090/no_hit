using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor", menuName = "Create Obj/Armor")]
public class CreateArmorScript : ScriptableObject
{
    public int id;
    public string nameArmor;
    public int price;
    public int setAC;
    public bool light;
    public bool heavy;
    public int condition;//str
    public Sprite[] image;//หัว, ตัว, แขนท่อนบนขวา, แขนท่อนบนซ้าย, แขนท่อนล่างขวา, แขนท่อนล่างซ้าย, ขาท่อนบนขวา, ขาท่อนบนซ้าย, ขาท่อนล่างขวา, ขาท่อนล่างซ้าย, หน้า2, หน้า3
}
