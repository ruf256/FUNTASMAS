using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject fantasma;
    [SerializeField] private GameObject punto;
    
    [SerializeField] private float spawnCooldown;
    [SerializeField] private float velocidadFantasmasChallenge;

    private bool endlessP;
    private GameObject movJugador;
    private Transform puntoVictoria;

    private CircleCollider2D[] puntos;
    public int movimientos { private set; get; }
    public int puntaje { private set; get; }
    public char grado { private set; get; }
    int valorGrado;

    public int puntajeMaximo { private set; get; }

    public int movimientosMaximos { private set; get; }
    void Start()
    {

        PlayerEventScript.Instance.SumarPunto += Instance_SumarPuntaje;
        PlayerEventScript.Instance.Moviose += Instance_Moviose;
        PlayerEventScript.Instance.LevelComplete += Instance_LevelComplete;
        endlessP = Loader.GetScene().StartsWith("End");
        movJugador = FindAnyObjectByType<PlayerMovement>().gameObject;
        if(!endlessP)
        puntoVictoria = GameObject.FindGameObjectWithTag("PuntoVictoria").transform;

        if (endlessP)
        {
            SpawnPunto(transform.position);
        } else
        {
            CircleCollider2D[] puntos = FindObjectsOfType<CircleCollider2D>();
            puntajeMaximo = puntos.Count();
            movimientosMaximos = Mathf.CeilToInt(puntajeMaximo * 2.25f);
        }
    }

    private void Instance_LevelComplete(object sender, System.EventArgs e)
    {
        if(GameData.gameData != null && GameData.gameData.LoadChallengeGradeValor(SceneManager.GetActiveScene().name) < valorGrado)
        {
            GameData.gameData.SaveNivelChallengeGrade(SceneManager.GetActiveScene().name, grado, valorGrado);
        }
        
    }

    private void Instance_Moviose(object sender, System.EventArgs e)
    {
       movimientos++;
    }

    private void Instance_SumarPuntaje(object sender, System.EventArgs e)
    {
        puntaje++;
        float x = Random.Range(-8.7f, 8.7f);
        float y = Random.Range(-4.7f, 4.7f);
        Vector3 pos = new Vector3(x, y, 0);
        if(endlessP) SpawnPunto(pos);
        SpawnearFantasma(pos);

        if (puntaje == puntajeMaximo && movimientos <= movimientosMaximos)
        {
            grado = 'S';
            valorGrado = 10;
        }
        else if (puntaje > Mathf.CeilToInt(puntajeMaximo / 2) && movimientos <= movimientosMaximos)
        {
            grado = 'A';
            valorGrado = 9;
        }
        else if (puntaje > Mathf.CeilToInt(puntajeMaximo / 2) && movimientos > movimientosMaximos)
        {
            grado = 'B';
            valorGrado = 7;
        }
        else if (puntaje > Mathf.CeilToInt(puntajeMaximo / 3) && movimientos <= movimientosMaximos)
        {
            grado = 'B';
            valorGrado = 7;
        }
        else if (puntaje < Mathf.FloorToInt(puntajeMaximo / 3) && movimientos > movimientosMaximos)
        {
            grado = 'C';
            valorGrado = 3;
        }
        else
        {
            grado = 'F';
            valorGrado = 1;
        }

        if (GameData.gameData != null && GameData.gameData.LoadHighScore() < puntaje)
        {
            GameData.gameData.SaveHighScore(puntaje);
        }
    }

    private void SpawnPunto(Vector3 pos)
    {
        GameObject pun = Instantiate(punto, pos, Quaternion.identity);
    }

    WaitForSeconds SpawnearFantasma(Vector3 punto)
    {
        if (endlessP)
        {
            GameObject fanta = Instantiate(fantasma, punto, Quaternion.identity);
            fanta.name = "FantasmaMalo";
            
        } else 
        {
            GameObject fanta = Instantiate(fantasma, puntoVictoria.position + new Vector3(Random.Range(-3,3f), Random.Range(-2f,2f)), Quaternion.identity);
            FantasmaScript f = fanta.GetComponent<FantasmaScript>();
            fanta.name = "FantasmaMalo";
            f.velocidad = velocidadFantasmasChallenge;
            f.fueraDeCam = true;
            
        }

        return new WaitForSeconds(2.5f);
    }
}
