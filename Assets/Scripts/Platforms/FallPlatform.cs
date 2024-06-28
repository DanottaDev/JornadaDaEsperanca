using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FirstGearGames.SmoothCameraShaker;

public class FallPlatform : MonoBehaviour
{   
    Rigidbody2D rb;
    Vector2 defaultPos;
    public ShakeData CameraShake;

    [SerializeField] float fallDelay, respawnTime;

    void Start()
    {
        defaultPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    if (collision.gameObject.tag == "Player")
    {   
        CameraShakerHandler.Shake(CameraShake);
        StartCoroutine("PlatformDrop");
        }
    }
    IEnumerator PlatformDrop()
    {
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(respawnTime);
        Reset();
    }
        private void Reset ()
        {
            rb.bodyType = RigidbodyType2D.Static;
            transform.position = defaultPos;
        }
}