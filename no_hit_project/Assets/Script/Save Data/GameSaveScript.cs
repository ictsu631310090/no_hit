using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSaveScript
{
    public int hitPoint;
    public int hitPointMax;
    public int str;//0
    public int dex;//1
    public int con;//2
    public int xp;
    public int levelPlayer;
    public int bonus;
    //public List<int> itemUse;
    //public List<GameObject> diceHave;

    public GameSaveScript (ShowPlayerScript player, UpLevelPlayerScript level)
    {
        hitPoint = player.hitPoint;
        hitPointMax = player.hitPointMax;
        str = level.str;
        dex = level.dex;
        con = level.con;
        xp = level.xp;
        levelPlayer = level.level;
        bonus = level.bonus;
        //itemUse = null;
        //diceHave = null;
    }
}
