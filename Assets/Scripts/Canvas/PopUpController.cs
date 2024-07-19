using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUpController : MonoBehaviour
{
    public GameObject popUpPanel; // Referência ao painel do pop-up
    public TextMeshProUGUI popUpText; // Referência ao texto do pop-up
    public string message; // Mensagem a ser exibida

    private void Start()
    {
        popUpPanel.SetActive(false); // Desativar o painel no início
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowPopUp();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HidePopUp();
        }
    }

    public void ShowPopUp()
    {
        popUpText.text = message;
        popUpPanel.SetActive(true);
    }

    public void HidePopUp()
    {
        popUpPanel.SetActive(false);
    }
}
