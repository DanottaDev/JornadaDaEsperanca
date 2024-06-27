using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform targetPoint; // Ponto para onde a plataforma vai se mover
    public float moveSpeed = 5f; // Velocidade de movimento da plataforma
    public float returnDelay = 3f; // Tempo de espera antes da plataforma retornar ao ponto inicial

    private bool moving; // Flag para controlar o movimento da plataforma
    private Vector3 initialPosition; // Posição inicial da plataforma
    private float timer; // Contador de tempo

    private void Start()
    {
        initialPosition = transform.position; // Salvando a posição inicial da plataforma
    }

    private void Update()
    {
        if (moving)
        {
            // Movendo a plataforma em direção ao ponto alvo
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

            // Verificando se a plataforma chegou ao ponto alvo
            if (transform.position == targetPoint.position)
            {
                moving = false; // Parando o movimento
                timer = 0f; // Reiniciando o contador de tempo
            }
        }
        else
        {
            // Contando o tempo para o retorno da plataforma
            timer += Time.deltaTime;
            if (timer >= returnDelay)
            {
                // Movendo a plataforma de volta para a posição inicial
                transform.position = Vector3.MoveTowards(transform.position, initialPosition, moveSpeed * Time.deltaTime);

                // Verificando se a plataforma chegou à posição inicial
                if (transform.position == initialPosition)
                {
                    // Reiniciando as variáveis
                    timer = 0f;
                    moving = false;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !moving)
        {
            moving = true; // Iniciando o movimento quando o jogador pisa na plataforma
        }
    }
}