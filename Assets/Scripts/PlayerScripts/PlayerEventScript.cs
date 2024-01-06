using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEventScript : MonoBehaviour
{
    public static PlayerEventScript Instance { private set; get; }

    private PlayerMovement playerMovement;

    public event EventHandler SumarPunto;
    public event EventHandler GameOver;
    public event EventHandler LevelComplete;
    public event EventHandler Moviose;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        playerMovement = GetComponentInChildren<PlayerMovement>();
    }


    private void GameOverEvent()
    {
        GameOver?.Invoke(this, EventArgs.Empty);
        if(GameData.gameData != null && GameData.gameData.LoadDesbloqueables() == 0 && GameData.gameData.LoadHighScore() >= 25)
        {
            GameData.gameData.SaveDesbloqueables(1);
        }
    }

    private void SumarPuntoEvent()
    {
        SumarPunto?.Invoke(this, EventArgs.Empty);
        
    }

    private void LevelCompleteEvent()
    {
        LevelComplete?.Invoke(this, EventArgs.Empty);
        if(GameData.gameData != null && GameData.gameData.LoadDesbloqueables() < SceneManager.sceneCountInBuildSettings - 2)
        {
            GameData.gameData.SaveDesbloqueables(GameData.gameData.LoadDesbloqueables() + 1);
        }
        else if (GameData.gameData != null && GameData.gameData.LoadDesbloqueables() == SceneManager.sceneCountInBuildSettings - 2)
        {
            GameData.gameData.SaveChallengeCompletado(1);
        }
    }

    private void MovioseEvent()
    {
        Moviose?.Invoke(this, EventArgs.Empty);
    }
}
