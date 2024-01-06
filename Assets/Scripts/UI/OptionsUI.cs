using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] Button mainMenuButton;
    [SerializeField] Button modoButton;
    [SerializeField] Volume globalVolume;
    void Awake()
    {
        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenu.ToString());
        });


    }
}
