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
    public Sprite tonturaIcon;
    public Sprite alteraVisaoIcon;
    public Sprite confusaoMentalIcon;
    public Sprite perdaMemoriaIcon;
    public Sprite fraquezaIcon;
    public Sprite tosseIcon;
    public Sprite febreIcon;
    public Sprite fadigaIcon;
    public Sprite perdaPesoIcon;
    public Sprite faltaArIcon;
    public Sprite cansacoIcon;

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
            case SymptomTrigger.SymptomType.Tontura:
                symptomIcon.sprite = tonturaIcon;
                symptomText.text = "Atlas: Minha cabeça está rodando... o que está acontecendo?";
                break;
            case SymptomTrigger.SymptomType.AlteraVisao:
                symptomIcon.sprite = alteraVisaoIcon;
                symptomText.text = "Atlas: Minha visão está estranha... parece que estou vendo duplicado.";
                break;
            case SymptomTrigger.SymptomType.ConfusaoMental:
                symptomIcon.sprite = confusaoMentalIcon;
                symptomText.text = "Atlas: Por que tudo parece tão confuso de repente?";
                break;
            case SymptomTrigger.SymptomType.PerdaMemoria:
                symptomIcon.sprite = perdaMemoriaIcon;
                symptomText.text = "Atlas: Isso parece familiar, mas não me lembro por quê.";
                break;
            case SymptomTrigger.SymptomType.Fraqueza:
                symptomIcon.sprite = fraquezaIcon;
                symptomText.text = "Atlas: Sinto como se não tivesse forças para continuar.";
                break;    
            case SymptomTrigger.SymptomType.Tosse:
                symptomIcon.sprite = tosseIcon;
                symptomText.text = "Celine: Acho que estou começando a ficar doente. Essa tosse não é normal...";
                break;
            case SymptomTrigger.SymptomType.PerdaPeso:
                symptomIcon.sprite = perdaPesoIcon;
                symptomText.text = "Celine: Eu costumava adorar comer aqui... Mas agora, nem consigo pensar em comida.";
                break;      
            case SymptomTrigger.SymptomType.Febre:
                symptomIcon.sprite = febreIcon;
                symptomText.text = "Celine: Estou ficando quente... Será que estou com febre?";
                break;
            case SymptomTrigger.SymptomType.Fadiga:
                symptomIcon.sprite = fadigaIcon;
                symptomText.text = "Celine: Minhas forças estão sumindo... Não consigo mais manter o ritmo.";
                break;
            case SymptomTrigger.SymptomType.FaltaAr:
                symptomIcon.sprite = faltaArIcon;
                symptomText.text = "Celine: Está ficando difícil respirar... Preciso parar por um momento.";
                break;
            case SymptomTrigger.SymptomType.Cansaco:
                symptomIcon.sprite = cansacoIcon;
                symptomText.text = "Celine: Meu corpo está tão pesado... Não sei se consigo continuar.";
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
