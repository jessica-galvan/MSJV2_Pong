using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    [SerializeField] private float radio;
    [SerializeField] private BallScript ball;
    [SerializeField] private PaletaScript paleta1;
    [SerializeField] private PaletaScript paleta2;
    [SerializeField] private Transform points;
    [SerializeField] private float appearTime;
    [SerializeField] private float inSceneTime;
    [SerializeField] private float cooldown;
    private float leaveTime;
    private float arriveTime;
    private bool canSetPosition;
    private bool isUp = true;

    public enum Ability
    {
        gravity,
        color,
        pelotaSize
    }

    public Ability opcion = Ability.color;

    private void Start()
    {
        transform.position = new Vector3(0f, 7f, -1f);
        arriveTime = appearTime + Time.time;
        leaveTime = appearTime + inSceneTime + Time.time;
    }

    private void Update()
    {
        //TIMER
        if(arriveTime < Time.time)
        {
            if(canSetPosition)
                SetPosition();
        } else
        {
            transform.position = new Vector3(0f, 7f, -1f);
            canSetPosition = true;
        }


        if (CheckDistance()) //si esta en contacto, fijate que powerup es
        {
            switch (opcion)
            {
                case Ability.gravity:
                    Gravity();
                    break;
                case Ability.color:
                    Color();
                    break;
                case Ability.pelotaSize:
                    PelotaSize();
                    break;
            }
        }
    }

    public void Desaparecer()
    {
        transform.position = new Vector3(0f, 7f, -1f);
    }
    
    public bool CheckDistance()
    {
        bool resultado = false;
        float sumaDeRadios = radio + ball.radio;
        if (Vector2.SqrMagnitude(transform.position - ball.transform.position) < sumaDeRadios * sumaDeRadios)
        {
            resultado = true;
        }
        return resultado;
    }

    public void Gravity()
    {
        ball.ChangeGravity();
        if (isUp)
        {
            isUp = !isUp;
            points.position = new Vector3(0f, 1003f, -10f);
        } else
        {
            isUp = !isUp;
            points.position = new Vector3(0f, -82f, -10f);
        }
        print("Gravity");
        Desaparecer();
    }

    public void Color()
    {
        int number = Random.Range(0, 3);
        ball.ChangeColor(number);
        print("Color");
        Desaparecer();
    }

    public void PelotaSize()
    {
        int number = Random.Range(0, 3);
        switch (number)
        {
            case 1:
                ball.ChangeSize(1f, 2f);
                break;
            case 2:
                ball.ChangeSize(0.2f, 0.5f);
                break;
            case 3:
                ball.ChangeSize(0.8f, 1.5f);
                break;
        }
        print("Size");
        Desaparecer();
    }

    public void SetPosition()
    {
        canSetPosition = false;
        int number = Random.Range(0, 6);
        switch (number)
        {
            case 1:
                transform.position = new Vector3(4f, 2f, 1f);
                break;
            case 2:
                transform.position = new Vector3(3f, -2f, 1f);
                break;
            case 3:
                transform.position = new Vector3(-3f, -2f, 1f);
                break;
            case 4:
                transform.position = new Vector3(-6f, 2f, 1f);
                break;
            case 5:
                transform.position = new Vector3(-1f, 1f, 1f);
                break;
            case 6:
                transform.position = new Vector3(0.5f, -1.5f, 1f);
                break;
        }

        //and reset time
        arriveTime = cooldown + Time.time;
        leaveTime = cooldown + inSceneTime + Time.time;
        print("reset");
    }
}
