using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningCutscene : MonoBehaviour
{
    void OnEnable()
    {
        // Carrega a próxima cena no build index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1, LoadSceneMode.Single);
    }
}