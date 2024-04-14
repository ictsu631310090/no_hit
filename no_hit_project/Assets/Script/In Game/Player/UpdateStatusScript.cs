using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateStatusScript : MonoBehaviour
{
    public int status;
    [HideInInspector] public int point;
    public void OnMouseDown()
    {
        point = 1;
    }
}
