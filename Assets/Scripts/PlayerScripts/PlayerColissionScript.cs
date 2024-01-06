using System.Collections.Generic;
using UnityEngine;

public class PlayerColissionScript : MonoBehaviour
{

    PlayerEventScript playerEventScript;
    PlayerMovement playerMovementScript;
    PuntoEffect puntoEffect;

    [SerializeField] List<Transform> limitesY;
    [SerializeField] List<Transform> limitesX;


    private void Start()
    {
        playerEventScript = GetComponentInParent<PlayerEventScript>();
        playerMovementScript = FindAnyObjectByType<PlayerMovement>();
        puntoEffect = GetComponent<PuntoEffect>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "FantasmaMalo":
                if (collision.GetComponent<SpriteRenderer>().color.a >= 1 && StateManager.Instance.isJugando())
                {
                    playerEventScript.SendMessage("GameOverEvent");
                }
                break;
            case "LimiteX":
                TPx(collision.transform);
                break;
            case "LimiteY":
                TPy(collision.transform);
                break;
            case "Punto":
                playerEventScript?.SendMessage("SumarPuntoEvent");
                puntoEffect.InstanciarEfecto(collision.transform.position);
                Destroy(collision.gameObject);
                break;
            case "Muerte":
                playerEventScript.SendMessage("GameOverEvent");
                break;
            case "PuntoVictoria":
                puntoEffect.InstanciarEfecto(collision.transform.position);
                playerEventScript?.SendMessage("SumarPuntoEvent");
                playerEventScript.SendMessage("LevelCompleteEvent");
                Destroy(collision.gameObject);
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "FantasmaMalo":
                if (collision.GetComponent<SpriteRenderer>().color.a >= 1 && StateManager.Instance.isJugando())
                {
                    playerEventScript.SendMessage("GameOverEvent");
                }
                break;
        }
    }

    void TPy(Transform collider)
    {
        if (limitesY[0] == collider)
        {
            if (limitesY[1].position.y > transform.position.y)
            {
                transform.position = new Vector2(transform.position.x, limitesY[1].position.y - 2f);
            }
            else
            {
                transform.position = new Vector2(transform.position.x, limitesY[1].position.y + 2f);
            }
            
        }
        else
        {
            if (limitesY[0].position.y > transform.position.y)
            {
                transform.position = new Vector2(transform.position.x, limitesY[0].position.y - 2f);
            }
            else
            {
                transform.position = new Vector2(transform.position.x, limitesY[0].position.y + 2f);
            }
        }
    }

    void TPx(Transform collider)
    {
        if (limitesX[0] == collider)
        {
            if (limitesX[1].position.x > transform.position.x)
            {
                transform.position = new Vector2(limitesX[1].position.x - 2f, transform.position.y);
            }
            else
            {
                transform.position = new Vector2(limitesX[1].position.x + 2f, transform.position.y);
            }
        }
        else
        {
            if (limitesX[0].position.x > transform.position.x)
            {
                transform.position = new Vector2(limitesX[0].position.x - 2f, transform.position.y);
            }
            else
            {
                transform.position = new Vector2(limitesX[0].position.x + 2f, transform.position.y);
            }
        }
    }

}
