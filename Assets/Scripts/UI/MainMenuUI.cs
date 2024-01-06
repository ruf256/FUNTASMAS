using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] GameData gd;
    [SerializeField] GameObject candado;
    [SerializeField] TextMeshProUGUI nivelChallengeCount;
    [SerializeField] TextMeshProUGUI endlessHS;
    void Awake()
    {
        if (gd.LoadDesbloqueables() != 0)
        {
            Destroy(candado);
            nivelChallengeCount.text = gd.LoadDesbloqueables() + " / " + (SceneManager.sceneCountInBuildSettings - 2);
        }

        if(gd.LoadHighScore() > 0)
        {
            endlessHS.text = "HighScore: " + gd.LoadHighScore();
        }
        else
        {
            endlessHS.text = "HighScore: 0";
        }
    }
    public void HideAndShow()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
