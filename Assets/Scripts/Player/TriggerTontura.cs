using System.Collections;
using UnityEngine;
using FirstGearGames.SmoothCameraShaker; // Certifique-se de que está importando o Smooth Camera Shaker

public class TonturaShakeTrigger : MonoBehaviour
{
    public float shakeDelay = 2.5f; // Tempo de atraso antes do shake

    public ShakeData TonturaCameraShake; // Dados do shake para a tontura

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Verifica se o player entrou no trigger
        {
            // Inicia a coroutine que vai causar o shake da câmera após o delay
            StartCoroutine(ShakeCameraAfterDelay());
        }
    }

    private IEnumerator ShakeCameraAfterDelay()
    {
        yield return new WaitForSeconds(shakeDelay); // Aguarda o tempo definido para iniciar o shake

        // Executa o camera shake com os dados de tontura
        CameraShakerHandler.Shake(TonturaCameraShake);
    }
}
