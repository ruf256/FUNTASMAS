using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] GameManager gameManager;

    // Update is called once per frame
    void Update()
    {
        textMeshPro.text = gameManager.puntaje.ToString();
    }
}
