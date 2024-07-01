using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public GameObject tutorialCanvas;  // Referência ao Canvas do tutorial
    public float displayTime = 5f;     // Tempo em segundos que o tutorial será exibido

    void Start()
    {
        // Certifique-se de que o Canvas está ativo quando o jogo começa
        tutorialCanvas.SetActive(true);

        // Inicie a coroutine para desativar o Canvas após alguns segundos
        StartCoroutine(DisableCanvasAfterTime(displayTime));
    }

    IEnumerator DisableCanvasAfterTime(float time)
    {
        // Aguarde pelo tempo especificado
        yield return new WaitForSeconds(time);

        // Desative o Canvas
        tutorialCanvas.SetActive(false);
    }
}
