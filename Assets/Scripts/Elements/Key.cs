using UnityEngine;

public class Key : MonoBehaviour
{
    public string keyType; // Tipo de chave (correspondente ao tipo de porta)

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            KeyInventory keyInventory = other.GetComponent<KeyInventory>();
            if (keyInventory != null)
            {
                keyInventory.AddKey(keyType);
                Debug.Log($"Chave {keyType} coletada pelo jogador.");
                Destroy(gameObject); // Destroi a chave após ser coletada
            }
            else
            {
                Debug.LogError("KeyInventory não encontrado no jogador.");
            }
        }
    }
}
