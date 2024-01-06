using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChallengeScript : MonoBehaviour
{
    [SerializeField] private List<Transform> cameraWayPoints;
    [SerializeField] private float velocidad;
    private float scaleFactor = 3;

    float velocidadInicial;

    private int currentWaypoint = 0;

    private void Start()
    {
        PlayerEventScript.Instance.GameOver += Instance_GameOver;
        velocidadInicial = velocidad;
        scaleFactor /= 10;
    }

    private void Instance_GameOver(object sender, EventArgs e)
    {
        StartCoroutine(ZoomCamara(4));
        Vector3 jugadorPos = FindAnyObjectByType<PlayerColissionScript>().transform.position;
        StartCoroutine(EnfocarJugador(new Vector3(jugadorPos.x, jugadorPos.y, -10)));
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!StateManager.Instance.isJugando())
        {
            return;
        }
            
        transform.position = Vector3.MoveTowards(transform.position, cameraWayPoints[currentWaypoint].position, velocidad * Time.deltaTime);

        if(Vector3.Distance(transform.position, cameraWayPoints[currentWaypoint].position) == 0)
        {
            
            if (currentWaypoint == cameraWayPoints.Count - 1)
            {

            }
            else
            {
                if (cameraWayPoints[currentWaypoint].localScale.x >= 1)
                {
                    StartCoroutine("ZoomCamara", cameraWayPoints[currentWaypoint].localScale.x + 5);
                }
                else if (cameraWayPoints[currentWaypoint].localScale.x < 1 && Camera.main.orthographicSize != 5)
                {
                    StartCoroutine("ZoomCamara", 5);
                }

                if (cameraWayPoints[currentWaypoint].localScale.y != 0)
                {
                    StartCoroutine("CambiarVelocidadCamara", cameraWayPoints[currentWaypoint].localScale.y );
                }

                currentWaypoint++;
            }
           
        }
        
    }

    IEnumerator ZoomCamara(float scala)
    {
        while (Camera.main.orthographicSize < scala)
        {
            Camera.main.orthographicSize += 2 * Time.deltaTime;
            gameObject.transform.localScale += new Vector3(Time.deltaTime * scaleFactor, Time.deltaTime * scaleFactor, 1);
            if (Camera.main.orthographicSize > scala) Camera.main.orthographicSize = scala;
            yield return null;
        }
        while(Camera.main.orthographicSize > scala)
        {
            Camera.main.orthographicSize -= 2 * Time.deltaTime;
            gameObject.transform.localScale -= new Vector3(Time.deltaTime * scaleFactor, Time.deltaTime * scaleFactor, 1);
            if (Camera.main.orthographicSize < scala) Camera.main.orthographicSize = scala;
            yield return null;
        }

    }

    IEnumerator CambiarVelocidadCamara(float y)
    {
        float veli = velocidad;
        while (velocidad < veli + y)
        {
            velocidad += y * Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator EnfocarJugador(Vector3 jug)
    {
        while(transform.position != jug)
        {
            transform.position = Vector3.MoveTowards(transform.position, jug, velocidad * Time.deltaTime);
            yield return null;
        }
    }
}
