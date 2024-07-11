using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int index;
    public float wordSpeed;
    public bool playerIsClose;
    public GameObject interactionButton;
    private PlayerController playerController;
    private bool isDialogueActive = false; // Variável para controlar se o diálogo está ativo

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
        else
        {
        }
    }

    void StartDialogue()
    {
        isDialogueActive = true; // Marca o diálogo como ativo
        dialoguePanel.SetActive(true);
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
