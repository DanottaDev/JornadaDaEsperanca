using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public string requiredKey; // Tipo de chave necessária para abrir a porta

    private Animator animator;
    private Collider2D doorCollider;
    private bool isOpen = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        doorCollider = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Colisão detectada."); // Verifica se a colisão está sendo detectada

        if (other.CompareTag("Player"))
        {
            Debug.Log("O jogador colidiu com a porta.");

            KeyInventory keyInventory = other.GetComponent<KeyInventory>();
            if (keyInventory != null)
            {
                if (keyInventory.HasKey(requiredKey))
                {
                    Debug.Log("Chave correta encontrada! Abrindo a porta.");
                    OpenDoor();
                    keyInventory.UseKey(requiredKey); // Remove a chave do inventário do jogador
                }
                else
                {
                    Debug.LogWarning($"O jogador não tem a chave necessária ({requiredKey}) para abrir esta porta.");
                }
            }
            else
            {
                Debug.LogError("KeyInventory não encontrado no jogador.");
            }
        }
    }

    void OpenDoor()
    {
        if (!isOpen)
        {
            isOpen = true;
            Debug.Log("Porta aberta.");
            animator.SetTrigger("Open");
            doorCollider.enabled = false; // Desativa o colisor da porta
        }
    }
}
