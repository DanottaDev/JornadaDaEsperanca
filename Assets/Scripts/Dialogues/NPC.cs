using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI npcNameText;
    public Image npcImage;
    public List<DialogueLine> dialogueLines; // Lista de linhas de diálogo
    private int index;
    public float wordSpeed;
    public bool playerIsClose;
    public GameObject interactionButton;
    private PlayerController playerController;
    private bool isDialogueActive = false;

    void Start()
    {
        dialogueText.text = "";
        dialoguePanel.SetActive(false);
        interactionButton.SetActive(false);
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose && !isDialogueActive)
        {
            StartDialogue();
        }

        if (dialogueText.text == dialogueLines[index].text)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                NextLine();
            }
        }
    }

    void StartDialogue()
    {
        isDialogueActive = true;
        dialoguePanel.SetActive(true);
        playerController.StopMovement();
        playerController.enabled = false;
        ShowDialogueLine();
    }

    void ShowDialogueLine()
    {
        DialogueLine line = dialogueLines[index];
        npcNameText.text = line.name;
        npcImage.sprite = line.image;
        dialogueText.text = "";
        StartCoroutine(Typing(line.text));
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
        playerController.enabled = true;
        isDialogueActive = false;
    }

    IEnumerator Typing(string text)
    {
        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        if (index < dialogueLines.Count - 1)
        {
            index++;
            ShowDialogueLine();
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
