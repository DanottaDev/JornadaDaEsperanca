using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Image symptomBackground;
    public Image symptomIcon;
    public TextMeshProUGUI symptomText;

    public Sprite feverIcon;
    public Sprite fatigueIcon;
    public Sprite bonePainIcon;
    public Sprite breathingDifficultyIcon;
    public Sprite tosseIcon;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowSymptom(SymptomTrigger.SymptomType symptom)
    {
        if (symptomIcon == null || symptomText == null || symptomBackground == null)
        {
            Debug.LogWarning("UI elements are missing");
            return;
        }

        switch (symptom)
        {
            case SymptomTrigger.SymptomType.Fever:
                symptomIcon.sprite = feverIcon;
                symptomText.text = "Leah: Acho que estou com febre";
                break;
            case SymptomTrigger.SymptomType.Fatigue:
                symptomIcon.sprite = fatigueIcon;
                symptomText.text = "Leah: Não estou muito bem, estou me sentindo fraca";
                break;
            case SymptomTrigger.SymptomType.BonePain:
                symptomIcon.sprite = bonePainIcon;
                symptomText.text = "Leah: Estou sentindo muita dor nos ossos!";
                break;
            case SymptomTrigger.SymptomType.BreathingDifficulty:
                symptomIcon.sprite = breathingDifficultyIcon;
                symptomText.text = "Leah: Estou com dificuldades para respirar!";
                break;
            case SymptomTrigger.SymptomType.Tosse:
                symptomIcon.sprite = tosseIcon;
                symptomText.text = "Atlas: Acho que estou começando a ficar doente. Essa tosse não é normal...";
                break;
        }

        symptomBackground.gameObject.SetActive(true);
        symptomIcon.gameObject.SetActive(true);
        symptomText.gameObject.SetActive(true);

        // Inicia a corrotina para ocultar o texto após 5 segundos
        StartCoroutine(HideSymptomTextAfterDelay(5f));
    }

    private IEnumerator HideSymptomTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        HideSymptomText();
    }

    public void HideSymptomText()
    {
        if (symptomText == null)
        {
            Debug.LogWarning("Text element is missing");
            return;
        }

        symptomText.gameObject.SetActive(false);
    }

    public void HideSymptom()
    {
        if (symptomIcon == null || symptomText == null || symptomBackground == null)
        {
            Debug.LogWarning("UI elements are missing");
            return;
        }

        symptomBackground.gameObject.SetActive(false);
        symptomIcon.gameObject.SetActive(false);
        symptomText.gameObject.SetActive(false);
    }
}
