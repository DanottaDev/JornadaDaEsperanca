using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    public float speed = 2f;  // Velocidade do inimigo
    public float detectionRange = 5f;  // Alcance da visão do inimigo
    private Transform player;  // Referência ao jogador
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        // Encontra o jogador na cena
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Calcula a distância entre o inimigo e o jogador
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            // Se o jogador estiver dentro do alcance, calcular a direção e mover-se em direção ao jogador
            Vector2 direction = (player.position - transform.position).normalized;
            movement = direction;
        }
        else
        {
            // Se o jogador estiver fora do alcance, o inimigo para de se mover
            movement = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        // Aplica o movimento ao Rigidbody2D do inimigo
        MoveCharacter(movement);
    }

    void MoveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.fixedDeltaTime));
    }

    // Método para desenhar o alcance da visão no editor da Unity
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
