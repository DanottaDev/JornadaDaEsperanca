using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplePressLever : MonoBehaviour
{
    public int requiredPresses = 5; // Quantidade de pressões necessárias para ativar a alavanca
    public float timeWindow = 1.5f; // Tempo máximo entre as pressões
    private int pressCount = 0;
    private float lastPressTime = 0f;
    private bool leverPulled = false;

    public MovePlatformY platformScript; // Referência ao script da plataforma
    public Animator leverAnimator; // Referência ao Animator da alavanca
    private bool playerInRange = false; // Verifica se o jogador está próximo da alavanca

    void Update()
    {
        // Verifica se a alavanca já foi puxada ou se o jogador não está perto
        if (leverPulled || !playerInRange)
            return;

        // Detecta se o jogador apertou o botão de interação (troque "Fire1" pelo seu botão de interação)
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Se for a primeira pressão ou se a última pressão foi dentro do tempo limite
            if (Time.time - lastPressTime <= timeWindow || pressCount == 0)
            {
                pressCount++;
                lastPressTime = Time.time;

                // Se o jogador apertou o botão o número necessário de vezes, ativa a alavanca
                if (pressCount >= requiredPresses)
                {
                    PullLever();
                }
            }
            else
            {
                // Reseta o contador se o tempo entre as pressões for muito longo
                pressCount = 1;
                lastPressTime = Time.time;
            }
        }
    }

    // Método para puxar a alavanca
    void PullLever()
    {
        leverPulled = true;
        // Ativa a plataforma
        platformScript.isActivated = true;
        // Toca a animação da alavanca
        leverAnimator.SetTrigger("Pull");
        Debug.Log("Alavanca Puxada! Plataforma ativada.");
    }

    // Detecta quando o jogador entra no alcance da alavanca
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Jogador está próximo da alavanca.");
        }
    }

    // Detecta quando o jogador sai do alcance da alavanca
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Jogador se afastou da alavanca.");
        }
    }
}
