using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine;

public class AirZone : MonoBehaviour
{
    public float damagePerSecond = 10f; // Dano causado por segundo
    public float zoneDuration = 5f; // Duração da AirZone em segundos
    private bool playerInZone = false;
    private PlayerController playerController;

    void Start()
    {
        // Destruir o objeto da AirZone após a duração especificada
        Destroy(gameObject, zoneDuration);
    }

    void Update()
    {
        if (playerInZone)
        {
            // Aplica dano ao jogador baseado no tempo dentro da zona
            playerController.TakeDamage(Mathf.RoundToInt(damagePerSecond * Time.deltaTime));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            playerController = other.GetComponent<PlayerController>();
            Debug.Log("Player entrou na zona de ar rarefeito!");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            Debug.Log("Player saiu da zona de ar rarefeito!");
        }
    }
}
