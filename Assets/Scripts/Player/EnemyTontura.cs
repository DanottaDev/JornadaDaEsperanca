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

        // Verificar se o jogador está à esquerda ou à direita
        if (jogador.position.x < transform.position.x)
        {
            // Jogador está à esquerda, então flip o inimigo para a esquerda
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            // Jogador está à direita, então flip o inimigo para a direita
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        // Se o tempo de ataque tiver acabado, realiza o próximo ataque
        if (tempoParaProximoAtaque <= 0f && !estaAtacando)
        {
            // Voltar para o estado idle antes de decidir o ataque
            animator.SetBool("IsAttacking", false); // Resetar o estado de ataque
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

            // Voltar para o estado Idle entre os ataques
            if (estaAtacando && tempoParaProximoAtaque > 0)
            {
                estaAtacando = false; // Reiniciar o estado de ataque
                animator.SetBool("IsAttacking", false); // Garantir que volte para idle
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
        rb.velocity = direcao * 5f; // Velocidade do projétil
        Debug.Log("Projétil lançado.");
        
        // Após o ataque, resetar o estado para idle
        Invoke("VoltarParaIdle", 0.5f);  // Retorna ao estado Idle após 0.5 segundos (ajuste conforme necessário)

        // Flipar o projétil se o jogador estiver à esquerda
    if (direcao.x < 0) // Se a direção do projétil for para a esquerda
    {
        Vector3 escalaProjetil = projetil.transform.localScale;
        escalaProjetil.x = -Mathf.Abs(escalaProjetil.x); // Garantir que o eixo x seja negativo
        projetil.transform.localScale = escalaProjetil;
    }
    else // Se o projétil for para a direita
    {
        Vector3 escalaProjetil = projetil.transform.localScale;
        escalaProjetil.x = Mathf.Abs(escalaProjetil.x); // Garantir que o eixo x seja positivo
        projetil.transform.localScale = escalaProjetil;
    }
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
        
        // Após o ataque, resetar o estado para idle
        Invoke("VoltarParaIdle", 0.5f);  // Retorna ao estado Idle após 0.5 segundos (ajuste conforme necessário)
    }

    void VoltarParaIdle()
    {
        animator.SetBool("IsAttacking", false);
        animator.SetBool("AtaqueDistancia", false);
        animator.SetBool("AtaqueLaser", false);
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
