using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    public float dashForce = 20f;
    public float wallSlideSpeed = 2f;
    public float wallJumpForce = 12f;

    [Header("Attack Settings")]
    public float attackCooldown = 0.5f;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    private Rigidbody2D rb;
    private bool isDashing;
    private bool isJumping;
    private bool isWallSliding;
    private bool isWallJumping;
    private bool canAttack = true;
    private float horizontalInput;
    private float attackTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        HandleMovement();
        HandleJump();
        HandleDash();
        HandleWallSlide();
        HandleWallJump();
        HandleAttack();
    }

    void HandleMovement()
    {
        if (!isDashing && !isWallJumping)
        {
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        }
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
        }
    }

    void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && !isWallSliding)
        {
            isDashing = true;
            rb.velocity = new Vector2(horizontalInput * dashForce, rb.velocity.y);
            Invoke("EndDash", 0.2f); // Duration of the dash
        }
    }

    void EndDash()
    {
        isDashing = false;
    }

    void HandleWallSlide()
    {
        if (IsTouchingWall() && !IsGrounded() && horizontalInput != 0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
        }
        else
        {
            isWallSliding = false;
        }
    }

    void HandleWallJump()
    {
        if (isWallSliding && Input.GetButtonDown("Jump"))
        {
            isWallJumping = true;
            rb.velocity = new Vector2(-horizontalInput * wallJumpForce, jumpForce);
            Invoke("EndWallJump", 0.2f);
        }
    }

    void EndWallJump()
    {
        isWallJumping = false;
    }

    void HandleAttack()
    {
        if (Input.GetButtonDown("Fire1") && canAttack)
        {
            canAttack = false;
            attackTime = Time.time + attackCooldown;
            Invoke("PerformAttack", 0.1f); // Delay to sync with animation
        }

        if (Time.time >= attackTime)
        {
            canAttack = true;
        }
    }

    void PerformAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Handle enemy damage here
            Debug.Log("Hit " + enemy.name);
        }
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Ground"));
    }

    bool IsTouchingWall()
    {
        return Physics2D.Raycast(transform.position, Vector2.right * horizontalInput, 1f, LayerMask.GetMask("Wall"));
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
