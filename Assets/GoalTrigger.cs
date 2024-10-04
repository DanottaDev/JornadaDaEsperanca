using UnityEngine;
using System.Collections;

public class GoalTrigger : MonoBehaviour
{
    public GameObject ball; // A bola que vai entrar no gol
    public ParticleSystem goalParticles; // O sistema de partículas a ser ativado
    private Vector2 initialBallPosition; // Posição inicial da bola
    private Rigidbody2D ballRigidbody; // Referência ao Rigidbody2D da bola

    void Start()
    {
        // Salva a posição inicial da bola
        initialBallPosition = ball.transform.position;
        ballRigidbody = ball.GetComponent<Rigidbody2D>(); // Obtém o Rigidbody2D da bola
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto que colidiu é a bola
        if (other.gameObject == ball)
        {
            // Ativa o sistema de partículas
            goalParticles.Play();

            // Para a bola
            StopBall();

            // Inicia a corrotina para voltar a bola à posição inicial após 2 segundos
            StartCoroutine(ResetBallPositionAfterDelay(2f));
        }
    }

    private void StopBall()
    {
        // Para a bola
        if (ballRigidbody != null)
        {
            // Define a velocidade e rotação da bola como zero para parar
            ballRigidbody.velocity = Vector2.zero;
            ballRigidbody.angularVelocity = 0f; // Zera a rotação
        }
    }

    private IEnumerator ResetBallPositionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Retorna a bola à posição inicial
        ball.transform.position = initialBallPosition;

        // Zera a velocidade e rotação da bola novamente ao retornar
        StopBall();

        // Para o sistema de partículas após o tempo
        goalParticles.Stop();
    }
}
