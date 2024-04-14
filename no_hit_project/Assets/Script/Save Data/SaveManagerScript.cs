using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class SaveManagerScript : MonoBehaviour
{
    [SerializeField] private Button loadGameButtom;

    [SerializeField] private GameObject player;
    private ShowPlayerScript playerData;
    private UpLevelPlayerScript level;

    public void NewGameButtom()
    {
        
    }
    public void LoadGameButtom()
    {
        GameSaveScript data = CreateSaveScript.LoadGame();
        playerData.hitPoint = data.hitPoint;
        playerData.hitPointMax = data.hitPointMax;
        level.str = data.str;
        level.dex = data.dex;
        level.con = data.con;
        level.xp = data.xp;
        level.level = data.levelPlayer;
        level.bonus = data.bonus;
    }

    public void SaveGame()
    {
        CreateSaveScript.Save(playerData, level);
    }

    private void Start()
    {
        playerData = player.GetComponent<ShowPlayerScript>();
        level = player.GetComponent<UpLevelPlayerScript>();

        //if (gameSave == null)
        //{
        //    loadGameButtom.interactable = false;
        //}
    }
}
