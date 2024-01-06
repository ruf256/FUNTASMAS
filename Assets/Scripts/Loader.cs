using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{

    public enum Scene
    {
        MainMenu, Endless,
        Obstaculos
    }

    public static void Load(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public static string GetScene()
    {
        return SceneManager.GetActiveScene().name;
    }
}
