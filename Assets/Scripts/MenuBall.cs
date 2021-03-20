using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBall : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private float changeTime = 5f;

    //Settings
    private Vector3 gravedad = new Vector3(0f, 0f, 0f);
    private Vector3 velocidad;
    private float paredHorizontalA = 3f;
    private float paredHorizontalB = -5f;
    private float paredVerticalD = 9.5f;
    private float paredVerticalI = -5f;
    private float radio = 0.5f;

    //Timer
    private float nextChange;
    private int number = 2;
    private bool canChange;

    //Others
    private SpriteRenderer spriteRender;

    void Start()
    {
        velocidad = new Vector3(speed, speed, 0f);
        nextChange = Time.time + changeTime;
        spriteRender = GetComponent<SpriteRenderer>();
        gravedad.y = -9.8f;
        print(gravedad);
    }

    // Update is called once per frame
    void Update()
    {
        //REBOTE ARRIBA
        if (transform.position.y >= paredHorizontalA - radio)
        {
            velocidad.y = -Mathf.Abs(velocidad.y);
        }

        //REBOTE ABAJO
        if (transform.position.y <= paredHorizontalB + radio)
        {
            velocidad.y = Mathf.Abs(velocidad.y);
        }

        //PARED DERECHA
        if (transform.position.x >= paredVerticalD - radio)
        {
            velocidad.x = -Mathf.Abs(velocidad.x);
        }

        //PARED IZQUIERDA
        if (transform.position.x <= paredVerticalI + radio)
        {
            velocidad.x = Mathf.Abs(velocidad.x);
        }

        //CAMBIAR GRAVEDAD
        /*
        if(nextChange < Time.time)
        {
            canChange = true;
            nextChange = Time.time + changeTime;
        }

        if (canChange)
        {
            canChange = false;
            //Gravity();
        }
        */

        //MOVIMIENTO
        velocidad += gravedad * Time.deltaTime;
        transform.position += velocidad * Time.deltaTime + 0.5f * gravedad * Time.deltaTime * Time.deltaTime;
    }

    public void Gravity()
    {
        canChange = false;
        switch (number)
        {
            case 1:
                number++;
                gravedad.y = 0f;
                spriteRender.color = Color.black;
                break;

            case 2:
                number++;
                gravedad.y = 9.8f;
                spriteRender.color = Color.blue;
                break;

            case 3:
                number = 1;
                gravedad.y = -9.8f;
                spriteRender.color = Color.red;
                break;
        }
        print("change");
    }
}
