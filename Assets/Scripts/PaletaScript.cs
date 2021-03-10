using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaletaScript : MonoBehaviour
{
    [SerializeField] private float radio = 0.5f;
    [SerializeField] private int speed = 5;
    [SerializeField] private KeyCode arriba;
    [SerializeField] private KeyCode abajo;
    private Vector3 velocidad;
    private float paredHorizontal = 5f;
    public float altura = 1.5f;
    
    void Start()
    {
        velocidad = Vector3.zero;
    }

    private void Update()
    {
        velocidad.y = 0f;

        if (Input.GetKey(arriba))
        {
            velocidad.y += speed;
        }

        if (Input.GetKey(abajo))
        {
            velocidad.y -= speed;
        }

        //Si me pase por el borde
        if (transform.position.y >= paredHorizontal - altura)
        {
            //velocidad.y = 0f; //Esto no funciona, me deja freezado y nunca más me puedo mover
            transform.position = new Vector3(transform.position.x, paredHorizontal - altura, transform.position.z);
        }

        if (transform.position.y <= -paredHorizontal + altura)
        {
            transform.position = new Vector3(transform.position.x, -paredHorizontal + altura, transform.position.z);
        }

        transform.position += velocidad * Time.deltaTime;
    }
}
