using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaController : MonoBehaviour
{
    public GameObject plataforma1;
    public GameObject plataforma2;
    public float tempoLigada = 1.0f; // Tempo que cada plataforma fica visível e com colisor ativado
    public float tempoDesligada = 1.0f; // Tempo que cada plataforma fica invisível e com colisor desativado

    private SpriteRenderer spriteRenderer1;
    private Collider2D colisor1;
    private SpriteRenderer spriteRenderer2;
    private Collider2D colisor2;

    void Start()
    {
        spriteRenderer1 = plataforma1.GetComponent<SpriteRenderer>();
        colisor1 = plataforma1.GetComponent<Collider2D>();
        spriteRenderer2 = plataforma2.GetComponent<SpriteRenderer>();
        colisor2 = plataforma2.GetComponent<Collider2D>();
        StartCoroutine(CicloAlternarPlataformas());
    }

    IEnumerator CicloAlternarPlataformas()
    {
        while (true)
        {
            // Plataforma 1 ligada, plataforma 2 desligada
            spriteRenderer1.enabled = true;
            colisor1.enabled = true;
            spriteRenderer2.enabled = false;
            colisor2.enabled = false;
            yield return new WaitForSeconds(tempoLigada);

            // Plataforma 1 desligada, plataforma 2 ligada
            spriteRenderer1.enabled = false;
            colisor1.enabled = false;
            spriteRenderer2.enabled = true;
            colisor2.enabled = true;
            yield return new WaitForSeconds(tempoDesligada);
        }
    }
}
