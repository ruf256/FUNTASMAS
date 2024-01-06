using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance { get; private set; }

    public EventHandler CambioEstado;

    private float esperandoParaEmpezarTimer = 0;
    private float contandoParaEmpezarTimer = 3;

    enum State
    {
        EsperandoParaEmpezar,
        ContandoParaEmpezar,
        Jugando,
        NivelCompletado,
        GameOver,
    }
    private State state;
    private bool nivelCompletado = false;
    private bool gameOver = false;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayerEventScript.Instance.GameOver += Instance_GameOver;
        PlayerEventScript.Instance.LevelComplete += Instance_NivelCompletado;

        if (Loader.GetScene().StartsWith("E")) state = State.Jugando;
        else
        {
            state = State.EsperandoParaEmpezar;
        }
    }

    private void Instance_NivelCompletado(object sender, EventArgs e)
    {
        nivelCompletado = true;
    }

    private void Instance_GameOver(object sender, EventArgs e)
    {
        gameOver = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.EsperandoParaEmpezar:
                esperandoParaEmpezarTimer -= Time.deltaTime;
                if (esperandoParaEmpezarTimer < 0f)
                {
                    state = State.ContandoParaEmpezar;
                    CambioEstado?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.ContandoParaEmpezar:
                contandoParaEmpezarTimer -= Time.deltaTime;
                if (contandoParaEmpezarTimer < 0)
                {
                    state = State.Jugando;
                    CambioEstado?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.Jugando:
                if (gameOver) state = State.GameOver;
                else if (nivelCompletado) state = State.NivelCompletado;
                break;
            case State.NivelCompletado:
                break;
            case State.GameOver:
                break;
        }
       // Debug.Log(state);
    }

    public bool isGameOver()
    {
        return state == State.GameOver;
    }

    public bool isJugando()
    {
        return state == State.Jugando;
    }

    public bool isNivelCompletado()
    {
        return state == State.NivelCompletado;
    }

    public bool isEsperandoParaEmpezar()
    {
        return state == State.EsperandoParaEmpezar;
    }

    public bool isConteoActivo()
    {
        return state == State.ContandoParaEmpezar;
    }

    public float GetConteoParaEmpezar()
    {
        return contandoParaEmpezarTimer;
    }
}
