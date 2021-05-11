using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuerpoFisicoScript : MonoBehaviour
{
    [SerializeField] protected float masa = 1f;
    protected Vector3 velocidad;
    protected Vector3 aceleracion;

    protected void PasoDeFisica() // Como afecto al objeto
    {
        velocidad += aceleracion * Time.deltaTime;

        //esta es la cuenta x(t+1) = x(t= + v * t + 1/2 * a * t *t);
        transform.position += velocidad * Time.deltaTime + 0.5f * aceleracion * Time.deltaTime * Time.deltaTime;
        aceleracion = Vector3.zero;
    }

    protected void ApplyForce(Vector3 fuerza) //Metodo de calculo para calcular los valores
    {
        aceleracion += fuerza / masa; //* Time.deltaTime;
    }
}
