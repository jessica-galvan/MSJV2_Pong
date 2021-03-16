using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaletaScript : MonoBehaviour
{
    [SerializeField] private float radio = 0.5f;
    //[SerializeField] private int speed = 5;
    [SerializeField] private KeyCode arriba;
    [SerializeField] private KeyCode abajo;
    private float velocidadDeseada; //la velocidad a al que me gustaria ir segun las teclas apretdadas
    [SerializeField] private float velocidadMaxima; //la velocidad maxima a la que puedo ir.
    [SerializeField] private float aceleracionTeclas; //que tan rapido me gustaria cambiar mi velocidad
    private Vector3 aceleracion; //cuanto esta acelerando ahora mismo
    private Vector3 velocidad; //cuanto se esta movimiendo ahora mismo.
    private float paredHorizontal = 5f;
    public float altura = 1.5f;
    
    void Start()
    {
        aceleracion = Vector3.zero;
        velocidad = Vector3.zero;
    }

    private void Update()
    {
        //velocidad.y = 0f; //ya no puedo olvidarme la velocidad anterior
        velocidadDeseada = 0f;

        if (Input.GetKey(arriba))
        {
            //velocidad.y += speed;
            velocidadDeseada += velocidadMaxima;
        }

        if (Input.GetKey(abajo))
        {
            //velocidad.y -= speed;
            velocidadDeseada -= velocidadMaxima;
        }

        //Acelerar para acercarme a la velocidadDeseada
        if(velocidad.y < velocidadDeseada)
        {
            aceleracion = Vector3.up * aceleracionTeclas;
            velocidad += aceleracion * Time.deltaTime;

            //verifico que no me pase de la velocidad deseada
            if(velocidad.y > velocidadDeseada)
            {
                velocidad.y = velocidadDeseada;
            }
        }

        if(velocidad.y > velocidadDeseada)
        {
            aceleracion = Vector3.down * aceleracionTeclas;
            velocidad += aceleracion * Time.deltaTime;

            if (velocidad.y < velocidadDeseada)
            {
                velocidad.y = velocidadDeseada;
            }
        }

        //Si me pase por el borde
        if (transform.position.y >= paredHorizontal - altura)
        {
            //velocidad.y = 0f; //Esto no funciona, me deja freezado y nunca más me puedo mover
            transform.position = new Vector3(transform.position.x, paredHorizontal - altura, transform.position.z);
            velocidad.y = 0f;
        }

        if (transform.position.y <= -paredHorizontal + altura)
        {
            transform.position = new Vector3(transform.position.x, -paredHorizontal + altura, transform.position.z);
            velocidad.y = 0f;
        }

        //esta es la cuenta x(t+1) = x(t= + v * t + 1/2 * a * t *t;
        transform.position += velocidad * Time.deltaTime + 0.5f * aceleracion *Time.deltaTime * Time.deltaTime;
    }
}
