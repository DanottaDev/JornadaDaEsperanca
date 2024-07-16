using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI npcNameText; // Referência para o nome do NPC
    public Image npcImage; // Referência para a imagem do NPC
    public string[] dialogue;
    private int index;
    public float wordSpeed;
    public bool playerIsClose;
    public GameObject interactionButton;
    private PlayerController playerController;
    private bool isDialogueActive = false; // Variável para controlar se o diálogo está ativo

    // Adicione as variáveis para o nome e a imagem do NPC
    public string npcName;
    public Sprite npcSprite;

    void Start()
    {
        dialogueText.text = "";
        dialoguePanel.SetActive(false);
        interactionButton.SetActive(false);
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose && !isDialogueActive) // Verifica se o diálogo não está ativo
        {
            StartDialogue();
        }

        if (dialogueText.text == dialogue[index])
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                NextLine();
            }
        }
    }

    void StartDialogue()
    {
        isDialogueActive = true; // Marca o diálogo como ativo
        dialoguePanel.SetActive(true);
        npcNameText.text = npcName; // Define o nome do NPC
        npcImage.sprite = npcSprite; // Define a imagem do NPC
        playerController.StopMovement();
        playerController.enabled = false;
        StartCoroutine(Typing());
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
        playerController.enabled = true;
        isDialogueActive = false; // Marca o diálogo como não ativo ao cancelar
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            playerIsClose = true;
            interactionButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            playerIsClose = false;
            interactionButton.SetActive(false);
        }
    }
}
