using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformY : MonoBehaviour
{
    public int velocidade;
    public Transform pontoA, pontoB;

    Vector2 targetPos;

    void Start ()
    {
        targetPos = pontoB.position;
    }
    void Update()
    {
        if (Vector2.Distance(transform.position, pontoA.position) < .1f) targetPos = pontoB.position;
        
        if (Vector2.Distance(transform.position, pontoB.position) < .1f) targetPos = pontoA.position;

        transform.position = Vector2.MoveTowards(transform.position, targetPos, velocidade * Time.deltaTime);
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform);
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}