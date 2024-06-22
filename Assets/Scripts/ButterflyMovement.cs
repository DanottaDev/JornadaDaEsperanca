using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyMovement : MonoBehaviour
{
    public float speed = 2f;             // Velocidade da borboleta
    public float distance = 5f;          // Distância de um lado para o outro
    private Vector3 startPosition;       // Posição inicial da borboleta

    void Start()
    {
        startPosition = transform.position;  // Salva a posição inicial
    }

    void Update()
    {
        // Calcula a nova posição usando uma função seno para movimento suave
        Vector3 newPosition = startPosition;
        newPosition.x += Mathf.Sin(Time.time * speed) * distance;
        transform.position = newPosition;
    }
}