using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPatrol : MonoBehaviour
{
    public float speed = .5f; // Velocidade de movimento do NPC
    public Transform leftBoundary; // Limite esquerdo do caminho
    public Transform rightBoundary; // Limite direito do caminho
    private bool movingRight = true; // Direção do NPC

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        // Movendo para a direita
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime); // Move para a direita

            // Se o NPC ultrapassar o limite direito
            if (transform.position.x >= rightBoundary.position.x)
            {
                Flip(); // Inverte a orientação do NPC
                movingRight = false; // Muda a direção para a esquerda
            }
        }
        // Movendo para a esquerda
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime); // Move para a esquerda

            // Se o NPC ultrapassar o limite esquerdo
            if (transform.position.x <= leftBoundary.position.x)
            {
                Flip(); // Inverte a orientação do NPC
                movingRight = true; // Muda a direção para a direita
            }
        }
    }

    void Flip()
    {
        // Inverte a escala do NPC no eixo X
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
