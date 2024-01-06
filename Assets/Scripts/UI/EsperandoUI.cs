using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EsperandoUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmp;
    void Start()
    {
        tmp.text = "READY?";
    }

    // Update is called once per frame
    void Update()
    {
        if (!StateManager.Instance.isConteoActivo() && !StateManager.Instance.isEsperandoParaEmpezar())
        {
            gameObject.SetActive(false); 
            return;
        }
        if (StateManager.Instance.GetConteoParaEmpezar() < 1f)
        {
                tmp.text = "GO!";
                tmp.color = new Color(0, 0, 0, tmp.color.a - Time.deltaTime);
        }

    }
}
