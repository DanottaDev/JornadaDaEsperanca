using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    private float bounce = 9f;
    private Animator animator;
    private AudioSource audioSource;

    private void Start()
    {
        // Obtém o componente Animator
        animator = GetComponent<Animator>();
        // Obtém o componente AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o objeto colidido tem a tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Adiciona força ao jogador
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);

            // Aciona a animação de ativação
            animator.SetBool("Activated", true);

            // Toca o som
            if (audioSource != null)
            {
                audioSource.Play();
            }

            // (Opcional) Volta para o estado inicial após um tempo
            StartCoroutine(ResetAnimation());
        }
    }

    private IEnumerator ResetAnimation()
    {
        // Aguarda 0.2 segundos (ajuste conforme necessário)
        yield return new WaitForSeconds(0.2f);

        // Volta para o estado inicial
        animator.SetBool("Activated", false);
    }
}
