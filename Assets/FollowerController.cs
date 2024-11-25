using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : MonoBehaviour
{
    public Transform leader; // O Transform do personagem líder
    public float followSpeed = 5f; // Velocidade de ajuste do movimento
    public float distanceOffset = 1f; // Distância entre o seguidor e o líder
    public Vector3 positionOffset; // Offset de posição (opcional, para alinhar melhor)

    private Vector3 targetPosition;

    void Update()
    {
        if (leader != null)
        {
            // Calcula a posição alvo com base no líder
            targetPosition = leader.position - leader.forward * distanceOffset + positionOffset;

            // Interpola suavemente para a posição alvo
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

            // Rotaciona para manter a direção
            transform.rotation = Quaternion.Lerp(transform.rotation, leader.rotation, followSpeed * Time.deltaTime);
        
            float distanceToLeader = Vector3.Distance(transform.position, leader.position);

            if (distanceToLeader > 10f) // Distância máxima permitida
            {
                transform.position = leader.position + positionOffset;
            }
            else
            {
                targetPosition = leader.position - leader.forward * distanceOffset + positionOffset;
                transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
            }

            transform.rotation = Quaternion.Lerp(transform.rotation, leader.rotation, followSpeed * Time.deltaTime);
        }
    }
}
