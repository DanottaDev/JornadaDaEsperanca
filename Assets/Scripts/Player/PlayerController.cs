using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FirstGearGames.SmoothCameraShaker;

public class PlayerController : MonoBehaviour
{
    // Variáveis Públicas
    public Collision collision; // Referência ao script de colisão
    public float dashForce = 6f; // Força do dash
    public float dashDuration = 0.2f; // Duração do dash em segundos
    public float dashCooldown = 1f; // Tempo de espera entre dashes
    public GameObject fallDetector; // Detector de queda
    public ShakeData DashCameraShake; // Dados do shake da câmera durante o dash
    public HealthBar healthBar; // Adicionando referência para HealthBar
    public int maxHealth = 5; // Saúde máxima do jogador
    public ParticleSystem invulnerabilityParticles; // Referência ao sistema de partículas de invulnerabilidade
    public float attackRadius = 1f; // Raio de ataque
    public LayerMask enemyLayer; // Layer dos inimigos
    public int attackDamage = 1; // Dano do ataque
    public AudioClip jumpSound;
    public AudioClip dashSound;
    public AudioClip attackSound;
    public AudioClip takeDamageSound;
    public AudioClip healSound;
    public AudioClip wallJumpSound;

    public AudioClip[] footstepSounds; // Array de clipes de som de passos
    public float footstepInterval = 0.5f; // Intervalo entre os sons de passos
    public AudioSource audioSource; // Referência ao AudioSource

    // Variáveis Privadas
    private float originalSpeed;
    private float originalJumpForce;
    private bool isWalking = false;
    private bool isDashing = false; // Flag para saber se está dashing
    private float dashTimer = 0f; // Temporizador do dash
    private float cooldownTimer = 0f; // Temporizador de cooldown do dash
    private float coyoteTime = 0.2f; // Tempo de coyote em segundos
    private float coyoteTimeCounter; // Contador de tempo de coyote
    private float jumpBufferTime = 0.2f; // Tempo de buffer em segundos
    private float jumpBufferCounter; // Contador de tempo de buffer
    private int currentHealth; // Saúde atual do jogador
    private SpriteRenderer spriteRenderer;
    private bool isInvulnerable = false; // Flag para invulnerabilidade
    private bool isPlayingFootstepSound = false;

    // Variáveis de Wall Slide e Wall Jump
    private bool isWallSliding = false;
    public float wallSlideSpeed = 2f; // Velocidade do wall slide
    public float wallJumpForce = 30f; // Força do wall jump
    public Vector2 wallJumpDirection = new Vector2(1, 1); // Direção do wall jump

    // Referências de Componentes
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public int speed = 5; // Tornar público
    [SerializeField] public float jumpForce = 50; // Tornar público
    [SerializeField] private TrailRenderer tr;
    private Animator animator;
    private Vector3 respawnPoint;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        respawnPoint = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSpeed = speed;
        originalJumpForce = jumpForce;

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        invulnerabilityParticles.Stop();

