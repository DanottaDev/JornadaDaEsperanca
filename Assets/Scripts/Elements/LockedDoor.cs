using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public string requiredKey; // Tipo de chave necessária para abrir a porta

    // private Animator animator;
    private bool isOpen = false;

    void Start()
    {
        // animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            KeyInventory keyInventory = other.GetComponent<KeyInventory>();
            if (keyInventory != null && keyInventory.HasKey(requiredKey))
            {
                OpenDoor();
                keyInventory.UseKey(requiredKey); // Remove a chave do inventário do jogador
            }
        }
    }

    void OpenDoor()
    {
        if (!isOpen)
        {
            isOpen = true;
            // animator.SetTrigger("Open"); // Assume que você tenha uma animação de abertura de porta
            // Alternativamente, você pode simplesmente desativar o colisor da porta:
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
