using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsScript : MonoBehaviour
{
    [SerializeField] private BallScript ball = null;
    [SerializeField] private Text txt = null;

    
    void Start()
    {
        ball.OnChangePoints.AddListener(CountPoints);
        CountPoints();   
    }

    void CountPoints()
    {
        txt.text = ball.getPoints();
        
    }
}
