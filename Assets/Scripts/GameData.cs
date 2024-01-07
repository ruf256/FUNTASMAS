using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    [HideInInspector] public static GameData gameData { private set; get; }

    void Awake()
    {
        Application.targetFrameRate = 60;


        if (gameData == null)
        {
            DontDestroyOnLoad(gameObject);
            gameData = this;
        }
        else if (gameData != this)
        {
            Destroy(gameObject);
        }

        if (!PlayerPrefs.HasKey("Desbloqueables")) SaveDesbloqueables(0);
        if (!PlayerPrefs.HasKey("EndlessHighScore")) SaveHighScore(0);
        if (!PlayerPrefs.HasKey("ChallengeCompletado")) SaveChallengeCompletado(0);
        if (!PlayerPrefs.HasKey("MasterVolumen")) PlayerPrefs.SetFloat("MasterVolumen", -9f);

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        Screen.fullScreen = !Screen.fullScreen;
    }


    public void SaveHighScore(int score)
    {
        PlayerPrefs.SetInt("EndlessHighScore", score);
    }

    public int LoadHighScore()
    {     
        return PlayerPrefs.GetInt("EndlessHighScore");
    }

    public void SaveModoVisual(int modo)
    {
        //modo 0 = claro 1 = oscuro
        PlayerPrefs.SetInt("Modo", modo);
    }

    public int LoadModoVisual()
    {
        return PlayerPrefs.GetInt("Modo");
    }

    public void SaveDesbloqueables(int n)
    {
        //0 = nada, 1 = obstaculos1, 2 = obstaculos2, ...
        PlayerPrefs.SetInt("Desbloqueables", n);
    }

    public int LoadDesbloqueables()
    {
        return PlayerPrefs.GetInt("Desbloqueables");
    }

    public void SaveNivelChallengeGrade(string chg, char a, int valorGrado)
    {
        PlayerPrefs.SetString(chg, a.ToString());
        PlayerPrefs.SetInt(chg + "V", valorGrado);
    }

    public string LoadChallengeGrade(string chg)
    {
        return PlayerPrefs.GetString(chg);
    }

    public int LoadChallengeGradeValor(string chg)
    {
        return PlayerPrefs.GetInt(chg + "V");
    }

    public void SaveChallengeCompletado(int b)
    {
        PlayerPrefs.SetInt("ChallengeCompletado", b);
    }

    public int LoadChallengeCompletado()
    {
        return PlayerPrefs.GetInt("ChallengeCompletado");
    }

    public void SaveMasterVolumen(float a)
    {
        PlayerPrefs.SetFloat("MasterVolumen", a);
    }

    public float LoadMasterVolumen()
    {
        return PlayerPrefs.GetFloat("MasterVolumen");
    }
}
