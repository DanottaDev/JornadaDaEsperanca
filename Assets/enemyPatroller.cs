using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroller : MonoBehaviour
{
    public float detectionRange = 5f; // Distância de detecção do player
    public float attackRange = 2f; // Distância para iniciar o ataque
    public float patrolSpeed = 2f; // Velocidade de patrulha
    public float chaseSpeed = 4f; // Velocidade de perseguição
    public Transform[] patrolPoints; // Pontos de patrulha
    public Animator animator; // Referência ao Animator
    public PlayerController player; // Referência ao PlayerController
    public float attackCooldown = 1f; // Tempo de espera entre os ataques

    private int currentPatrolIndex = 0;
    private bool isChasing = false;
    private bool canAttack = true; // Controla se o inimigo pode atacar
    
    void Start()
    {
        // Início da patrulha, sem necessidade de chamar GotoNextPatrolPoint()
    }

    void Update()
    {
        // Calcula a distância até o player usando o Transform do PlayerController
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= attackRange)
        {
            AttackPlayer();
        }
        else if (distanceToPlayer <= detectionRange)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        isChasing = false;
        animator.SetBool("isChasing", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isWalking", true); // Ativa a animação de patrulha

        // Movimenta o inimigo até o próximo ponto de patrulha
        Transform targetPatrolPoint = patrolPoints[currentPatrolIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPatrolPoint.position, patrolSpeed * Time.deltaTime);

        // Verifica se chegou ao ponto de patrulha
        if (Vector3.Distance(transform.position, targetPatrolPoint.position) < 0.2f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            animator.SetBool("isWalking", false); // Para a animação de patrulha
        }
    }

    void ChasePlayer()
    {
        isChasing = true;
        animator.SetBool("isChasing", true);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isWalking", true); // Ativa a animação de patrulha
        // Movimenta o inimigo em direção ao player
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, chaseSpeed * Time.deltaTime);
    }

    void AttackPlayer()
{
    if (!canAttack) return; // Se não pode atacar, sai do método

    isChasing = false;
    animator.SetBool("isChasing", false);
    animator.SetBool("isAttacking", true);
    animator.SetBool("isWalking", false); // Para a animação de patrulha

    // Aqui o inimigo ataca e causa dano ao player
    player.TakeDamage(1); // Causa 10 de dano ao player, por exemplo

    canAttack = false; // Bloqueia novos ataques
    StartCoroutine(ResetAttackCooldown()); // Reinicia o cooldown do ataque
}

private IEnumerator ResetAttackCooldown()
{
    yield return new WaitForSeconds(attackCooldown); // Aguarda o tempo do cooldown
    canAttack = true; // Permite um novo ataque
}

    void OnDrawGizmosSelected()
    {
        // Desenhar as áreas de detecção e ataque no editor para ajustes
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
