using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed = 2f; // Velocidade de movimento da plataforma
    public float moveDistance = 5f; // Distância que a plataforma irá se mover
    public float disappearDelay = 2f; // Tempo até a plataforma desaparecer

    private bool isActivated = false;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private float startTime;

    void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition + Vector3.up * moveDistance;
    }

    void Update()
    {
        if (isActivated)
        {
            float distCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distCovered / moveDistance;
            transform.position = Vector3.Lerp(initialPosition, targetPosition, fractionOfJourney);

            if (fractionOfJourney >= 1.0f)
            {
                StartCoroutine(DisappearAfterDelay());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isActivated)
        {
            isActivated = true;
            startTime = Time.time;
        }
    }

    private IEnumerator DisappearAfterDelay()
    {
        yield return new WaitForSeconds(disappearDelay);
        Destroy(gameObject);
    }
}
