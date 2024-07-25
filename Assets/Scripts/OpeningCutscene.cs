using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class OpeningCutscene : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("Level01", LoadSceneMode.Single);
    }

}
