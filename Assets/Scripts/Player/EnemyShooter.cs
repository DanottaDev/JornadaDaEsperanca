using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    public GameObject projectilePrefab;  // Prefab do projétil
    public Transform firePoint;          // Ponto de origem do projétil
    public float fireRate = 2f;          // Intervalo entre os disparos (em segundos)
    public float detectionRange = 10f;   // Alcance de detecção do jogador
    public float projectileSpeed = 5f;   // Velocidade do projétil

    private Transform player;            // Referência ao jogador
    private bool playerInRange = false;  // Se o jogador está no alcance
    private float fireCooldown = 0f;     // Controla o cooldown entre disparos
    private Animator animator;           // Referência ao Animator

    private void Start()
    {
        // Encontrar o jogador pela tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // Pegar o componente Animator
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Atualiza o cooldown
        fireCooldown -= Time.deltaTime;

        // Verificar a distância entre o inimigo e o jogador
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Se o jogador estiver no alcance, definir playerInRange como verdadeiro
        if (distanceToPlayer <= detectionRange)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }

        // Se o jogador está no alcance e o cooldown permite, atira
        if (playerInRange && fireCooldown <= 0f)
        {
            ShootAtPlayer();
            fireCooldown = 1f / fireRate;  // Reinicia o cooldown após disparar
        }
    }

    private void ShootAtPlayer()
    {
        // Ativar animação de disparo
        animator.SetTrigger("atirando");

        // Calcular a direção do projétil com base na posição do jogador
        Vector2 direction = (player.position - firePoint.position).normalized;

        // Instanciar o projétil
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        // Configurar a direção e velocidade do projétil
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction * projectileSpeed;

        // Girar o inimigo na direção do jogador
        if (player.position.x < transform.position.x)
        {
            // Jogador à esquerda
            transform.localScale = new Vector3(-2, 2, 1);  // Inverter o inimigo para olhar para a esquerda
        }
        else
        {
            // Jogador à direita
            transform.localScale = new Vector3(2, 2, 1);   // Manter o inimigo olhando para a direita
        }

        Debug.Log("Projétil disparado no jogador.");
    }
}


