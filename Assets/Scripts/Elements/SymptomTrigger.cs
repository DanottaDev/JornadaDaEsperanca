using UnityEngine;

public class SymptomTrigger : MonoBehaviour
{
    public enum SymptomType { Fever, Fatigue, BonePain, BreathingDifficulty, Tosse, AlteraVisao, Tontura, PerdaMemoria, Fraqueza, ConfusaoMental}
    public SymptomType symptom;
    private SymptomEffectController playerSymptomEffect;

    private void Start()
    {
        playerSymptomEffect = GameObject.FindGameObjectWithTag("Player").GetComponent<SymptomEffectController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.ShowSymptom(symptom);
            playerSymptomEffect.ApplySymptom(symptom);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerSymptomEffect.ResetEffects();
            UIManager.Instance.HideSymptom();
        }
    }
}
