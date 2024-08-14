using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public GameObject barrier; // Referência para a barreira
    private bool hasInteracted = false;
    private bool isPlayerInRange = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        if (isPlayerInRange && !hasInteracted && Input.GetKeyDown(KeyCode.E))
        {
            PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            if (player != null)
            {
                UnlockAttack(player); // Desbloqueia o ataque
                OpenBarrier(); // Abre a barreira
            }
        }
    }

    private void UnlockAttack(PlayerController player)
    {
        player.canAttack = true; // Desbloqueia o ataque
        hasInteracted = true; // Garante que essa interação só ocorra uma vez
        Debug.Log("Ataque desbloqueado!");
    }

    private void OpenBarrier()
    {
        if (barrier != null)
        {
            barrier.SetActive(false); // Desativa a barreira
            Debug.Log("Barreira aberta!");
        }
    }
}

