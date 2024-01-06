using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntoPulsoAnimacion : MonoBehaviour
{
    [SerializeField] Vector3 escalaInicial;
    private float timer;
    private bool creciendo;

    void Awake()
    {
        transform.localScale = Vector2.zero;
        creciendo = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(creciendo)
        {
            transform.localScale += new Vector3(Time.deltaTime / 2, Time.deltaTime / 2 , 0);
            if(transform.localScale.x >= escalaInicial.x) creciendo = false;
        }
        else if (!creciendo)
        {
            transform.localScale -= new Vector3(Time.deltaTime / 2, Time.deltaTime / 2, 0);
            if(transform.localScale.x <= 0.25) creciendo = true;
        }
    }
}
