using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Image fill; // Referência ao preenchimento da barra
    private Coroutine flashCoroutine; // Referência para a Coroutine em execução
    private Color originalColor; // Armazena a cor original permanentemente

    private void Start()
    {
        // Salva a cor original da barra no início
        originalColor = fill.color;
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public void FlashHealthBar()
    {
        // Interrompe o Flash atual, se necessário
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }
        flashCoroutine = StartCoroutine(Flash());
    }

    private IEnumerator Flash()
    {
        fill.color = Color.red; // Define a cor como vermelha

        yield return new WaitForSeconds(0.1f);

        fill.color = originalColor; // Restaura a cor original

        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(0.1f);
            fill.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            fill.color = originalColor;
        }

        // Garante a restauração da cor original no final
        fill.color = originalColor;
        flashCoroutine = null; // Indica que o Flash terminou
    }
}
