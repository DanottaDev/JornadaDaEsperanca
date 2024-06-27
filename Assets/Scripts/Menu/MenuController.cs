using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    public string cena;

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void QuitGame()
    {
        // Editor Unity
        UnityEditor.EditorApplication.isPlaying = false;
        // Para o jogo compilado
        // Application.Quit();
    }
    
}
