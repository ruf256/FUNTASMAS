using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CamaraLimiteScript : MonoBehaviour
{
    [SerializeField] Transform[] limitesX;
    void Start()
    {
        float aspect = Camera.main.aspect;
        if (aspect > 2f && limitesX.Length != 0)
        {
            limitesX[0].position = new Vector3(limitesX[0].position.x - (aspect *1.5f - 1.7f), limitesX[0].position.y, limitesX[0].position.z);
            limitesX[1].position = new Vector3(limitesX[1].position.x + (aspect *1.5f - 1.7f), limitesX[1].position.y, limitesX[1].position.z);
        }
        
    }


}
