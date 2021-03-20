using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("MainMenu Settings")]
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonQuit;

    void Start()
    {
        buttonPlay.onClick.AddListener(OnClickPlayHandler);
        buttonQuit.onClick.AddListener(OnClickQuitHandler);
    }

    private void OnClickPlayHandler() //inicia el juego
    {
        SceneManager.LoadScene("SampleScene");
    }
    private void OnClickQuitHandler()  // Cierra el Menu
    {
        Application.Quit();
        print("Cerramos el juego");
    }
}
