using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public HeartSystem heartSystem;
    public PlayerController playerController;
    public int damage;
    public Animator animator;
    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Player"))
        {
            heartSystem.vida -= damage;
            animator.SetTrigger("isDamage");
        }
    }
}