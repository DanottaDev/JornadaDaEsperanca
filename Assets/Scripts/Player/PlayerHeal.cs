using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlayer : MonoBehaviour
{
    public int healAmount = 1; // Quantidade de cura que o objeto proporciona

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.Heal(healAmount);
                Destroy(gameObject); // Destrói o objeto de cura após ser pego
            }
        }
    }
}
