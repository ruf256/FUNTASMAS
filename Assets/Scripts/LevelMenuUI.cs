using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelMenuUI : MonoBehaviour
{   
    public void HideAndShow()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
