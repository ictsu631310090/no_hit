using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveManagerScript : MonoBehaviour
{
    [SerializeField] private DataPlayerScript dataPlayer;
    [SerializeField] private UIScript ui;
    [SerializeField] private CreateArmorScript[] dataArmor;
    [SerializeField] private CreateShieldScript[] dataShield;
    [SerializeField] private CreateWeaponScript[] dataWeapon;

    //just use in this script.
    [HideInInspector] public List<CreateArmorScript> armorList;
    [HideInInspector] public List<CreateShieldScript> shieldList;
    [HideInInspector] public List<CreateWeaponScript> weaponList;
    public void NewGameButtom()
    {
        Debug.Log("New Game");
        dataPlayer.str = 10;
        dataPlayer.dex = 10;
        dataPlayer.con = 10;
        dataPlayer.xp = 0;
        dataPlayer.level = 1;
        dataPlayer.pointLevel = 0;
        dataPlayer.armorUse = null;
        dataPlayer.listArmor.Clear();
        dataPlayer.listShield.Clear();
        dataPlayer.listWeapon.Clear();
        for (int i = 0; i < dataPlayer.diceHave.Length; i++)
        {
            dataPlayer.diceHave[i] = 0;
        }
        dataPlayer.haveDamage = 0;
        ui.moneyPlayer = 0;//UI
        dataPlayer.room = 0;
        SaveGame();
    }
    public void SaveGame()
    {
        CreateSaveScript.Save(dataPlayer, ui);
        Debug.Log("Game Save");
    }
    public void LoadGameButtom()
    {
        GameSaveScript dataSave = CreateSaveScript.LoadGame();
        if (dataSave != null)
        {
            dataPlayer.str = dataSave.str;
            dataPlayer.dex = dataSave.dex;
            dataPlayer.con = dataSave.con;
            dataPlayer.xp = dataSave.xp;
            dataPlayer.level = dataSave.levelPlayer;
            dataPlayer.pointLevel = dataSave.pointLevel;
            dataPlayer.room = dataSave.room;
            ChangeBackData(dataSave);
            dataPlayer.diceHave = dataSave.diceHave;
            dataPlayer.haveDamage = dataSave.haveDamage;

            ui.moneyPlayer = dataSave.moneyHave;//UI
        }
        UpdateImageItem();
        Debug.Log("Load Game");
    }
    public void ChangeBackData(GameSaveScript dataSave)
    {
        armorList.Clear();
        shieldList.Clear();
        weaponList.Clear();
        dataPlayer.armorUse = findArmor(dataSave.armorUseID);
        for (int i = 0; i < 2; i++)
        {
            dataPlayer.rlHandWeapon[i] = findWeapon(dataSave.weaponInhandID[i]);
            dataPlayer.rlHandShield[i] = findShield(dataSave.shieldInhandID[i]);
        }
        foreach (var item in dataSave.listArmorID)
        {
            if (findArmor(item) != null)
            {
                armorList.Add(findArmor(item));
            }
        }
        dataPlayer.listArmor = armorList;
        foreach (var item in dataSave.listShieldID)
        {
            if (findShield(item) != null)
            {
                shieldList.Add(findShield(item));
            }
        }
        dataPlayer.listShield = shieldList;
        foreach (var item in dataSave.listWeaponID)
        {
            if (findWeapon(item) != null)
            {
                weaponList.Add(findWeapon(item));
            }
        }
        dataPlayer.listWeapon = weaponList;
    }
    private void UpdateImageItem()
    {
        dataPlayer.UpdateImageArmor();
        if (dataPlayer.showPlayer != null)
        {
            dataPlayer.showPlayer.UpdateACText();
        }

        for (int i = 0; i < 2; i++)
        {
            if (dataPlayer.rlHandWeapon[i] != null)
            {
                if (dataPlayer.rlHandWeapon[i].twoHand)
                {
                    dataPlayer.weaponTwoHand = true;
                    dataPlayer.UpdateImageWeapon(i, true);
                    break;
                }
                else
                {
                    dataPlayer.UpdateImageWeapon(i, true);
                }
            }
            else if (dataPlayer.rlHandShield[i] != null)
            {
                dataPlayer.UpdateImageWeapon(i, false);
            }
        }
    }
    private CreateArmorScript findArmor(int id)
    {
        CreateArmorScript data = null;
        foreach (CreateArmorScript item in dataArmor)
        {
            if (id == item.id)
            {
                data = item;
                break;
            }
            else if (id == 0)
            {
                data = null;
            }
        }
        return data;
    }
    private CreateShieldScript findShield(int id)
    {
        CreateShieldScript data = null;
        foreach (CreateShieldScript item in dataShield)
        {
            if (id == item.id)
            {
                data = item;
                break;
            }
            else if(id == 0)
            {
                data = null;
            }
        }
        return data;
    }
    private CreateWeaponScript findWeapon(int id)
    {
        CreateWeaponScript data = null;
        foreach (CreateWeaponScript item in dataWeapon)
        {
            if (id == item.id)
            {
                data = item;
                break;
            }
            else if (id == 0)
            {
                data = null;
            }
        }
        return data;
    }
}
