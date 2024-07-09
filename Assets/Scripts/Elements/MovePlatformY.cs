using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformY : MonoBehaviour
{
    public float speed; // Velocidade da plataforma
    public Transform pontoA, pontoB; // Pontos entre os quais a plataforma se moverá

    private Vector3 targetPos; // Posição alvo para a plataforma
    private GameObject player; // Referência ao jogador

    void Start()
    {
        targetPos = pontoB.position; // Inicialmente, a plataforma se moverá para o pontoB
    }

    void Update()
    {
        // Se a plataforma está perto do pontoA, muda a posição alvo para pontoB
        if (Vector3.Distance(transform.position, pontoA.position) < 0.1f)
            targetPos = pontoB.position;

        // Se a plataforma está perto do pontoB, muda a posição alvo para pontoA
        if (Vector3.Distance(transform.position, pontoB.position) < 0.1f)
            targetPos = pontoA.position;

        // Move a plataforma em direção à posição alvo
        Vector3 previousPosition = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        Vector3 movement = transform.position - previousPosition;

        // Se o jogador está na plataforma, move o jogador junto com a plataforma
        if (player != null)
        {
            player.transform.position += movement;
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
