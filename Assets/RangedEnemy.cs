using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public Transform firePoint;  // O ponto de origem do projétil
    public GameObject projectilePrefab;  // O prefab do projétil
    public float attackRange = 10f;  // Distância máxima de ataque
    public float fireRate = 2f;  // Tempo entre os disparos
    private float nextFireTime = 0f;

    public Transform player;

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        
        if (distanceToPlayer <= attackRange && Time.time >= nextFireTime)
        {
            Attack();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Attack()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
}
