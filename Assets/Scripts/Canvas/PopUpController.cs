using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUpController : MonoBehaviour
{
    public GameObject popUpPanel; // Referência ao painel do pop-up
    public TextMeshProUGUI popUpText; // Referência ao texto do pop-up
    public string message; // Mensagem a ser exibida
    public float fadeDuration = 0.5f; // Duração do efeito de fade

    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = popUpPanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = popUpPanel.AddComponent<CanvasGroup>();
        }
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
        StartCoroutine(FadeIn());
    }

    public void HidePopUp()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));
            yield return null;
        }
        popUpPanel.SetActive(false);
    }
}
