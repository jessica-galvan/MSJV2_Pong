using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaletaScript : CuerpoFisicoScript
{
    [SerializeField] private float radio = 0.5f;
    [SerializeField] private KeyCode arriba;
    [SerializeField] private KeyCode abajo;
    private float velocidadDeseada; //la velocidad a al que me gustaria ir segun las teclas apretdadas
    [SerializeField] private float velocidadMaxima; //la velocidad maxima a la que puedo ir.
    //[SerializeField] private float fuerzaTeclas; //que tan rapido me gustaria cambiar mi velocidad
    private float paredHorizontal = 5f;
    public float altura = 1.5f;
    private float fuerza;
    private float fuerzaMaxima = 10f;

    void Start()
    {
        aceleracion = Vector3.zero;
        velocidad = Vector3.zero;
    }

    private void Update()
    {
        velocidadDeseada = 0f;
        fuerza = 0;

        if (Input.GetKey(arriba))
        {
            velocidadDeseada += velocidadMaxima;
        }

        if (Input.GetKey(abajo))
        {
            velocidadDeseada -= velocidadMaxima;
        }

        fuerza = masa * (velocidadDeseada - velocidad.y) / Time.deltaTime;
        fuerza = Mathf.Clamp(fuerza, -fuerzaMaxima, fuerzaMaxima);
        ApplyForce(Vector3.up * fuerza);

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

        PasoDeFisica();
    }
}
