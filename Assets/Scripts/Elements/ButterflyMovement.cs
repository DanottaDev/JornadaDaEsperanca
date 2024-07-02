using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyMovement : MonoBehaviour
{
    public float speed = 2f;           // Velocidade da borboleta
    public float radius = 5f;          // Raio do movimento circular
    private Vector3 startPosition;     // Posição inicial da borboleta

    void Start()
    {
        startPosition = transform.position;  // Salva a posição inicial
    }

    void Update()
    {
        // Calcula a nova posição usando funções seno e cosseno para movimento circular
        Vector3 newPosition = startPosition;
        newPosition.x += Mathf.Cos(Time.time * speed) * radius;
        newPosition.y += Mathf.Sin(Time.time * speed) * radius;
        transform.position = newPosition;
    }
}
