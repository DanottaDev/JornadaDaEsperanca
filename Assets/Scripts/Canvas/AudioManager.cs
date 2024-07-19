using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private bool estadoSom = true;
    [SerializeField] private AudioSource fundoMusical;
    [SerializeField] private Sprite somLigadoSprite;
    [SerializeField] private Sprite somDesligadoSprite;
    [SerializeField] private Image muteImage;
    public void LigarDesligarSom()
    {
        estadoSom = !estadoSom;
        fundoMusical.enabled = estadoSom;
        
        if (estadoSom)
        {
            muteImage.sprite = somLigadoSprite;
        }
        else
        {
            muteImage.sprite = somDesligadoSprite;
        }
    }

    public void VolumeMusical (float value)
    {
        fundoMusical.volume = value;
    }
}
