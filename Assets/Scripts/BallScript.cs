using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallScript : MonoBehaviour
{
    [SerializeField] private float radio = 0.5f;
    [SerializeField] private int speed;
    [SerializeField] private PaletaScript paletaDerecha;
    [SerializeField] private PaletaScript paletaIzquierda;
    public int puntajeIzquierda = 0;
    public int puntajeDerecha = 0;
    public UnityEvent OnChangePoints = new UnityEvent();

    //Settings
    private Vector3 velocidad;
    private float paredHorizontal = 5f;
    private float paredVertical = 9.5f;
    private float paletaLocation = 8.5f;
    private bool canMove = true;
    private bool startTimer = false;
    private float cooldown = 0.8f;
    private float timer = 0f;

    void Start()
    {
        velocidad = new Vector3(speed, speed, 0f);
        ResetPosition();
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
                print("hola");
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
            transform.position += velocidad * Time.deltaTime;
        }

        //TIMER
        if(startTimer && !canMove & timer < Time.time)
        {
            canMove = true;
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

    public string getPoints()
    {
        return puntajeIzquierda + " | " + puntajeDerecha;
    }
}
