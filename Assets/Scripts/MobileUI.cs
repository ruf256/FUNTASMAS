using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MobileUI : MonoBehaviour
{
    [SerializeField] Image[] botones;


    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0) return; 
        if (GameData.gameData.LoadModoVisual() == 0)
        {
            foreach (Image item in botones) 
            { 
                item.color = new Color(0,0,0,0.5f);
            }
        }
        else
        {
            foreach (Image item in botones)
            {
                item.color = new Color(0, 0, 0, 0.05f);
            }
        }

    }

}
