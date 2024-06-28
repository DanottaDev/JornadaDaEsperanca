using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;
    private int index;
    public GameObject continueButton;
    public float wordSpeed;
    public bool playerIsClose;
    public GameObject interactionButton;
    private Animator animator;  // Adicione uma referência para o Animator
    private PlayerController playerController;  // Adicione uma referência para o PlayerController

    // Start is called before the first frame update
    void Start()
    {
        dialogueText.text = "";
        dialoguePanel.SetActive(false);
        interactionButton.SetActive(false);
        animator = GetComponent<Animator>();  // Inicialize o Animator
        playerController = FindObjectOfType<PlayerController>();  // Encontre o PlayerController na cena
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                zeroText();
                animator.SetBool("IsInteracting", false);  // Pare a animação de interação
                playerController.enabled = true;  // Reative o PlayerController
            }
            else
            {
                dialoguePanel.SetActive(true);
                animator.SetBool("IsInteracting", true);  // Inicie a animação de interação
                playerController.StopMovement();  // Pare o movimento do jogador
                playerController.enabled = false;  // Desative o PlayerController
                StartCoroutine(Typing());
            }
        }

        if (dialogueText.text == dialogue[index])
        {
            continueButton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                NextLine();
            }
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
        animator.SetBool("IsInteracting", false);  // Pare a animação de interação
        playerController.enabled = true;  // Reative o PlayerController
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        continueButton.SetActive(false);

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
