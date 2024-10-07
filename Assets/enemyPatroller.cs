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
    public Transform player; // Referência ao player

    private int currentPatrolIndex = 0;
    private bool isChasing = false;

    void Start()
    {
        // Início da patrulha, sem necessidade de chamar GotoNextPatrolPoint()
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

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
    transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
}

void AttackPlayer()
{
    isChasing = false;
    animator.SetBool("isChasing", false);
    animator.SetBool("isAttacking", true);
    animator.SetBool("isWalking", false); // Para a animação de patrulha
    // Aqui você pode adicionar a lógica de ataque, como causar dano ao player
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
