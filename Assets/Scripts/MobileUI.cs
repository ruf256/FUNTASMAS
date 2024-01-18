using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MobileUI : MonoBehaviour
{
    [SerializeField] Image[] botones;
    private void Start()
    {
        if (PlayerEventScript.Instance != null)
        {
            PlayerEventScript.Instance.GameOver += Instance_GameOver;
            PlayerEventScript.Instance.LevelComplete += Instance_LevelComplete;

            foreach (Image item in botones)
            {
                item.color = new Color(0, 0, 0, 0f);
            }
        }
        else
        {
            if (GameData.gameData.LoadModoVisual() == 0)
            {
                foreach (Image item in botones)
                {
                    item.color = new Color(0, 0, 0, 0.5f);
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

    private void Instance_LevelComplete(object sender, System.EventArgs e)
    {
        if (GameData.gameData.LoadModoVisual() == 0)
        {
            foreach (Image item in botones)
            {
                item.color = new Color(0, 0, 0, 0.5f);
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

    private void Instance_GameOver(object sender, System.EventArgs e)
    {
        if (GameData.gameData.LoadModoVisual() == 0)
        {
            foreach (Image item in botones)
            {
                item.color = new Color(0, 0, 0, 0.5f);
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
