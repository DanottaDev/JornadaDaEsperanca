using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Image fill; // Referência ao preenchimento da barra

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
        StartCoroutine(Flash());
    }

    private IEnumerator Flash()
    {
        Color originalColor = fill.color;
        fill.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        fill.color = originalColor;

        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(0.1f);
            fill.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            fill.color = originalColor;
        }
    }
}
