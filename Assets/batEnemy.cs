using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemy : MonoBehaviour
{
    // Referências
    public Animator animator;
    public float detectionRange = 5f; // Distância para acordar
    public float attackRange = 2f;    // Distância para atacar
    public float speed = 2f;          // Velocidade de voo
    public Transform player;

    // Estados do morcego
    private bool isSleeping = true;
    private bool isFlying = false;
    private bool isAttacking = false;

    void Start()
    {
        // Certificar-se que começa com a animação de sleep
        animator.Play("Sleep");
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Se o morcego está dormindo e o player está perto, acordar
        if (isSleeping && distanceToPlayer < detectionRange)
        {
            WakeUp();
        }

        // Se acordou e está voando, perseguir o player
        if (isFlying && distanceToPlayer > attackRange)
        {
            FlyTowardsPlayer();
        }

        // Se o player estiver dentro do alcance de ataque, atacar
        if (isFlying && distanceToPlayer <= attackRange && !isAttacking)
        {
            Attack();
        }

        // Volta para o estado idle se estiver voando e fora de alcance de ataque
        if (isFlying && distanceToPlayer > attackRange && isAttacking)
        {
            isAttacking = false;
            animator.Play("IdleFlying");
        }
    }

    void WakeUp()
    {
        isSleeping = false;
        animator.Play("WakeUp");
        Invoke("StartFlying", 1f); // Tempo para a animação de acordar antes de voar
    }

    void StartFlying()
    {
        isFlying = true;
        animator.Play("Run"); // A animação de voar
    }

    void FlyTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    void Attack()
    {
        isAttacking = true;
        animator.Play("Attack");
    }

    // Função para desenhar a área de detecção e ataque no editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange); // Área de detecção
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);    // Área de ataque
    }
}
