using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapScript : MonoBehaviour
{
    [SerializeField] private Button[] mapButton;
    [SerializeField] private DataPlayerScript dataplayer;
    public void FightScene1()
    {
        SceneManager.LoadScene(3);
    }
    public void FightScene2()
    {
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
