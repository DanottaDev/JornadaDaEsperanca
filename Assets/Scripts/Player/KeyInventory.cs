using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    public bool HasKey(string keyType)
    {
        return keys.ContainsKey(keyType) && keys[keyType] > 0;
    }

    public void UseKey(string keyType)
    {
        if (HasKey(keyType))
        {
            keys[keyType]--;
            if (keys[keyType] == 0)
            {
                keys.Remove(keyType);
            }
        }
    }
}
