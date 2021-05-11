using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PROFECuerpoFisicoScript : MonoBehaviour
{
    public float masa;
    public Vector3 velocidad;

    Vector3 aceleracion;

    public void ApplyForce(Vector3 fuerza)
    {
        aceleracion += fuerza / masa;
    }

    public void PasoDeFisica()
    {
        velocidad += aceleracion*Time.deltaTime;
        transform.position += velocidad * Time.deltaTime + 0.5f * aceleracion * Time.deltaTime * Time.deltaTime;
        aceleracion = Vector3.zero;
    }
}
