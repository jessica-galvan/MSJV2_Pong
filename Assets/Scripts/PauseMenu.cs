using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private AudioSource clip;
    [SerializeField] private AudioSource musicLevel;

    [Header("Pause Settings")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button buttonResume;
    [SerializeField] private Button buttonMainMenu;
    [SerializeField] private Button buttonQuit;
    private bool pauseMenuActive;
    private float lowerVolume = 0.90f;

    [Header("Win Settings")]
    [SerializeField] private GameObject winMenu;
    [SerializeField] private Button buttonWinPlay;
    [SerializeField] private Button buttonWinMainMenu;
    [SerializeField] private Button buttonWinQuit;
    [SerializeField] private Text winMessage;
    private bool winMenuActive;


    void Start()
    {
        ExitMenu();
        winMenuActive = false;
        winMenu.SetActive(false);
        //winMessage = GetComponent<Text>();
        buttonResume.onClick.AddListener(OnClickResumeHandler);
        buttonQuit.onClick.AddListener(OnClickQuitHandler);
        buttonMainMenu.onClick.AddListener(OnClickMenuHandler);
        buttonWinPlay.onClick.AddListener(OnClickPlayHandler);
        buttonWinQuit.onClick.AddListener(OnClickQuitHandler);
        buttonWinMainMenu.onClick.AddListener(OnClickMenuHandler);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) & !winMenuActive)
        {
            if (!pauseMenuActive)
            {
                Pause();
            }
            else
            {
                ExitMenu();
            }
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SelectWinner(1);
        }
    }

    private void Pause()
    {
        Time.timeScale = 0;
        musicLevel.volume -= lowerVolume;
        pauseMenuActive = true;
        pauseMenu.SetActive(true);

    }

    private void ExitMenu()
    {
        Time.timeScale = 1;
        musicLevel.volume += lowerVolume;
        pauseMenuActive = false;
        pauseMenu.SetActive(false);
    }

    private void OnClickResumeHandler()
    {
        clip.Play();
        ExitMenu();
    }

    private void OnClickPlayHandler()
    {
        clip.Play();
        SceneManager.LoadScene("SampleScene");
    }

    private void OnClickQuitHandler()
    {
        clip.Play();
        Application.Quit();
        print("Se cierra el juego");
    }

    private void OnClickMenuHandler()
    {
        clip.Play();
        SceneManager.LoadScene("Menu");
        print("Menu");
    }

    public void SelectWinner(int number)
    {
        winMessage.text = $"PLAYER {number} WINS";
        Time.timeScale = 1;
        pauseMenuActive = false;
        pauseMenu.SetActive(false);
        winMenuActive = true;
        winMenu.SetActive(true);
    }
}
