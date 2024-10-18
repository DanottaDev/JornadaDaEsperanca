using System.Collections;
using UnityEngine;

public class AirZone : MonoBehaviour
{   
    public int damage = 1;
    private PlayerController playerController;
    private int originalSpeed;
    private float originalJumpForce;
    public float slowDuration = 2f;  // Duração da lentidão

    private void Start()
    {
        // Pegando a referência do PlayerController no jogador
        playerController = FindObjectOfType<PlayerController>(); // Certifica-se de pegar a referência correta
        originalSpeed = playerController.speed;
        originalJumpForce = playerController.jumpForce;
    }

    // Detecta quando o player entra na área da nebulosa
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                playerController.TakeDamage(damage);  // Causa dano ao jogador
                StartCoroutine(SlowPlayer());  // Aplica a lentidão
            }
        }
    }

    // Aplica a lentidão ao jogador por um tempo determinado
    IEnumerator SlowPlayer()
    {
        playerController.speed = (int)(originalSpeed * 0.75f);  // Converte o resultado para int
        playerController.jumpForce = (int)(originalJumpForce * 0.75f);  // Converte o resultado para int
        yield return new WaitForSeconds(slowDuration);  // Espera a duração da lentidão
        playerController.speed = originalSpeed;  // Restaura a velocidade original
        playerController.jumpForce = originalJumpForce;  // Restaura a força de salto original
    }

    // Detecta quando o player sai da área e restaura a velocidade, se necessário
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                playerController.speed = originalSpeed;  // Restaura a velocidade ao sair da nebulosa
                playerController.jumpForce = originalJumpForce;  // Restaura a força de salto ao sair da nebulosa
            }
        }
    }
}
