using UnityEngine;
using System.Collections.Generic;

public class KeyInventory : MonoBehaviour
{
    private Dictionary<string, int> keys = new Dictionary<string, int>();

    public void AddKey(string keyType)
    {
        if (keys.ContainsKey(keyType))
        {
            keys[keyType]++;
        }
        else
        {
            keys[keyType] = 1;
        }
        Debug.Log($"Chave coletada: {keyType}. Total de chaves desse tipo: {keys[keyType]}");
    }

    public bool HasKey(string keyType)
    {
        bool hasKey = keys.ContainsKey(keyType) && keys[keyType] > 0;
        Debug.Log($"Verificando se o jogador tem a chave {keyType}: {hasKey}");
        return hasKey;
    }

    public void UseKey(string keyType)
    {
        if (HasKey(keyType))
        {
            keys[keyType]--;
            Debug.Log($"Chave usada: {keyType}. Chaves restantes desse tipo: {keys[keyType]}");
            if (keys[keyType] == 0)
            {
                keys.Remove(keyType);
                Debug.Log($"Todas as chaves do tipo {keyType} foram usadas.");
            }
        }
    }
}
