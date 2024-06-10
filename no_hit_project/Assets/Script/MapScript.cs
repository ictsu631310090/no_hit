using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapScript : MonoBehaviour
{
    [SerializeField] private DataPlayerScript dataplayer;
    [SerializeField] private Button[] mapButton;

    public DataMap[] allDataMaps;
    public static DataMap useNow;
    public void FightScene1()
    {
        useNow = allDataMaps[0];
        //useNow.numOfMon = allDataMaps[0].numOfMon;
        //useNow.dataMon = allDataMaps[0].dataMon;

        SceneManager.LoadScene(3);
    }
    public void FightScene2()
    {
        useNow.numOfMon = allDataMaps[1].numOfMon;
        useNow.dataMon = allDataMaps[1].dataMon;

        SceneManager.LoadScene(3);
    }
    public void FightScene3()
    {
        useNow.numOfMon = allDataMaps[2].numOfMon;
        useNow.dataMon = allDataMaps[2].dataMon;

        SceneManager.LoadScene(3);
    }
    public void GoShopScene()
    {
        SceneManager.LoadScene(4);
    }
    public void GoRestScene()
    {
        SceneManager.LoadScene(5);
    }
    IEnumerator nextRoom()
    {
        yield return new WaitForSeconds(0.1f);
        if (dataplayer.room >= mapButton.Length)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            for (int i = 0; i < mapButton.Length; i++)
            {
                if (i == dataplayer.room)
                {
                    mapButton[i].interactable = true;
                }
                else
                {
                    mapButton[i].interactable = false;
                }
            }

        }
    }
    private void Start()
    {
        Debug.Log("room :" + dataplayer.room);
        StartCoroutine(nextRoom());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            dataplayer.room = mapButton.Length - 1;
            Debug.Log("dataPlayer.room : " + dataplayer.room);
            Debug.Log("mapButton.Length : " + mapButton.Length);
            StartCoroutine(nextRoom());
        }
    }
}
