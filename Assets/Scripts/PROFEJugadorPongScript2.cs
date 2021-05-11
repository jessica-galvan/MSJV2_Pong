using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PROFEJugadorPongScript : PROFECuerpoFisicoScript
{
    // La velocidad a la que me gustaria ir segun las teclas que tengo apretadas
    float velocidadDeseada;
    // Que tan rapido puedo ir
    public float velocidadMaxima;
    // Que tan rapido me gustaria que cambie mi velocidad
    public float fuerzaMaxima;

    float paredHorizontal = 5f;
    public float altura = 1.5f;

    public KeyCode arriba;
    public KeyCode abajo;

    void Start()
    {
      
    }

    // Update is called once per frame
    public void Update()
    {
        velocidadDeseada = 0f;

        if (Input.GetKey(arriba))
        {
            velocidadDeseada += velocidadMaxima;
        }

        if (Input.GetKey(abajo))
        {
            velocidadDeseada -= velocidadMaxima;
        }

        // Calculamos la fuerza necesaria para llegar en un frame a la velocidad Deseada
        // fuerza = masa * aceleracion   <-- Ley de Newton (1)
        // aceleracion = deltaVelocidad / deltaTime   <-- Definicion (2)
        // fuerza = masa * (deltaVelocidad / deltaTime)   <-- Reemplazo (2) en (1)
        float fuerza = masa * (velocidadDeseada - velocidad.y) / Time.deltaTime;
        // Limitamos la fuerza de forma que no se pase de FuerzaMaxima
        fuerza = Mathf.Clamp(fuerza, -fuerzaMaxima, fuerzaMaxima);
        ApplyForce(Vector3.up * fuerza);

        if (transform.position.y > paredHorizontal - altura)
        {
            transform.position = new Vector3(transform.position.x, paredHorizontal - altura);
            velocidad.y = 0f;
        }

        if (transform.position.y < - paredHorizontal + altura)
        {
            transform.position = new Vector3(transform.position.x, - paredHorizontal + altura);
            velocidad.y = 0f;
        }

        PasoDeFisica();
    }
}
