using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSaveScript
{
    public int str;//0
    public int dex;//1
    public int con;//2
    public int xp;
    public int levelPlayer;
    public int pointLevel;

    public int armorUseID;
    public int[] weaponInhandID = { 0, 0 };
    public int[] shieldInhandID = { 0, 0 };
    public int[] listArmorID;
    public int[] listShieldID;
    public int[] listWeaponID;

    public int[] diceHave = { 0, 0, 0, 0, 0 };//4, 6, 8, 10, 12
    public int hitPoint;
    public int hitPointMax;

    public int moneyHave;//UI
    public GameSaveScript (DataPlayerScript dataPlayer , UIScript money)
    {
        str = dataPlayer.str;
        dex = dataPlayer.dex;
        con = dataPlayer.con;
        xp = dataPlayer.xp;
        levelPlayer = dataPlayer.level;
        pointLevel = dataPlayer.pointLevel;
        ChangeDataItem(dataPlayer);

        diceHave = dataPlayer.diceHave;
        hitPoint = dataPlayer.hitPoint;
        hitPointMax = dataPlayer.hitPointMax;

        moneyHave = money.moneyPlayer;//UI
    }
    public void ChangeDataItem(DataPlayerScript dataPlayer)
    {
        if (dataPlayer.armorUse != null)
        {
            armorUseID = dataPlayer.armorUse.id;
        }
        else
        {
            armorUseID = 0;
        }
        for (int i = 0; i < 2; i++)
        {
            if (dataPlayer.rlHandWeapon[i] != null)
            {
                weaponInhandID[i] = dataPlayer.rlHandWeapon[i].id;
            }
            else
            {
                weaponInhandID[i] = 0;
            }

            if (dataPlayer.rlHandShield[i] != null)
            {
                shieldInhandID[i] = dataPlayer.rlHandShield[i].id; 
            }
            else
            {
                shieldInhandID[i] = 0;
            }
        }
        ClearListItem(dataPlayer);
        for (int i = 0; i < dataPlayer.listArmor.Count; i++)
        {
            listArmorID[i] = dataPlayer.listArmor[i].id;
        }
        for (int i = 0; i < dataPlayer.listShield.Count; i++)
        {
            listShieldID[i] = dataPlayer.listShield[i].id;
        }
        for (int i = 0; i < dataPlayer.listWeapon.Count; i++)
        {
            listWeaponID[i] = dataPlayer.listWeapon[0].id;
        }
    }
    public void ClearListItem(DataPlayerScript dataPlayer)
    {
        listArmorID = new int[dataPlayer.listArmor.Count];
        for (int i = 0; i < dataPlayer.listArmor.Count; i++)
        {
            listArmorID[i] = 0;
        }
        listShieldID = new int[dataPlayer.listShield.Count];
        for (int i = 0; i < dataPlayer.listShield.Count; i++)
        {
            listShieldID[i] = 0;
        }
        listWeaponID = new int[dataPlayer.listWeapon.Count];
        for (int i = 0; i < dataPlayer.listWeapon.Count; i++)
        {
            listWeaponID[i] = 0;
        }
    }
    private void Start()
    {
        
    }
}
