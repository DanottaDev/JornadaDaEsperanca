using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Checkpoint : MonoBehaviour
{
    public Light2D checkpointLight; // Referência à luz do checkpoint
    private static Checkpoint lastCheckpoint; // Variável estática para o último checkpoint

    private void Start()
    {
        if (checkpointLight != null)
        {
            checkpointLight.enabled = false; // Desativa a luz inicialmente
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica se o jogador entrou no trigger
        {
            if (lastCheckpoint != null && lastCheckpoint != this)
            {
                lastCheckpoint.DeactivateCheckpoint(); // Desativa a luz do último checkpoint
            }

            ActivateCheckpoint(); // Ativa a luz do checkpoint atual
            lastCheckpoint = this; // Atualiza a referência do último checkpoint
        }
    }

    private void ActivateCheckpoint()
    {
        if (checkpointLight != null)
        {
            checkpointLight.enabled = true; // Ativa a luz
        }
    }

    private void DeactivateCheckpoint()
    {
        if (checkpointLight != null)
        {
            checkpointLight.enabled = false; // Desativa a luz
        }
    }
}
