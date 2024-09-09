using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemy : MonoBehaviour
{
    public float speed = 2f; // Velocidade de movimento do morcego
    public float attackRange = 1.5f; // Distância em que o morcego inicia o ataque
    public float detectionRange = 10f; // Raio de detecção do jogador
    public float health = 3; // Vida do morcego
    public float damage = 1; // Dano que o morcego causa
    public float attackCooldown = 1f; // Tempo entre ataques

    private Transform player; // Referência ao jogador
    private Animator animator; // Referência ao componente Animator
    private Rigidbody2D rb; // Referência ao Rigidbody2D
    private bool isAttacking = false; // Controle de estado de ataque
    private bool isDead = false; // Controle de estado de morte
    private float lastAttackTime = 0f; // Controle de cooldown entre ataques

    public Collider2D attackCollider; // Collider do ataque

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

        if (distanceToPlayer < detectionRange && !isAttacking)
        {
            FollowPlayer(); // Seguir o jogador se não estiver atacando
            
            if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
            {
                StartAttack();
            }
        }
    }

    void FollowPlayer()
    {
        if (isAttacking) return; // Se estiver atacando, não seguir o jogador

        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * speed, direction.y * speed);

        // Inverter sprite se o player estiver à esquerda/direita
        if (direction.x > 0)
            transform.localScale = new Vector3(3, 3, 1); // Para a direita
        else
            transform.localScale = new Vector3(-3, 3, 1); // Para a esquerda
    }

    void StartAttack()
    {
        isAttacking = true;
        rb.velocity = Vector2.zero; // Parar de se mover
        animator.SetTrigger("Attack"); // Iniciar animação de ataque
        attackCollider.enabled = true; // Ativar o collider de ataque
    }

    // Chamado no final da animação de ataque (isso deve ser configurado no Animator)
    public void EndAttack()
    {
        Debug.Log("Ataque terminou!");
        isAttacking = false;
        lastAttackTime = Time.time;
        attackCollider.enabled = false;
        FollowPlayer();
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
