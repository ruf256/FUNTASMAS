using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndlessMusica : MonoBehaviour
{
    [HideInInspector] public static EndlessMusica eM { private set; get; }


    private void Awake()
    {
        if (eM == null)
        {
            eM = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (eM != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Destroy(gameObject);
        }
    }

}
