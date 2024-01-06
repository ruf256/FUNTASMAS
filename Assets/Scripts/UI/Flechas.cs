using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Flechas : MonoBehaviour
{
    PlayerInputActions playerInputActions;
    float fill;
    float jInput;
    Scene escena;

    private Image fillIzq;
    private Image fillDer;

    [SerializeField] private Image candado;
    [SerializeField] private Image luzImagen;

    [SerializeField] private Image sonidoImagen;

    [SerializeField] private Sprite[] sonidosSprite;

    [SerializeField] private Sprite lightMode;
    [SerializeField] private Sprite darkMode;

    [SerializeField] private LevelMenuUI levelMenuUI;
    [SerializeField] private MainMenuUI mainMenuUI;

    [SerializeField] RectTransform indicador;
    [SerializeField] RectTransform[] niveles;
    [SerializeField] TextMeshProUGUI rank;
    public float currentLevel;

    private Vector3 initScaleCandado;

    Volume vol;

    private string escenaIzq;
    private string escenaDer;
    private void Awake()
    {
        vol = FindAnyObjectByType<VolumeScript>().GetComponent<Volume>();
        Image[] fills = GetComponentsInChildren<Image>();

        foreach (Image fil in fills)
        {
            if (fil.name == "FillDer")
            {
                fillDer = fil;
            }
            else if (fil.name == "FillIzq")
            {
                fillIzq = fil;
            }
        }



    }
    private void Start()
    {
        
        if (candado) initScaleCandado = candado.rectTransform.localScale;

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Movimiento.started += Movimiento_started;
        playerInputActions.Player.Movimiento.canceled += Movimiento_canceled;
        playerInputActions.Player.Movimiento.performed += Movimiento_performed;

        if (indicador != null && niveles.Length > 0)
        {
            indicador.position = new Vector2(niveles[0].position.x, niveles[0].position.y - 1.5f);
            currentLevel = 0;
        }


        escena = SceneManager.GetActiveScene();
        if (escena.name.StartsWith("M"))
        {
            
            escenaIzq = "Obstaculos" + GameData.gameData.LoadDesbloqueables();
            escenaDer = "Endless";
           

        }
        else if (escena.name.StartsWith("E"))
        {
            escenaIzq = "MainMenu";
            escenaDer = "Endless";
        }
        else
        {
            escenaIzq = "MainMenu";
            if(SceneManager.GetActiveScene().buildIndex != SceneManager.sceneCountInBuildSettings - 2)
            escenaDer = "Obstaculos" + (escena.buildIndex + 1);
            else
            {
                escenaDer = "MainMenu";
            }
        }

        if (GameData.gameData != null && GameData.gameData.LoadModoVisual() == 0)
        {
            luzImagen.sprite = darkMode;
        }
        else
        {
            luzImagen.sprite = lightMode;
        }

        if (GameData.gameData != null && GameData.gameData.LoadMasterVolumen() == -9f)
        {
            sonidoImagen.sprite = sonidosSprite[0];
        }
        else if (GameData.gameData != null && GameData.gameData.LoadMasterVolumen() == -15f)
        {
            sonidoImagen.sprite = sonidosSprite[1];
        }
        else
        {
            sonidoImagen.sprite = sonidosSprite[2];
        }


    }

    private void Movimiento_started(InputAction.CallbackContext context)
    {
        jInput = playerInputActions.Player.Movimiento.ReadValue<float>();
    }

    private void Movimiento_performed(InputAction.CallbackContext obj)
    {
        jInput = playerInputActions.Player.Movimiento.ReadValue<float>();
        if (this == null || !gameObject.activeInHierarchy) return;
        StartCoroutine("CargarIndice", jInput);
    }

    private void OnEnable()
    {
        fillDer.fillClockwise = true;
        fillIzq.fillClockwise = false;
        fillDer.fillAmount = 0;
        fillIzq.fillAmount = 0;
    }

    private void Update()
    {
        if (StateManager.Instance != null && StateManager.Instance.isGameOver())
        {
            escenaDer = SceneManager.GetActiveScene().name;
        }
        else if (StateManager.Instance != null && StateManager.Instance.isNivelCompletado() && SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1).IsValid())
        {
            escenaDer = "Obstaculos" + (SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (StateManager.Instance != null && StateManager.Instance.isNivelCompletado() && GameData.gameData.LoadChallengeCompletado() == 1)
        {
            escenaDer = "MainMenu";
        }
        else if (transform.parent.name == "LevelMenuUI")
        {
            escenaDer = "Obstaculos" + (currentLevel + 1).ToString();
        }
    }


    private void Movimiento_canceled(InputAction.CallbackContext context)
    {
        if (this == null || !gameObject.activeInHierarchy || Time.timeSinceLevelLoad < 0.5f) return;
        fill = 0;
        fillDer.fillAmount = 0;
        fillIzq.fillAmount = 0;
        if (candado) candado.rectTransform.localScale = initScaleCandado;

        if (context.duration < 0.15)
        {

            if (levelMenuUI != null && levelMenuUI.gameObject.activeSelf)
            {
                CambiarLevel(jInput);
            }
            else
            {
                if (jInput == 1)
                {
                    float m = GameData.gameData.LoadMasterVolumen();
                    SoundData.soundData.ChangeMasterVolumen();


                    if (m == -9f)
                    {
                        sonidoImagen.sprite = sonidosSprite[1];
                    }
                    else if (m == -15f)
                    {
                        sonidoImagen.sprite = sonidosSprite[2];
                    }
                    else
                    {
                        sonidoImagen.sprite = sonidosSprite[0];
                    }
                }
                else
                {
                    ToggleLightMode();
                }
            }

        }

        jInput = playerInputActions.Player.Movimiento.ReadValue<float>();
    }
     IEnumerator CargarIndice(float x)
     {
            if (playerInputActions != null && fillDer != null && fillIzq != null)
            {
                while (jInput != 0)
                {
                    if (x > 0.15f)
                    {
                        fill += x * Time.deltaTime;
                        fillDer.fillAmount = fill;
                        if (fill >= 1)
                        {
                            Loader.Load(escenaDer);
                        }
                    }
                    else if (x < -0.15f)
                    {
                        if (candado)
                        {
                            candado.transform.localScale = new Vector3(32.5f, 32.5f, 0);
                        }
                        else
                        {
                            fill += -x * Time.deltaTime;
                            fillIzq.fillAmount = fill;
                            if (fill >= 1 && GameData.gameData.LoadChallengeCompletado() == 1 && SceneManager.GetActiveScene().name == "MainMenu")
                            {
                                if (!levelMenuUI.gameObject.activeSelf)
                                {
                                    levelMenuUI.HideAndShow();
                                    mainMenuUI.HideAndShow();
                                }
                                else
                                {
                                    levelMenuUI.HideAndShow();
                                    mainMenuUI.HideAndShow();
                                }
                                
                            }
                            else if (fill >= 1)
                            {
                                Loader.Load(escenaIzq);
                            }
                        }
                    }
                    yield return null;
                }
            }
     }
        


    private void ToggleLightMode()
    {
            vol.gameObject.SetActive(!vol.gameObject.activeSelf);
            if (GameData.gameData != null && !vol.gameObject.activeSelf)
            {
                luzImagen.sprite = darkMode;
                GameData.gameData.SaveModoVisual(0);
            }
            else if (GameData.gameData != null)
            {
                luzImagen.sprite = lightMode;
                GameData.gameData.SaveModoVisual(1);
            }
    }

    void CambiarLevel(float x)
    {
            if (currentLevel == 0 && x == -1) currentLevel = niveles.Length;
            else if (currentLevel == niveles.Length - 1 && x == 1) { currentLevel = 0; x = 0; }
            currentLevel += x;
            indicador.position = new Vector2(niveles[(int)(currentLevel)].position.x, niveles[0].position.y - 1.5f);

            rank.text = GameData.gameData.LoadChallengeGrade("Obstaculos" + (currentLevel + 1).ToString());

    }
}


