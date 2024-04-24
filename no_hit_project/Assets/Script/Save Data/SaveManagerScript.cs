using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveManagerScript : MonoBehaviour
{
    [SerializeField] private DataPlayerScript dataPlayer;
    [SerializeField] private Button loadGameButtom;


    public void NewGameButtom()
    {
        
    }
    public void LoadGameButtom()
    {
        GameSaveScript data = CreateSaveScript.LoadGame();
        dataPlayer.hitPoint = data.hitPoint;
        dataPlayer.str = data.str;
        dataPlayer.dex = data.dex;
        dataPlayer.con = data.con;
        dataPlayer.xp = data.xp;
        dataPlayer.level = data.levelPlayer;
        dataPlayer.bonus = data.bonus;
    }

    public void SaveGame()
    {
        CreateSaveScript.Save(dataPlayer);
    }
}
