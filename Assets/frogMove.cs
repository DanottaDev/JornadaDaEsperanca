using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frogMove : MonoBehaviour
{
    public Transform pointA; // Primeiro ponto de patrulha
    public Transform pointB; // Segundo ponto de patrulha
    public float speed = 2f; // Velocidade do NPC

    private Vector3 targetPosition;
    private bool movingToB = true; // Direção inicial

    void Start()
    {
        // Define o alvo inicial como o ponto A
        targetPosition = pointA.position;
    }

    void Update()
    {
        // Move o NPC em direção ao ponto alvo
        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Verifica se o NPC chegou ao ponto alvo
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Alterna entre os pontos A e B
            movingToB = !movingToB;
            targetPosition = movingToB ? pointB.position : pointA.position;
        }

        // Verifica a direção do movimento no eixo X para flipar
        if (moveDirection.x > 0)
        {
            // Se movendo para a direita, ajuste normal
            transform.localScale = new Vector3(2, 2, 1); // Normal
        }
        else if (moveDirection.x < 0)
        {
            // Se movendo para a esquerda, flip
            transform.localScale = new Vector3(-2, 2, 1); // Flipado
        }
    }
}

