using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fullscreen : MonoBehaviour
{
    public void Change()
    {
        Screen.fullScreen = !Screen.fullScreen;
        print("Modo de tela alterado");
    }
}