using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveManagerScript : MonoBehaviour
{
    [SerializeField] private DataPlayerScript dataPlayer;
    [HideInInspector] public UIScript ui;
    [SerializeField] private CreateArmorScript[] dataArmor;
    [SerializeField] private CreateShieldScript[] dataShield;
    [SerializeField] private CreateWeaponScript[] dataWeapon;

    //just use in this script.
    [HideInInspector] public List<CreateArmorScript> armorList;
    [HideInInspector] public List<CreateShieldScript> shieldList;
    [HideInInspector] public List<CreateWeaponScript> weaponList;
    public void NewGameButtom()
    {
        
    }
    public void SaveGame()
    {
        CreateSaveScript.Save(dataPlayer, ui);
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
            ChangeBackData(dataSave);
            dataPlayer.diceHave = dataSave.diceHave;
            dataPlayer.hitPoint = dataSave.hitPoint;
            dataPlayer.hitPointMax = dataSave.hitPointMax;

            ui.moneyPlayer = dataSave.moneyHave;//UI
        }
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
            armorList.Add(findArmor(item));
        }
        dataPlayer.listArmor = armorList;
        foreach (var item in dataSave.listShieldID)
        {
            shieldList.Add(findShield(item));
        }
        dataPlayer.listShield = shieldList;
        foreach (var item in dataSave.listWeaponID)
        {
            weaponList.Add(findWeapon(item));
        }
        dataPlayer.listWeapon = weaponList;
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
                break;
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
                break;
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
                break;
            }
        }
        return data;
    }
    private void Awake()
    {
        ui = GetComponent<UIScript>();
    }
}
