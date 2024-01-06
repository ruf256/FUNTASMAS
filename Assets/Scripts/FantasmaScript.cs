using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FantasmaScript : MonoBehaviour
{

    public float velocidad;

    [SerializeField]
    float frequency = 20f;

    [SerializeField]
    float magnitude = 0.5f;

    private PlayerColissionScript jugador;
    private SpriteRenderer spriteRenderer;
    Vector3 pos;
    private float transparencia;
    [SerializeField] private float tiempoTransparencia;

    private float esOpacoTimer;
    [SerializeField] private float opacoCooldown;
    private bool volviendoTransparencia;

    private Vector3 offset;
    private float offsetOsc;

    //Challenge
    public bool fueraDeCam = false;
    private float distanciaEnter;
    private float distanciaExit;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(spriteRenderer.color.r,spriteRenderer.color.g,spriteRenderer.color.b, transparencia);
 
        offset = new Vector3(Random.Range(-1.5f,1.5f), Random.Range(-2f, 2f), 0);
        offsetOsc = Random.Range(0, 3.5f);
        frequency += offsetOsc;
    }

    private void Start()
    {
        PlayerEventScript.Instance.SumarPunto += Instance_RestartTransparencia;
        jugador = FindAnyObjectByType<PlayerColissionScript>();
    }

    private void Instance_RestartTransparencia(object sender, System.EventArgs e)
    {
        if(transparencia > 0.8)
        {
            transparencia -= 0.25f;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!StateManager.Instance.isJugando()) return;
        if(Vector2.Distance(transform.position, jugador.transform.position) > 2.5f)
        {
            pos = Vector2.MoveTowards(transform.position, jugador.transform.position + offset, velocidad * Time.deltaTime);
        }
        else
        {
            pos = Vector2.MoveTowards(transform.position, jugador.transform.position, velocidad * Time.deltaTime);
        }
        
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;

        

        if(transparencia < 1 && !volviendoTransparencia && !fueraDeCam)
        {
            transparencia = Mathf.Lerp(transparencia, transparencia + 0.2f, tiempoTransparencia * Time.deltaTime);
        }
        else if (transparencia >= 1 && esOpacoTimer < opacoCooldown)
        {
            esOpacoTimer += Time.deltaTime;
        }
        else if (esOpacoTimer >= opacoCooldown && !volviendoTransparencia)
        {
            volviendoTransparencia = true;
        }
        else if (transparencia > 0 && volviendoTransparencia)
        {
            transparencia = Mathf.Lerp(transparencia, transparencia - 0.2f, tiempoTransparencia * Time.deltaTime);
        }
        else
        {
            volviendoTransparencia = false;
            esOpacoTimer = 0;
        }
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, transparencia);
        if (jugador.transform.position.x < transform.position.x)
        {
            transform.rotation = new Quaternion(transform.rotation.x,180,transform.rotation.z,transform.rotation.w);
        }
        else
        {
            transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.StartsWith("Limite"))
        {
            distanciaEnter = Vector2.Distance(transform.position, Camera.main.transform.position);
            fueraDeCam = !fueraDeCam;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!StateManager.Instance.isJugando()) return;
        distanciaExit = Vector2.Distance(transform.position, Camera.main.transform.position);
        if (collision.tag.StartsWith("Limite") && distanciaExit < distanciaEnter)
        {
            fueraDeCam = false;
        }
        else if (collision.tag.StartsWith("Limite") && distanciaExit > distanciaEnter)
        {
            fueraDeCam = true;
            if(transparencia >= 0.7f) transparencia -= 0.25f;

        }
    }

}
