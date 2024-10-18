using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroller : MonoBehaviour
{
    public float detectionRange = 5f; // Distância de detecção do player
    public float attackRange = 2f; // Distância para iniciar o ataque
    public float patrolSpeed = 2f; // Velocidade de patrulha
    public Transform[] patrolPoints; // Pontos de patrulha
    public Animator animator; // Referência ao Animator
    public PlayerController player; // Referência ao PlayerController
    public float attackCooldown = 1f; // Tempo de espera entre os ataques

    private int currentPatrolIndex = 0;
    private bool isChasing = false;
    private bool canAttack = true; // Controla se o inimigo pode atacar
    private bool facingRight = true;
    
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
        
        // Verifica a direção para flipar
        Vector3 direction = targetPatrolPoint.position - transform.position;
        Flip(direction.x);

        transform.position = Vector3.MoveTowards(transform.position, targetPatrolPoint.position, patrolSpeed * Time.deltaTime);

        // Verifica se chegou ao ponto de patrulha
        if (Vector3.Distance(transform.position, targetPatrolPoint.position) < 0.2f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            animator.SetBool("isWalking", false); // Para a animação de patrulha
        }
    }

    void AttackPlayer()
{
    if (!canAttack) return; // Se não pode atacar, sai do método

    isChasing = false;
    animator.SetBool("isChasing", false);
    animator.SetBool("isAttacking", true);
    animator.SetBool("isWalking", false); // Para a animação de patrulha

    // Verifica se é necessário flipar
    Vector3 directionToPlayer = player.transform.position - transform.position;
    Flip(directionToPlayer.x);

    // Aqui o inimigo ataca e causa dano ao player
    player.TakeDamage(1); // Causa 1 de dano ao player, por exemplo

    canAttack = false; // Bloqueia novos ataques
    StartCoroutine(ResetAttackCooldown()); // Reinicia o cooldown do ataque
}

// Função para flipar o inimigo
    void Flip(float moveDirection)
    {
        if ((moveDirection > 0 && !facingRight) || (moveDirection < 0 && facingRight))
        {
            facingRight = !facingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1; // Inverte o eixo X
            transform.localScale = localScale;
        }
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
