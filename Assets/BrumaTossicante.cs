using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrumaTossicante : MonoBehaviour
{
    // Configurações gerais
    public float moveSpeed = 2f;
    public float detectionRadius = 5f;
    public float soundWaveInterval = 3f;
    public float airExplosionRadius = 2f;
    public float airZoneDuration = 5f;

    // Referências
    public GameObject airZonePrefab;
    private Transform player;
    private bool isPlayerInRange;
    private float soundWaveTimer;
    private float airZoneTimer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        soundWaveTimer = soundWaveInterval;
        airZoneTimer = airZoneDuration;
    }

    void Update()
    {
        Move();

        if (isPlayerInRange)
        {
            HandleSoundWaveAttack();
            HandleAirExplosion();
        }

        HandleAirZoneCreation();
    }

    void Move()
    {
        // Movimenta a Bruma de forma simples, você pode adaptar para um movimento mais complexo
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    void HandleSoundWaveAttack()
    {
        soundWaveTimer -= Time.deltaTime;
        if (soundWaveTimer <= 0f)
        {
            EmitSoundWave();
            soundWaveTimer = soundWaveInterval;
        }
    }

    void EmitSoundWave()
    {
        // Simula o ataque de ondas sonoras
        Debug.Log("Bruma Tossicante emitiu uma onda sonora!");
        // Aqui você adiciona o efeito de tosse no player (exemplo: interromper movimentos ou aplicar debuffs)
    }

    void HandleAirExplosion()
    {
        // Verifica a distância do player
        if (Vector3.Distance(transform.position, player.position) <= airExplosionRadius)
        {
            // Aplica o efeito de empurrão no player
            Vector3 pushDirection = (player.position - transform.position).normalized;
            player.GetComponent<Rigidbody2D>().AddForce(pushDirection * 300f); // Adapta a força conforme necessário
            Debug.Log("Player foi empurrado pela explosão de ar!");
        }
    }

    void HandleAirZoneCreation()
    {
        airZoneTimer -= Time.deltaTime;
        if (airZoneTimer <= 0f)
        {
            CreateAirZone();
            airZoneTimer = airZoneDuration;
        }
    }

    void CreateAirZone()
    {
        Instantiate(airZonePrefab, transform.position, Quaternion.identity);
        Debug.Log("Bruma Tossicante criou uma zona de ar rarefeito!");
        // Aqui você pode criar uma zona no ambiente que cause dano contínuo ao player
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}