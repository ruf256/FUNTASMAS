using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerEventScript playerEventScript;
    float movimientoValue;
    PlayerInputActions playerInputActions;

    private Rigidbody2D ownBody;
    public float velocidadRotacion;

    [SerializeField] private float velocidad;
    public float velocidadInicial;
    [SerializeField] private float limiteVelocidad = 210;

    private float tiempoAceleracion = 2;
    [SerializeField] private GameObject sprite;
    [SerializeField] private SpriteRenderer pointer;

    [SerializeField] private AudioSource cargarSound;
    [SerializeField] private AudioSource tirarSound;
    [SerializeField] private AudioSource puntoSound;

    private float ultimoPuntoAgarrado = 2;
    private float cdPitch = 1.5f;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Movimiento.canceled += Movimiento_canceled;
        ownBody = GetComponentInParent<Rigidbody2D>();
        playerEventScript = GetComponentInParent<PlayerEventScript>();

        
    }

    void Start()
    {
        velocidadInicial = velocidad;
        tiempoAceleracion = 2;
        sprite.transform.position = transform.position;

        PlayerEventScript.Instance.SumarPunto += Instance_SumarPunto;
    }

    void Update()
    {
        ultimoPuntoAgarrado += Time.deltaTime;
        if (transform.rotation.eulerAngles.z >= 90 && transform.rotation.eulerAngles.z <= 270)
        {
            sprite.transform.rotation = new Quaternion(0, 180, 0, 0);

        }
        else
        {
            sprite.transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        if (StateManager.Instance.isGameOver() || StateManager.Instance.isNivelCompletado())
        {
            if (cargarSound.isPlaying) cargarSound.Stop();
            playerInputActions.Player.Movimiento.canceled -= Movimiento_canceled;
            sprite.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0.50f);
            return;
        }

        movimientoValue = playerInputActions.Player.Movimiento.ReadValue<float>();
        //Movimiento();
        if (movimientoValue != 0)
        {
            if(!cargarSound.isPlaying) cargarSound.Play();
            transform.Rotate(Vector3.forward, -1 * movimientoValue * velocidadRotacion * Time.deltaTime);
            pointer.color = new Color(255, 255, 255, 1);

            if (!StateManager.Instance.isJugando()) return;
            velocidad *= 0.975f;
            tiempoAceleracion += 2 * Time.fixedDeltaTime;
            if (tiempoAceleracion >= 5.5f)
            {
                tiempoAceleracion = 5.5f;
            }
            else
            {
                pointer.transform.localScale += pointer.transform.localScale * 0.5f * Time.deltaTime;
            }
        }
        else
        {
            velocidad = Mathf.Lerp(velocidad, velocidadInicial, 0.5f * Time.deltaTime);
            pointer.color = new Color(255, 255, 255, 0);
            pointer.transform.localScale = new Vector3(1.2f,1.2f,1.2f);
        }

    }

    void FixedUpdate()
    {
       
        if (!StateManager.Instance.isJugando())
        {
            ownBody.velocity = Vector2.zero;
            return;
            
        }
        
        ownBody.velocity = (Vector2)transform.right * velocidad * Time.fixedDeltaTime;

        sprite.transform.position = transform.position;
    }

    private void Movimiento_canceled(InputAction.CallbackContext ctx)
    {
        cargarSound.Stop();
        if (!StateManager.Instance.isJugando()) return;
        if (tiempoAceleracion > 3.5f)
        {
            tirarSound.volume = tiempoAceleracion / 10;
            tirarSound.Play();
        }
        velocidad = velocidadInicial * tiempoAceleracion;
        tiempoAceleracion = 2;
        pointer.color = new Color(255, 255, 255, 0);
        pointer.transform.localScale = Vector3.one;
        playerEventScript.SendMessage("MovioseEvent");
    }


    private void Instance_SumarPunto(object sender, System.EventArgs e)
    {
        if (ultimoPuntoAgarrado < cdPitch) puntoSound.pitch += 0.25f;
        else puntoSound.pitch = 1.25f;
        ultimoPuntoAgarrado = 0;
        puntoSound.Play();

        if (velocidadInicial < limiteVelocidad) velocidadInicial += 10;

        if (velocidadRotacion < 185) velocidadRotacion += 10;
    }

}
