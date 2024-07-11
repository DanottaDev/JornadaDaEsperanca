using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 3;
    public float speed = 2f;
    public Transform[] waypoints;
    
    private int currentHealth;
    private int currentWaypointIndex = 0;
    private bool isAttacking = false;
    private bool isDying = false;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Vector3 lastPosition;

    private void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        lastPosition = transform.position;
    }

    private void Update()
    {
        if (!isAttacking && !isDying)
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector2 direction = (targetWaypoint.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        // Flip the sprite based on the movement direction
        if (transform.position.x > lastPosition.x)
        {
            spriteRenderer.flipX = false;
        }
        else if (transform.position.x < lastPosition.x)
        {
            spriteRenderer.flipX = true;
        }

        lastPosition = transform.position;
    }

    public void TakeDamage(int damage)
    {
        if (isDying) return;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(FlashRed());
        }
    }

    private void Die()
    {
        isDying = true;
        animator.SetTrigger("Die");
        StartCoroutine(DestroyAfterAnimation());
    }

    private IEnumerator FlashRed()
    {
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
    }

    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}
