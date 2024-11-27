using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // Método para fechar o jogo
    public void ExitGame()
    {
        // Exibe mensagem no console (útil para testes no editor do Unity)
        Debug.Log("Fechando o jogo...");

        // Fecha o aplicativo quando executado fora do editor
        Application.Quit();
    }
}
