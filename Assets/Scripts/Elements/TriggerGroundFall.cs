using UnityEngine;
using FirstGearGames.SmoothCameraShaker; // Certifique-se que esse pacote esteja importado
using System.Collections;

public class ShakeTrigger : MonoBehaviour
{
    public Animator objectAnimator; // Referência ao Animator do objeto que vai animar (opcional)
    public string triggerName; // Nome do Trigger no Animator (opcional)
    public float shakeDelay = 2.5f; // O tempo de atraso para o camera shake

    public ShakeData DashCameraShake; // Dados do shake da câmera

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Verifica se é o jogador que entrou no trigger
        {
            if (objectAnimator != null && !string.IsNullOrEmpty(triggerName))
            {
                objectAnimator.SetTrigger(triggerName); // Aciona o trigger para iniciar a animação, se necessário
            }

            StartCoroutine(ShakeCameraAfterDelay());
        }
    }

    private IEnumerator ShakeCameraAfterDelay()
    {
        yield return new WaitForSeconds(shakeDelay); // Espera 2.5 segundos (ou o valor que você definir)
        
        // Executa o shake da câmera
        CameraShakerHandler.Shake(DashCameraShake);
    }
}
