using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1;         // Dano que o projétil causa
    public float lifetime = 3f;     // Tempo de vida do projétil antes de ser destruído

    private void Start()
    {
        Destroy(gameObject, lifetime);  // Destruir o projétil após um tempo
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Aplicar dano ao jogador
            other.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(gameObject);  // Destruir o projétil após a colisão
        }
    }
}