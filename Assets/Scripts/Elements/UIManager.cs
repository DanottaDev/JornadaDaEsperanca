using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
                symptomText.text = "Leah está com Febre!";
                break;
            case SymptomTrigger.SymptomType.Fatigue:
                symptomIcon.sprite = fatigueIcon;
                symptomText.text = "Leah se sente fraca";
                break;
            case SymptomTrigger.SymptomType.BonePain:
                symptomIcon.sprite = bonePainIcon;
                symptomText.text = "Leah está com dor nos ossos";
                break;
            case SymptomTrigger.SymptomType.BreathingDifficulty:
                symptomIcon.sprite = breathingDifficultyIcon;
                symptomText.text = "Leah sente dificuldade para Respirar!";
                break;
        }

        symptomBackground.gameObject.SetActive(true);
        symptomIcon.gameObject.SetActive(true);
        symptomText.gameObject.SetActive(true);
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
