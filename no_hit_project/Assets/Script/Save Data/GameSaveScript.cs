using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSaveScript
{
    public int hitPoint;
    public int str;//0
    public int dex;//1
    public int con;//2
    public int xp;
    public int levelPlayer;
    public int bonus;
    //public List<int> itemUse;
    //public List<GameObject> diceHave;

    public GameSaveScript (DataPlayerScript dataPlayer)
    {
        hitPoint = dataPlayer.hitPoint;
        str = dataPlayer.str;
        dex = dataPlayer.dex;
        con = dataPlayer.con;
        xp = dataPlayer.xp;
        levelPlayer = dataPlayer.level;
        bonus = dataPlayer.bonus;
        //itemUse = null;
        //diceHave = null;
    }
}
