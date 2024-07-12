using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompAttack : MonoBehaviour
{
    public float stompForce = 10f; // A força do stomp attack
    public LayerMask enemyLayer; // A camada dos inimigos

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Verifica se o jogador está caindo
        if (rb.velocity.y < 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, enemyLayer);
            if (hit.collider != null)
            {
                // Adiciona força para o stomp attack
                rb.velocity = new Vector2(rb.velocity.x, stompForce);

                // Aqui você pode adicionar dano ao inimigo atingido
                hit.collider.GetComponent<EnemyController>().TakeDamage(1, true);

                // Opcional: aplicar knockback ao jogador
                ApplyKnockback();
            }
        }
    }

    void ApplyKnockback()
    {
        // Aplica um pequeno knockback ao jogador para cima
        rb.AddForce(new Vector2(0, stompForce / 2), ForceMode2D.Impulse);
    }
}
