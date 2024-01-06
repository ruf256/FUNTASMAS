using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialEndlessUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI primerText;
    [SerializeField] private TextMeshProUGUI segundoText;
    private int contador = 0;

    void Start()
    {
        primerText.color = new Color(0, 0, 0, 0);
        segundoText.color = new Color(0, 0, 0, 0);
        PlayerEventScript.Instance.SumarPunto += Instance_SumarPunto;
    }

    private void Instance_SumarPunto(object sender, System.EventArgs e)
    {
        if(contador == 0)
        {
            StartCoroutine(AnimarOpacidad(primerText));
        }else if (contador == 1)
        {
            StartCoroutine(AnimarOpacidad(segundoText));
        }
        else if (contador == 4)
        {
            StartCoroutine(AnimarOpacidad(primerText));
            StartCoroutine(AnimarOpacidad(segundoText));
        }

        contador++;
    }

    IEnumerator AnimarOpacidad(TextMeshProUGUI text)
    {
        //float transparencia = text.color.a;
        if(text.color.a == 0)
        {
            while(text.color.a != 1)
            {
                text.color = new Color(0, 0, 0, Mathf.Lerp(text.color.a, 1, Time.deltaTime));
                
                yield return null;
            }
        }
        else
        {
            while(text.color.a != -2)
            {
                
                text.color = new Color(0, 0, 0, Mathf.Lerp(text.color.a, -2, Time.deltaTime));

                yield return null;
            }
           
        }
    }
}
