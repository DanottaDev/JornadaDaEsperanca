using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapActivator : MonoBehaviour
{
    public Rigidbody2D trapRigidbody;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            trapRigidbody.gravityScale = 1f;  // Ativa a gravidade
            PlayerController playerController = collision.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage(1); // Aplica dano ao jogador (ajuste conforme necessário)
            }
        }
    }
}
