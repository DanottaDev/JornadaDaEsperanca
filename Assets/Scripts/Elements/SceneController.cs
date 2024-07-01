using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    [SerializeField] Animator transitionAnim;
    public GameObject tutorialCanvas;  // Adicionado: referência ao Canvas do tutorial
    public float tutorialDisplayTime = 5f;  // Tempo que o tutorial será exibido

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
    }

    IEnumerator LoadLevel(int sceneIndex)
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(sceneIndex);
        transitionAnim.SetTrigger("Start");
        
        // Aguarde até a nova cena ser carregada
        yield return new WaitForSeconds(1);

        // Exiba o Canvas do tutorial
        ShowTutorialCanvas();
    }

    IEnumerator LoadLevel(string sceneName)
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(sceneName);
        transitionAnim.SetTrigger("Start");
        
        // Aguarde até a nova cena ser carregada
        yield return new WaitForSeconds(1);

        // Exiba o Canvas do tutorial
        ShowTutorialCanvas();
    }

    void ShowTutorialCanvas()
    {
        if (tutorialCanvas != null)
        {
            tutorialCanvas.SetActive(true);
            StartCoroutine(DisableCanvasAfterTime(tutorialDisplayTime));
        }
    }

    IEnumerator DisableCanvasAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        if (tutorialCanvas != null)
        {
            tutorialCanvas.SetActive(false);
        }
    }
}
