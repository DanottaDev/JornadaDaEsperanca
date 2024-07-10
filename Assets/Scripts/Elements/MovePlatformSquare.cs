using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformSquare : MonoBehaviour
{
    public float speed = 2.0f; // Velocidade da plataforma
    public Transform[] points; // Array de pontos para definir os cantos do quadrado

    private int targetIndex = 0; // Índice do ponto alvo atual
    private Vector3 targetPos; // Posição alvo para a plataforma
    private GameObject player; // Referência ao jogador

    void Start()
    {
        if (points.Length > 0)
        {
            targetPos = points[targetIndex].position; // Inicializa a posição alvo
        }
    }

    void Update()
    {
        // Move a plataforma em direção à posição alvo
        Vector3 previousPosition = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        Vector3 movement = transform.position - previousPosition;

        // Se o jogador está na plataforma, move o jogador junto com a plataforma
        if (player != null)
        {
            player.transform.position += movement;
        }

        // Se a plataforma está perto do ponto alvo, muda para o próximo ponto
        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            targetIndex = (targetIndex + 1) % points.Length; // Incrementa o índice do ponto alvo e volta ao início se necessário
            targetPos = points[targetIndex].position; // Atualiza a posição alvo
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Se o objeto colidido tem a tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject; // Armazena a referência ao jogador
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Se o objeto colidido tem a tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            player = null; // Remove a referência ao jogador
        }
    }
}
