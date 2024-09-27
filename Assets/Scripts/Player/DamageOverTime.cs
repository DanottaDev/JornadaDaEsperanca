using System.Collections;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class DamageOverTime : MonoBehaviour
{
    public int damage = 1; // Dano a ser aplicado
    public float delayBeforeDamage = 3f; // Tempo de espera antes de começar a aplicar o dano
    public float damageInterval = 5f; // Intervalo entre cada aplicação de dano
    public float invulnerabilityDuration = 1.0f; // Tempo de invulnerabilidade em segundos

    private bool isPlayerInside = false;
    private bool isInvulnerable = false;
    private GameObject player;
    private Coroutine damageCoroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isPlayerInside)
        {
            player = collision.gameObject;
            isPlayerInside = true;
            // Inicia a coroutine para começar o dano após o delay
            damageCoroutine = StartCoroutine(DamagePlayerAfterDelay());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isPlayerInside)
        {
            isPlayerInside = false;
            // Para o dano quando o jogador sair do trigger
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
            }
        }
    }

    private IEnumerator DamagePlayerAfterDelay()
    {
        // Espera o tempo definido antes de começar a aplicar o dano
        yield return new WaitForSeconds(delayBeforeDamage);

        // Continua aplicando dano enquanto o player estiver dentro do trigger
        while (isPlayerInside)
        {
            if (!isInvulnerable)
            {
                ApplyDamage();
                StartCoroutine(InvulnerabilityCooldown());
            }
            yield return new WaitForSeconds(damageInterval); // Aplica dano de acordo com o intervalo definido
        }
    }

    private void ApplyDamage()
    {
        // Aplica dano ao player
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.TakeDamage(damage);
        }
    }

    private IEnumerator InvulnerabilityCooldown()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDuration);
        isInvulnerable = false;
    }
}