using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI endlessHsText;
    private void Start()
    {
        gameObject.SetActive(false);
        PlayerEventScript.Instance.GameOver += Instance_GameOver;
    }

    private void Instance_GameOver(object sender, EventArgs e)
    {
        if (SceneManager.GetActiveScene().name.StartsWith("End"))
        {
           endlessHsText.text = "HIGHSCORE: " + GameData.gameData.LoadHighScore();
        }
        gameObject.SetActive(true);
    }

}
