using System.Collections;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class NebulosaArea : MonoBehaviour
{
    public Vector2[] spawnPositions;      // Posições pré-determinadas onde a nebulosa pode aparecer
    public GameObject airZonePrefab;      // Prefab da área nebulosa
    public float spawnIntervalMin = 5f;   // Intervalo mínimo entre spawns
    public float spawnIntervalMax = 10f;  // Intervalo máximo entre spawns

    public float airZoneDuration = 4f;    // Duração da nebulosa (em segundos)

    private void Start()
    {
        // Inicia a corrotina de spawn aleatório da nebulosa
        StartCoroutine(StartSpawning());
    }

    // Corrotina que spawna a nebulosa em intervalos aleatórios
    IEnumerator StartSpawning()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(spawnIntervalMin, spawnIntervalMax));
            StartCoroutine(SpawnNebulosa());
        }
    }

    // Função que instancia a nebulosa em uma posição aleatória e a destrói após um tempo
    IEnumerator SpawnNebulosa()
    {
        int randomIndex = Random.Range(0, spawnPositions.Length);
        GameObject airZone = Instantiate(airZonePrefab, spawnPositions[randomIndex], Quaternion.identity);
        
        // Aguarda o tempo de duração da nebulosa
        yield return new WaitForSeconds(airZoneDuration);

        // Destroi a nebulosa após o tempo determinado
        Destroy(airZone);
    }
}
