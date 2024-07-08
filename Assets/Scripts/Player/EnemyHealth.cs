using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isDying = false;

    private void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
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
