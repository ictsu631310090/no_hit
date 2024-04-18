using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMapScript : MonoBehaviour
{
    private Button button;
    public void Buttom()
    {
        button.interactable = false;
    }
    private void Awake()
    {
        button = this.GetComponent<Button>();
    }
}
