using UnityEngine;

public class SymptomTrigger : MonoBehaviour
{
    public enum SymptomType { Fever, Fatigue, BonePain, BreathingDifficulty }
    public SymptomType symptom;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.ShowSymptom(symptom);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.HideSymptom();
        }
    }
}
