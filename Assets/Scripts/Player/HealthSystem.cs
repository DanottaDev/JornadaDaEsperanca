using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartSystem : MonoBehaviour
{
    public int vida;
    public int vidaMaxima;
    public Image[] coracao;
    public Sprite cheio;
    public Sprite vazio;
    
    public Transform player; // Referência ao transform do player
    public Vector3 startPosition; // Posição inicial da fase

    void Start()
    {
        // Inicializa a posição inicial do player
        startPosition = player.position;
    }

    void Update()
    {
        HealthLogic();

        // Verifica se as vidas do player chegaram a 0
        if (vida <= 0)
        {
            ResetPlayerPosition();
        }
    }

    void HealthLogic()
    {
        for (int i = 0; i < coracao.Length; i++)
        {
            if (i < vida)
            {
                coracao[i].sprite = cheio;
            }
            else
            {
                coracao[i].sprite = vazio;
            }

            if (i < vidaMaxima)
            {
                coracao[i].enabled = true;
            }
            else
            {
                coracao[i].enabled = false;
            }
        }
    }

    void ResetPlayerPosition()
    {
        // Reseta a posição do player para a posição inicial da fase
        player.position = startPosition;

        // Opcional: Reseta as vidas do player para o valor máximo
        vida = vidaMaxima;

        // Atualiza a UI
        HealthLogic();
    }
}
