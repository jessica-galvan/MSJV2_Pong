using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallScript : MonoBehaviour
{
    public float radio = 0.5f;
    public float scale = 1f;
    [SerializeField] private int speed;
    [SerializeField] private PaletaScript paletaDerecha;
    [SerializeField] private PaletaScript paletaIzquierda;
    [SerializeField] private Transform powerUp;
    //private float radioPowerUp = 1f;
    public int puntajeIzquierda = 0;
    public int puntajeDerecha = 0;
    public UnityEvent OnChangePoints = new UnityEvent();
    [SerializeField] private Vector3 gravedad = new Vector3 (0f, 0f, 0f);

    //Settings
    private Vector3 velocidad;
    private float paredHorizontal = 5f;
    private float paredVertical = 9.5f;
    private float paletaLocation = 8.5f;
    private bool canMove = true;
    private bool startTimer = false;
    private float cooldown = 0.8f;
    private float timer = 0f;
    private float originalRadio = 0.5f;
    private float originalScale = 1f;
    private bool canTimerScale;
    private float timerScale = 0f;
    private float scaleCD = 15f;

    //Others
    private SpriteRenderer spriteRender;

    void Start()
    {
        velocidad = new Vector3(speed, speed, 0f);
        ResetPosition();
        spriteRender = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //REBOTE EN X
        if(transform.position.y >= paredHorizontal - radio)
        {
           velocidad.y = -Mathf.Abs(velocidad.y);
        }

        if (transform.position.y <= -paredHorizontal + radio)
        {
            velocidad.y = Mathf.Abs(velocidad.y);
        }

        //PARED DERECHA
        if(transform.position.x >= paletaLocation - radio)
        {
            float paletaD = paletaDerecha.transform.position.y;
            //Rebote con PaletaDerecha
            if (transform.position.y <= paletaD + paletaDerecha.altura && transform.position.y >= paletaD - paletaDerecha.altura)
            {
                velocidad.x = -Mathf.Abs(velocidad.x);
            }
        } 
        
        if(transform.position.x >= paredVertical - radio)
        {
            puntajeIzquierda++;
            ResetPosition();
        }

        //PARED IZQUIERDA
        if (transform.position.x <= -paletaLocation + radio)
        {
            float paletaI = paletaIzquierda.transform.position.y;
            if (transform.position.y <= paletaI + paletaIzquierda.altura && transform.position.y >= paletaI - paletaIzquierda.altura)
            {
                velocidad.x = Mathf.Abs(velocidad.x);
            }
        }
        
        if(transform.position.x <= -paredVertical + radio)
        {
            puntajeDerecha++;
            ResetPosition();
        }

        //MOVIMIENTO
        if (canMove)
        {
            //gravedad -> velocidad -> posicion
            //La gravedad me modifica a la velocidad, la velocidad me modifica a la posicion

            velocidad += gravedad * Time.deltaTime;

            transform.position += velocidad * Time.deltaTime + 0.5f * gravedad * Time.deltaTime * Time.deltaTime;
        }

        //TIMER
        if(startTimer && !canMove & timer < Time.time)
        {
            canMove = true;
        }

        if (canTimerScale & timerScale < Time.time)
        {
            canTimerScale = false;
            radio = originalRadio;
            scale = originalScale;
            this.gameObject.transform.localScale = new Vector3(scale, scale, 0f);
        }
    }

    void ResetPosition()
    {
        OnChangePoints.Invoke();
        canMove = false;
        transform.position = Vector3.zero;
        startTimer = true;
        timer = cooldown + Time.time;
    }

    public string GetPoints()
    {
        return puntajeIzquierda + " | " + puntajeDerecha;
    }

    public void ChangeColor(int number)
    {
        switch (number)
        {
            case 1:
                spriteRender.color = Color.red;
                break;
            case 2:
                spriteRender.color = Color.blue;
                break;
            case 3:
                spriteRender.color = Color.white;
                break;
        }
    }

    public void ChangeGravity()
    {
        gravedad.y = -gravedad.y;
    }

    public void ChangeSize(float newRadio, float newScale)
    {
        radio = newRadio;
        scale = newScale;
        this.gameObject.transform.localScale = new Vector3(scale, scale, 0f);
        canTimerScale = true;
        timerScale = scaleCD + Time.time;
    }
}
