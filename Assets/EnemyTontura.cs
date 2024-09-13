using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoTontura : MonoBehaviour
{
    public GameObject projetilPrefab; // Prefab do projétil
    public Transform pontoDeDisparo;  // Ponto de onde o projétil será lançado
    public float distanciaMaximaAtaque = 10f;  // Distância máxima para o ataque à distância
    public float alcanceCorpoACorpo = 3f;  // Distância para o ataque de curto alcance (laser)
    public float tempoEntreAtaques = 2f;  // Tempo entre os ataques
    public int danoProjetil = 10;
    public int danoLaser = 20;

    private Transform jogador;
    private float tempoParaProximoAtaque;

    void Start()
    {
        jogador = GameObject.FindWithTag("Player").transform;
        tempoParaProximoAtaque = 0f;
    }

    void Update()
    {
        float distanciaParaJogador = Vector3.Distance(jogador.position, transform.position);

        if (tempoParaProximoAtaque <= 0f)
        {
            if (distanciaParaJogador <= alcanceCorpoACorpo)
            {
                AtacarComLaser();  // Laser ou ataque corpo-a-corpo
            }
            else if (distanciaParaJogador <= distanciaMaximaAtaque)
            {
                AtacarComProjetil();
            }

            tempoParaProximoAtaque = tempoEntreAtaques;
        }
        else
        {
            tempoParaProximoAtaque -= Time.deltaTime;
        }
    }

    void AtacarComProjetil()
    {
        // Animação de ataque à distância (projétil)
        GetComponent<Animator>().SetTrigger("Attack2");

        // Lança o projétil
        GameObject projetil = Instantiate(projetilPrefab, pontoDeDisparo.position, pontoDeDisparo.rotation);
        Rigidbody2D rb = projetil.GetComponent<Rigidbody2D>();
        Vector2 direcao = (jogador.position - pontoDeDisparo.position).normalized;
        rb.velocity = direcao * 10f; // Velocidade do projétil

        // Aqui você pode adicionar dano via colisão do projétil no script do próprio projétil
    }

    void AtacarComLaser()
    {
        // Animação de ataque de laser
        GetComponent<Animator>().SetTrigger("Attack1");

        // Aplicar dano diretamente, já que o ataque é imediato
        jogador.GetComponent<PlayerController>().TakeDamage(danoLaser);
    }
}
