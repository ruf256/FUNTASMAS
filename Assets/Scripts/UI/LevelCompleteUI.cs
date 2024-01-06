using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelCompleteUI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI GradoObtenidoText;
    [SerializeField] private TextMeshProUGUI PuntosObtenidoText;
    [SerializeField] private TextMeshProUGUI MovimientosHechosText;


    void Start()
    {
        PlayerEventScript.Instance.LevelComplete += Instance_LevelComplete;
        gameObject.SetActive(false);
    }

    private void Instance_LevelComplete(object sender, EventArgs e)
    {
        PuntosObtenidoText.text = "POINTS: " + gameManager.puntaje.ToString() + "/" + gameManager.puntajeMaximo.ToString();
        MovimientosHechosText.text = "MOVEMENTS: " + gameManager.movimientos.ToString();
        GradoObtenidoText.text = gameManager.grado.ToString();

        gameObject.SetActive(true);
    }
}
