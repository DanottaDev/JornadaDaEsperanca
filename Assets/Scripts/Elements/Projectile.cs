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

            // Destruir o projétil após a animação
            Destroy(gameObject, 0.1f);  // O tempo de 0.3f deve ser ajustado para a duração da animação
        }
    }
}