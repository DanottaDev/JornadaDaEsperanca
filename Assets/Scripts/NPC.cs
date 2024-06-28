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
    private Animator animator;
    private PlayerController playerController;  // Adicione uma referência ao script PlayerController

    // Start is called before the first frame update
    void Start()
    {
        dialogueText.text = "";
        dialoguePanel.SetActive(false);
        interactionButton.SetActive(false);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                EndInteraction();
            }
            else
            {
                StartInteraction();
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
        continueButton.SetActive(false);

        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            EndInteraction();
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            playerIsClose = true;
            interactionButton.SetActive(true);
            playerController = coll.GetComponent<PlayerController>();  // Obtenha a referência ao PlayerController do jogador
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

    private void StartInteraction()
    {
        dialoguePanel.SetActive(true);
        animator.SetBool("IsInteracting", true);  // Inicie a animação de interação
        StartCoroutine(Typing());
        if (playerController != null)
        {
            playerController.enabled = false;  // Desative o controle de movimento do jogador
        }
    }

    private void EndInteraction()
    {
        zeroText();
        animator.SetBool("IsInteracting", false);  // Pare a animação de interação
        if (playerController != null)
        {
            playerController.enabled = true;  // Reative o controle de movimento do jogador
        }
    }
}
