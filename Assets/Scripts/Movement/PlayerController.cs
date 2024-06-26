using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using FirstGearGames.SmoothCameraShaker;
public class PlayerController : MonoBehaviour
{
    public Collision collision;
    public float dashForce = 6f; // Força do dash
    public float dashDuration = 0.2f; // Duração do dash em segundos
    public float dashCooldown = 1f; // Tempo de espera entre dashes
    private bool isDashing = false;
    private float dashTimer = 0f;
    private float cooldownTimer = 0f;
    private float coyoteTime = 0.2f; // Tempo de coyote em segundos
    private float coyoteTimeCounter;
    private float jumpBufferTime = 0.2f; // Tempo de buffer em segundos
    private float jumpBufferCounter;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int speed = 5;
    [SerializeField] private float jumpForce = 50;
    [SerializeField] private TrailRenderer tr;
    private Animator animator;
    private Vector3 respawnPoint;
    public GameObject fallDetector;
    public ShakeData DashCameraShake;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        respawnPoint = transform.position;
    }

    private void Update()
{
    if (isDashing)
    {
        return;
    }

    float x = Input.GetAxis("Horizontal");
    float y = Input.GetAxis("Vertical");
    Vector2 dir = new Vector2(x, rb.velocity.y);

    Walk(dir);

    if (collision.onGround)
    {
        coyoteTimeCounter = coyoteTime;
    }
    else
    {
        coyoteTimeCounter -= Time.deltaTime;
    }

    if (Input.GetButtonDown("Jump"))
    {
        jumpBufferCounter = jumpBufferTime;
    }
    else
    {
        jumpBufferCounter -= Time.deltaTime;
    }

    if (jumpBufferCounter > 0)
    {
        Jump();
    }

    if (Input.GetKeyDown(KeyCode.LeftShift) && CanDash())
    {
        StartCoroutine(Dash(x, y));
        CameraShakerHandler.Shake(DashCameraShake);
    }

    // Flip Player
    if (dir.x < 0)
    {
        FlipPlayer(-2.5f);
    }
    else if (dir.x > 0)
    {
        FlipPlayer(2.5f);
    }

    fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);

    animator.SetBool("isRunning", x != 0 && collision.onGround);
    animator.SetBool("isJumping", !collision.onGround && !collision.onWall && rb.velocity.y > 0);

    // Reduz o tempo de cooldown do dash
    cooldownTimer -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        {
            transform.position = respawnPoint;
        }
        else if (collision.tag == "Checkpoint")
        {
            respawnPoint = transform.position;
        }
    }

    private void FlipPlayer(float direction)
    {
        Vector3 scale = transform.localScale;
        scale.x = direction;
        transform.localScale = scale;
    }

    private void Walk(Vector2 dir)
    {
        rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
    }

    private void Jump()
    {
        if ((collision.onGround || coyoteTimeCounter > 0) && jumpBufferCounter > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.velocity += Vector2.up * jumpForce;
            coyoteTimeCounter = 0; // Reseta o tempo de coyote após o pulo
            jumpBufferCounter = 0; // Reseta o tempo de buffer após o pulo
        }
    }

    private bool CanDash()
    {
        return cooldownTimer <= 0f;
    }

    public bool IsDashing()
    {
        return isDashing;
    }
    private IEnumerator Dash(float x, float y)
{
    isDashing = true;
    dashTimer = 0f;
    cooldownTimer = dashCooldown;

    Vector2 dashDirection = new Vector2(x, y).normalized;
    // Ativa o rastro durante o dash
    tr.emitting = true;

    while (dashTimer < dashDuration)
    {
        rb.velocity = dashDirection * dashForce;
        dashTimer += Time.deltaTime;
        yield return null;
    }
    // Desativa o rastro após o dash
    tr.emitting = false;

    isDashing = false;
    rb.velocity = Vector2.zero;
}
}