        audioSource = GetComponent<AudioSource>(); // Obtém o AudioSource
    }

    private void Update()
    {
        Attack();

        if (isDashing)
        {
            return; // Se estiver dashing, não processa outras ações
        }

        // Controle de Movimento
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, rb.velocity.y);

        Walk(dir);

        // Controle de som dos passos
        if (x != 0 && collision.onGround)
        {
            if (!isWalking)
            {
                isWalking = true;
            }
        }
        else
        {
            if (isWalking)
            {
                isWalking = false;
            }
        }

        // Lógica de coyote time
        if (collision.onGround)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // Lógica de jump buffer
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (jumpBufferCounter > 0 && (collision.onGround || isWallSliding))
        {
            Jump();
        }

        // Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && CanDash())
        {
            StartCoroutine(Dash(x, y));
            CameraShakerHandler.Shake(DashCameraShake);
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

        // Wall Jump
        if (isWallSliding && Input.GetButtonDown("Jump"))
        {
            WallJump();
        }

        // Flip do Jogador
        if (dir.x < 0)
        {
            FlipPlayer(-2.5f);
        }
        else if (dir.x > 0)
        {
            FlipPlayer(2.5f);
        }

        // Atualiza posição do detector de queda
        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);

        // Atualiza animações
        animator.SetBool("isRunning", x != 0 && collision.onGround);
        animator.SetBool("isJumping", !collision.onGround && !collision.onWall && rb.velocity.y > 0);
        animator.SetBool("isWallSliding", isWallSliding);

        // Reduz o tempo de cooldown do dash
        cooldownTimer -= Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        {
            transform.position = respawnPoint; // Reseta a posição do jogador ao ponto de respawn
        }
        else if (collision.tag == "Checkpoint")
        {
            respawnPoint = transform.position; // Atualiza o ponto de respawn
        }
    }

    private void FlipPlayer(float direction)
    {
        Vector3 scale = transform.localScale;
        scale.x = direction;
        transform.localScale = scale; // Inverte a escala no eixo x para flipar o jogador
    }

    private void Walk(Vector2 dir)
    {
        rb.velocity = new Vector2(dir.x * speed, rb.velocity.y); // Movimento horizontal
        
        if (dir.x != 0 && collision.onGround && !isPlayingFootstepSound)
        {
            StartCoroutine(PlayFootstepSounds());
        }
    }


    private IEnumerator PlayFootstepSounds()
{
    isPlayingFootstepSound = true;
    while (isWalking && collision.onGround)
    {
        AudioClip footstepSound = footstepSounds[Random.Range(0, footstepSounds.Length)];
        audioSource.PlayOneShot(footstepSound);
        yield return new WaitForSeconds(footstepInterval);
    }
    isPlayingFootstepSound = false;
}


    private void Jump()
    {
    if ((collision.onGround || coyoteTimeCounter > 0 || isWallSliding) && jumpBufferCounter > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0); // Reseta a velocidade vertical antes de pular
            rb.velocity += Vector2.up * jumpForce; // Aplica força de pulo
            coyoteTimeCounter = 0; // Reseta o tempo de coyote após o pulo
            jumpBufferCounter = 0; // Reseta o tempo de buffer após o pulo
            audioSource.PlayOneShot(jumpSound); // Toca o som de pulo
        }
    }

    private void WallJump()
    {
        Vector2 jumpDirection = new Vector2(wallJumpDirection.x * -transform.localScale.x, wallJumpDirection.y);
        rb.velocity = new Vector2(jumpDirection.x * wallJumpForce, jumpDirection.y * wallJumpForce);
        StopWallSlide(); // Para o wall slide após o wall jump

        audioSource.PlayOneShot(wallJumpSound);
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

    private bool CanDash()
    {
        return cooldownTimer <= 0f; // Verifica se o cooldown do dash terminou
    }

    public bool IsDashing()
    {
        return isDashing; // Retorna se o jogador está dashing ou não
    }

    private IEnumerator Dash(float x, float y)
    {
        isDashing = true;
        dashTimer = 0f;
        cooldownTimer = dashCooldown;

        Vector2 dashDirection = new Vector2(x, y).normalized;
        tr.emitting = true; // Ativa o rastro durante o dash

        audioSource.PlayOneShot(dashSound); // Toca o som do dash

        while (dashTimer < dashDuration)
        {
            rb.velocity = dashDirection * dashForce;
            dashTimer += Time.deltaTime;
            yield return null;
        }

        tr.emitting = false; // Desativa o rastro após o dash
        isDashing = false;
        rb.velocity = Vector2.zero; // Reseta a velocidade após o dash
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("isAttacking");

            audioSource.PlayOneShot(attackSound); // Toca o som de ataque

            // Detecta inimigos no raio de ataque
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRadius, enemyLayer);

            // Dá dano aos inimigos detectados
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
                CameraShakerHandler.Shake(DashCameraShake);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    public void TakeDamage(int damage)
    {
        if (!isInvulnerable)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                ResetPlayerPosition();
            }
            healthBar.SetHealth(currentHealth);
            healthBar.FlashHealthBar(); // Chama o efeito de flash da barra de vida

            audioSource.PlayOneShot(takeDamageSound); // Toca o som de dano

            // Inicia o efeito de piscar vermelho
            StartCoroutine(FlashRed());
        }
    }

    private IEnumerator FlashRed()
    {
        // Salva a cor original do sprite
        Color originalColor = spriteRenderer.color;

        // Define a cor vermelha
        spriteRenderer.color = Color.red;

        // Espera um breve momento
        yield return new WaitForSeconds(0.1f);

        // Restaura a cor original
        spriteRenderer.color = originalColor;

        // Repete o efeito de piscar algumas vezes
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = originalColor;
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.SetHealth(currentHealth);
        audioSource.PlayOneShot(healSound); // Toca o som de cura
    }

    public void ResetPlayerPosition()
    {
        transform.position = respawnPoint; // Reseta a posição do jogador ao ponto de respawn
        currentHealth = maxHealth; // Reseta a saúde do jogador para o valor máximo
        healthBar.SetHealth(currentHealth); // Atualiza a barra de vida
        StartInvulnerability(3f); // Inicia a invulnerabilidade por 3 segundos
    }

    public void StartInvulnerability(float duration)
    {
        StartCoroutine(InvulnerabilityCooldown(duration));
    }

    private IEnumerator InvulnerabilityCooldown(float duration)
    {
        isInvulnerable = true;
        invulnerabilityParticles.Play(); // Ativa o sistema de partículas de invulnerabilidade, se houver

        // Piscar o jogador para indicar invulnerabilidade
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;

        for (float i = 0; i < duration; i += 0.2f)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f); // Define a cor com transparência
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(0.1f);
        }

        isInvulnerable = false;
        invulnerabilityParticles.Stop(); // Desativa o sistema de partículas de invulnerabilidade, se houver
    }

    public void ResetSpeedAndJump()
    {
        speed = (int)originalSpeed; // Cast explícito para int
        jumpForce = originalJumpForce;
    }

    public void StopMovement()
    {
        rb.velocity = Vector2.zero;
    }
}
