using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpLevelPlayerScript : MonoBehaviour
{
    public int xp;
    public int addXp;
    public int level;

    public void LevelUp()
    {
        if (addXp != 0)
        {
            xp += addXp;
            addXp = 0;
            switch (xp)
            {
                case < 300 :
                    level = 1;
                    break;
                case < 900:
                    level = 2;
                    break;
                case < 2700:
                    level = 3;
                    break;
                case < 6500:
                    level = 4;
                    break;
                case < 14000:
                    level = 5;
                    break;
                case < 23000:
                    level = 6;
                    break;
                case < 34000:
                    level = 7;
                    break;
                case < 48000:
                    level = 8;
                    break;
                case < 64000:
                    level = 9;
                    break;
                case < 85000:
                    level = 10;
                    break;
                case < 100000:
                    level = 11;
                    break;
                case < 120000:
                    level = 12;
                    break;
                case < 140000:
                    level = 13;
                    break;
                case < 165000:
                    level = 14;
                    break;
                case < 195000:
                    level = 15;
                    break;
                case < 225000:
                    level = 16;
                    break;
                case < 265000:
                    level = 17;
                    break;
                case < 305000:
                    level = 18;
                    break;
                case < 355000:
                    level = 19;
                    break;
                case > 355000:
                    level = 20;
                    break;
                default:
                    Debug.LogError("Level Error");
                    break;
            }
        }
    }
    private void Start()
    {
        addXp = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        LevelUp();
    }
}
