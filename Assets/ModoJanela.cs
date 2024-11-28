using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModoJanela : MonoBehaviour
{
    private bool isFullscreen = true;

    public void ToggleScreenMode()
    {
        isFullscreen = !isFullscreen;
        Screen.fullScreen = isFullscreen;
    }
}