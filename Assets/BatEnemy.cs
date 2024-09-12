using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemy : MonoBehaviour
{
    public float attackRange = 1.5f; // Distância em que o morcego inicia o ataque
    public float detectionRange = 10f; // Raio de detecção do jogador
    public float health = 3; // Vida do morcego
    public float damage = 1; // Dano que o morcego causa
    public float attackCooldown = 1f; // Tempo entre ataques
    public float moveSpeed = 2f;
    public float attackMoveDistance = 0.5f;

    private Transform player; // Referência ao jogador
    private Animator animator; // Referência ao componente Animator
    private Rigidbody2D rb; // Referência ao Rigidbody2D
    public Collider2D attackCollider; // Collider do ataque

    private bool isDead = false; // Controle de estado de morte
    private float nextAttackTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Encontrando o jogador pela tag
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        attackCollider.enabled = false; // O collider de ataque começa desativado
    }

    void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > attackRange)
        {
            FollowPlayer();
        }
        else if (Time.time >= nextAttackTime)
        {
            Attack();
        }
    }

    void FollowPlayer()
    {
        animator.SetBool("isAttacking", false);  // Para garantir que não está atacando enquanto se move
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        transform.localScale = new Vector2(player.position.x < transform.position.x ? 3 : -3, 3);  // Flip para virar na direção do player

    }

    void Attack()
    {
        animator.SetTrigger("Attack"); // Iniciar animação de ataque
        nextAttackTime = Time.time + attackCooldown;
        attackCollider.enabled = true;
         // Dá um pequeno impulso para frente
        Vector2 attackDirection = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, transform.position + (Vector3)attackDirection, attackMoveDistance);
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        rb.velocity = Vector2.zero; // Parar de se mover
        animator.SetTrigger("Die"); // Iniciar animação de morte
        attackCollider.enabled = false; // Certificar que o collider de ataque esteja desativado
        Destroy(gameObject, 1f); // Destruir o inimigo após 1 segundo
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Converta o dano de float para int antes de passar ao PlayerController
            other.GetComponent<PlayerController>().TakeDamage(Mathf.RoundToInt(damage));
        }
    }
}
