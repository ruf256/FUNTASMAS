using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelonScript : MonoBehaviour
{
    private float velocidad = 6.5f;
    private int Rdirec;
    private int Xdirec = 0;
    private int Ydirec = 0;

    private float timerDesaparecer;
    private void Start()
    {
        Rdirec = Random.Range(0, 4);

        switch (Rdirec)
        {
            case 0:
                Xdirec = 1;
                velocidad += 1;
                break;
            case 1:
                Xdirec = -1;
                velocidad += 1;
                break;
            case 2:
                Ydirec = 1;
                break;
            case 3:
                Ydirec = -1;
                break;
        }

    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(50 * Xdirec, 50 * Ydirec), velocidad * Time.deltaTime);
        timerDesaparecer += Time.deltaTime;

        if( timerDesaparecer > 3.5)
        {
            Destroy(gameObject);
        }
    }
}
