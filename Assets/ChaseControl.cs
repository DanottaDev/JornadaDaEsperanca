using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseControl : MonoBehaviour
{
    public FlyingEnemy[] enemyArray;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (FlyingEnemy enemy in enemyArray)
        {
            enemy.chase = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (FlyingEnemy enemy in enemyArray)
        {
            enemy.chase = false;
        }
    }
}
