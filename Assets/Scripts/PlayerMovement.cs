using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        // Captura as entradas do teclado
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calcula o movimento
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Aplica o movimento ao personagem
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
    }
}
