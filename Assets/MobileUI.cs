using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileUI : MonoBehaviour
{
    [SerializeField] Image[] botones;
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
