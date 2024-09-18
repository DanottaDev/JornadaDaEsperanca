using UnityEngine;

public class InimigoAtaque : MonoBehaviour
{
    public GameObject projetilPrefab; // Prefab do projétil
    public Transform pontoDeDisparo;  // Ponto de onde o projétil será lançado
    public float alcanceCorpoACorpo = 1.5f;  // Distância para o ataque de curto alcance (laser)
    public float alcanceMaximoProjetil = 5f;  // Alcance máximo para o ataque à distância
    public float tempoEntreAtaques = 1.5f;  // Tempo entre os ataques
    public int danoProjetil = 1;
    public int danoLaser = 2;

    private Transform jogador;
    private float tempoParaProximoAtaque;
    private bool estaAtacando; // Variável para monitorar o estado de ataque

    private Animator animator;

    void Start()
    {
        jogador = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>(); // Pegar o componente de animação
        tempoParaProximoAtaque = 0f;
        estaAtacando = false; // No início, o inimigo não está atacando
    }

    void Update()
    {
        float distanciaParaJogador = Vector3.Distance(jogador.position, transform.position);

        // Se o tempo de ataque tiver acabado, realiza o próximo ataque
        if (tempoParaProximoAtaque <= 0f && !estaAtacando)
        {
            // Resetar os booleanos de ataque para garantir que eles só serão ativados no momento correto
            animator.SetBool("AtaqueDistancia", false);
            animator.SetBool("AtaqueLaser", false);

            // Decidir qual ataque fazer, com base na distância
            if (distanciaParaJogador <= alcanceCorpoACorpo)
            {
                Debug.Log("Inimigo está perto do jogador. Executando ataque corpo a corpo (Laser).");
                AtacarComLaser();  // Laser ou ataque corpo a corpo
                estaAtacando = true;  // Marcar como atacando
            }
            else if (distanciaParaJogador > alcanceCorpoACorpo && distanciaParaJogador <= alcanceMaximoProjetil)
            {
                Debug.Log("Inimigo está distante do jogador. Executando ataque à distância (Projétil).");
                AtacarComProjetil();  // Ataque à distância
                estaAtacando = true;  // Marcar como atacando
            }
            else
            {
                Debug.Log("Inimigo fora do alcance de ataque.");
            }

            // Resetar o tempo de ataque
            tempoParaProximoAtaque = tempoEntreAtaques;
        }
        else
        {
            // Diminuir o tempo entre ataques
            tempoParaProximoAtaque -= Time.deltaTime;

            // Garantir que o inimigo retorne à animação idle quando não está atacando
            if (estaAtacando && tempoParaProximoAtaque <= 0)
            {
                estaAtacando = false; // Reiniciar o estado de ataque
                animator.SetBool("IsAttacking", false); // Definir como falso quando não está atacando
                Debug.Log("Inimigo parou de atacar. Voltando para o estado Idle.");
            }
        }
    }

    void AtacarComProjetil()
    {
        // Ativar a animação de ataque à distância (projétil)
        animator.SetBool("IsAttacking", true);
        animator.SetBool("AtaqueDistancia", true);  // Ativa a animação de ataque à distância
        Debug.Log("Animação de ataque à distância ativada.");

        // Lança o projétil
        GameObject projetil = Instantiate(projetilPrefab, pontoDeDisparo.position, pontoDeDisparo.rotation);
        Rigidbody2D rb = projetil.GetComponent<Rigidbody2D>();
        Vector2 direcao = (jogador.position - pontoDeDisparo.position).normalized;
        rb.velocity = direcao * 10f; // Velocidade do projétil
        Debug.Log("Projétil lançado.");
    }

    void AtacarComLaser()
    {
        // Ativar a animação de ataque corpo a corpo (laser)
        animator.SetBool("IsAttacking", true);
        animator.SetBool("AtaqueLaser", true);  // Ativa a animação de ataque corpo a corpo
        Debug.Log("Animação de ataque corpo a corpo ativada.");

        // Aplicar dano diretamente ao jogador
        jogador.GetComponent<PlayerController>().TakeDamage(danoLaser);
        Debug.Log("Dano de laser aplicado ao jogador.");
    }

    // Desenhar os raios de alcance no editor para visualização
    void OnDrawGizmosSelected()
    {
        // Cor vermelha para o alcance do laser (ataque corpo a corpo)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, alcanceCorpoACorpo);

        // Cor azul para o alcance do projétil (ataque à distância)
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, alcanceMaximoProjetil);
    }
}
