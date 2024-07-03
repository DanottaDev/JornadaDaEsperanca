using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingScreenManager : MonoBehaviour
{
    // Referência ao TextMeshPro no Canvas
    public TextMeshProUGUI messageText;

    // Lista de mensagens
    private string[] messages = new string[]
    {
        "A jornada para a cura começa com o reconhecimento dos primeiros sinais. Fique atento aos sintomas e busque ajuda médica imediatamente.",
        "Cada passo adiante é um passo para a esperança. Juntos, podemos vencer.",
        "Lembre-se, não estamos sozinhos nessa luta. O apoio da família, amigos e médicos é crucial.",
        "Coragem e determinação são nossas maiores armas. Continue avançando.",
        "A detecção precoce salva vidas. Conheça os sinais e ajude a espalhar a conscientização.",
        "A jornada pode ser difícil, mas a força está dentro de cada um de nós.",
        "Estamos perto da vitória. Cada batalha nos torna mais fortes.",
        "Nunca subestime o poder da esperança e da solidariedade. Juntos, somos invencíveis."
    };

    void Start()
    {
        // Seleciona uma mensagem aleatória da lista
        string randomMessage = GetRandomMessage();

        // Atribui a mensagem ao TextMeshPro
        messageText.text = randomMessage;
    }

    // Função para selecionar uma mensagem aleatória
    private string GetRandomMessage()
    {
        int randomIndex = Random.Range(0, messages.Length);
        return messages[randomIndex];
    }
}
