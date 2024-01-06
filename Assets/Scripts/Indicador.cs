using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicador : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] sprites;
    float timer;
    int i = 1;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0.5f)
        {
            spriteRenderer.sprite = sprites[i];
            timer = 0;
            if(i == 2)
            {
                i = 0;
            }
            else
            {
                i++;
            }
        }

        timer += Time.deltaTime;

    }
}
