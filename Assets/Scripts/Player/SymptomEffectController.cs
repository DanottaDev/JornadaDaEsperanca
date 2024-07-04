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
                /*Color feverColor;
                if (ColorUtility.TryParseHtmlString("#FF8888", out feverColor))
                {
                    playerSprite.color = feverColor; // Change color to the specified hexadecimal color for fever
                }*/
                feverEffect.Play();
                break;
            case SymptomTrigger.SymptomType.Fatigue:
                //playerSprite.color = Color.yellow; // Change color to yellow for fatigue
                fatigueEffect.Play();
                playerController.speed = (int)(originalSpeed * 0.75f); // Reduce speed by 25%
                break;
            case SymptomTrigger.SymptomType.BonePain:
                /*Color lightGrayColor;
                if (ColorUtility.TryParseHtmlString("#C0C0C0", out lightGrayColor))
                {
                    playerSprite.color = lightGrayColor; // Change color to light gray (hexadecimal #C0C0C0)
                }*/
                playerController.speed = (int)(originalSpeed * 0.75f); // Reduce speed by 25%
                playerController.jumpForce = originalJumpForce * 0.75f; // Reduce jump force by 25%
                break;
            case SymptomTrigger.SymptomType.BreathingDifficulty:
                /*Color breathingDifficultyColor;
                if (ColorUtility.TryParseHtmlString("#CFBBE5", out breathingDifficultyColor))
                {
                    playerSprite.color = breathingDifficultyColor; // Change color to the specified hexadecimal color for breathing difficulty
                }*/
                playerController.speed = (int)(originalSpeed * 0.75f); // Reduce speed by 25%
                break;
        }
    }

    public void ResetEffects()
    {
        playerSprite.color = Color.white;
        feverEffect.Stop();
        fatigueEffect.Stop();
        playerController.ResetSpeedAndJump(); // Reset speed and jump force
    }
}
