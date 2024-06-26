using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]public AudioSource musicSource;

    public void VolumeMusical (float value)
    {
        musicSource.volume = value;
    }
}
