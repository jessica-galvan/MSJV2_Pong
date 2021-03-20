using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private BallScript ball;
    [SerializeField] private PaletaScript paleta1;
    [SerializeField] private PaletaScript paleta2;
    [SerializeField] private Transform points;
    [SerializeField] private PowerUpScript otherBall1;
    [SerializeField] private PowerUpScript otherBall2;

    [Header("Other Settings")]
    [SerializeField] private float appearTime;
    [SerializeField] private float inSceneTime;
    [SerializeField] private float cooldown;
    [SerializeField] private float radio;

    public enum Ability
    {
        gravity,
        color,
        pelotaSize
    }
    public Ability opcion = Ability.color;

    //Time & ranges
    private Vector3 startingPoint = new Vector3(0f, 7f, -1f);
    private float leaveTime;
    private float arriveTime;
    private int rangeEnd;
    private int rangeStart;

    //BOOL ZONE
    private bool canUseAbility = true;
    private bool canSetPosition = true;
    private bool canReset = false;

    //SAVE ZONE
    private int previousPosition;
    private int previousSize;
    private int previousGravity = 1;

    private void Start()
    {
        transform.position = startingPoint; //empeza fuera del campo
        arriveTime = appearTime + Time.time;
        leaveTime = appearTime + inSceneTime + Time.time;

        switch (opcion)
        {
            case Ability.gravity:
                rangeStart = 0;
                rangeEnd = 3;
                break;
            case Ability.color:
                rangeStart = 4;
                rangeEnd = 6;
                break;
            case Ability.pelotaSize:
                rangeStart = 7;
                rangeEnd = 9;
                break;
        }

    }

    private void Update()
    {
        //TIMER
        if(arriveTime < Time.time && Time.time < leaveTime)
        {
            if (canSetPosition)
            {
                canSetPosition = false;
                SetPosition();
            }
        } else
        {
            if (canReset)
            {
                Desaparecer();
            } else
            {
                transform.position = startingPoint;
            }
        }


        if (CheckDistance() ) //si esta en contacto, fijate que powerup es
        {
            if (canUseAbility)
            {
                canUseAbility = false;
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

    public void Desaparecer()
    {
        transform.position = startingPoint;
        canUseAbility = true;
        canSetPosition = true;
        canReset = false;

        //RESET TIME
        arriveTime = cooldown + Time.time;
        leaveTime = cooldown + inSceneTime + Time.time;
    }

    public void Gravity()
    {
        int number = Randomnizer(1, 3, previousGravity);
        previousGravity = number;
        switch (number)
        {
            case 1:
                ball.ChangeGravity(0f);
                break;

            case 2:
                ball.ChangeGravity(9.8f);
                break;

            case 3: 
                ball.ChangeGravity(-9.8f);
                break;
        }
        Desaparecer();
    }

    public void Color() //Cambia el color de la pelota
    {
        ball.ChangeColor(RandomColor()); 
        Desaparecer();
    }

    public void PelotaSize() //Cambia el tamaño de la pelota
    {
        int number = Randomnizer(1, 3, previousSize);
        previousSize = number; 

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
        Desaparecer();
    }

    public void SetPosition()
    {
        canSetPosition = false;
        canReset = true;
        int number = Randomnizer(0, 9, previousPosition, otherBall1.GetPreviousPosition(), otherBall2.GetPreviousPosition());
        previousPosition = number;

        switch (number)
        {
            case 1:
                transform.position = new Vector3(4f, 2f, 1f);
                break;
            case 2:
                transform.position = new Vector3(3f, -2f, 1f);
                break;
            case 3:
                transform.position = new Vector3(4f, -0.2f, 1f);
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
            case 7:
                transform.position = new Vector3(-5.5f, 3f, 1f);
                break;
            case 8:
                transform.position = new Vector3(1.75f, 2.91f, 1f);
                break;
            case 9:
                transform.position = new Vector3(1.5f, -3.6f, 1f);
                break;
        }
    }

    private int Randomnizer(int start, int end, int previous, int ball1 = 0, int ball2 = 0)
    {
        int number = Random.Range(0, 3);
        while (number == previous || number == ball1 || number == ball2)
        {
            number = Random.Range(start, end);
        }
        return number;
    }

    private Color RandomColor() //para generar un color random
    {
        return new Color(Random.value, Random.value, Random.value);
    }

    private int GetPreviousPosition()
    {
        return previousSize;
    }

}
