using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Collision collision;
    private bool canDash = true;
    private bool isDashing;
    private bool isWallSliding;
    private float dashingPower = 4f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    private float coyoteTime = 0.2f; // Tempo de coyote em segundos
    private float coyoteTimeCounter;
    private float jumpBufferTime = 0.2f; // Tempo de buffer em segundos
    private float jumpBufferCounter;
    
    //private Vector2 dashDirection;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int speed = 5;
    [SerializeField] private float jumpForce = 50;
    [SerializeField] private float wallSlideSpeed = 0.5f;
    [SerializeField] private float wallJumpForce = 4f;
    [SerializeField] private Vector2 wallJumpDirection = new Vector2(1.5f, 2.5f); 
    [SerializeField] private TrailRenderer tr;
    private Animator animator;
    private Vector3 respawnPoint;
    public GameObject fallDetector;
    void Start()
    {
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    respawnPoint = transform.position;
    }
    void Update()
    {
        if (isDashing)
        {
            return;
        }

        float x = Input.GetAxis("Horizontal");
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

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
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

        // Wall Slide
        if (collision.onWall && !collision.onGround && rb.velocity.y < 0)
        {
            StartWallSlide();
        }
        else
        {
            StopWallSlide();
        }

        fallDetector.transform.position = new Vector2 (transform.position.x, fallDetector.transform.position.y);

        animator.SetBool("isRunning", x != 0 && collision.onGround);
        animator.SetBool("isJumping", !collision.onGround && !collision.onWall && rb.velocity.y > 0);
        animator.SetBool("isWallSliding", isWallSliding);
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
    else if (collision.onWall && !collision.onGround)
    {
        WallJump();
    }
    }
    private void WallJump()
    {
    StopWallSlide();
    Vector2 jumpDirection = new Vector2(wallJumpDirection.x * collision.wallSide, wallJumpDirection.y);
    rb.velocity = new Vector2(0, 0);  // Reseta a velocidade
    rb.velocity += jumpDirection * wallJumpForce;

    // Empurrar o jogador para longe da parede
    rb.AddForce(new Vector2(-collision.wallSide * wallJumpForce, 0), ForceMode2D.Impulse);
    }
    private void StartWallSlide()
    {
        isWallSliding = true;
        rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
    }
    private void StopWallSlide()
    {
        isWallSliding = false;
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        tr.emitting = true;

        /* Calcula a direção do dash com base nas entradas do jogador
        float dashInputX = Input.GetAxis("Horizontal");
        float dashInputY = Input.GetAxis("Vertical");
        
        dashDirection = new Vector2(dashInputX, dashInputY).normalized;  // Normaliza para manter a velocidade constante

        Se não houver entrada, manter a direção atual
        
        if (dashDirection == Vector2.zero)
        {
            dashDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        }
        
        rb.velocity = dashDirection * dashingPower;*/
        
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}

