using UnityEngine;

public class SymptomEffectController : MonoBehaviour
{
    public SpriteRenderer playerSprite;
    public ParticleSystem feverEffect;
    public ParticleSystem fatigueEffect;
    public ParticleSystem breathingDifficultyEffect;

    private PlayerController playerController;
    private float originalSpeed;
    private float originalJumpForce;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        originalSpeed = playerController.speed;
        originalJumpForce = playerController.jumpForce;
    }

    public void ApplySymptom(SymptomTrigger.SymptomType symptom)
    {
        ResetEffects(); // Certifique-se de que todos os efeitos anteriores sejam removidos antes de aplicar novos

        switch (symptom)
        {
            case SymptomTrigger.SymptomType.Fever:
                feverEffect.Play();
                break;
            case SymptomTrigger.SymptomType.Fatigue:
                fatigueEffect.Play();
                playerController.speed = (int)(originalSpeed * 0.75f); // Reduce speed by 25%
                break;
            case SymptomTrigger.SymptomType.BonePain:
                playerController.speed = (int)(originalSpeed * 0.75f); // Reduce speed by 25%
                playerController.jumpForce = originalJumpForce * 0.75f; // Reduce jump force by 25%
                break;
            case SymptomTrigger.SymptomType.BreathingDifficulty:
            
                breathingDifficultyEffect.Play();
                playerController.speed = (int)(originalSpeed * 0.75f); // Reduce speed by 25%
                break;
        }
    }

    public void ResetEffects()
    {
        playerSprite.color = Color.white;
        feverEffect.Stop();
        fatigueEffect.Stop();
        breathingDifficultyEffect.Stop();
        playerController.ResetSpeedAndJump(); // Reset speed and jump force
    }
}
