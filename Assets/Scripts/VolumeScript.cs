using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        if (GameData.gameData != null && GameData.gameData.LoadModoVisual() == 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
