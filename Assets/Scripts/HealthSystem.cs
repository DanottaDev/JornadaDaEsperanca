using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartSystem : MonoBehaviour
{

    public int vida;
    public int vidaMaxima;
    public Image[] coracao;
    public Sprite cheio;
    public Sprite vazio;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        morrer();
        HealthVogic();
    }

    void HealthVogic(){
        for (int i = 0; i < coracao.Length; i++)
        {

            if(i < vida)
            {
                coracao[i].sprite = cheio;
            } else
            {
                coracao[i].sprite = vazio;
            }

            if(i < vidaMaxima)
            {
                coracao[i].enabled = true;
            } else
            {
                coracao[i].enabled = false;
            }

        }
    }

    void morrer(){
        if(vida <= 0){
            Debug.Log("morreu");
        }
    }

}