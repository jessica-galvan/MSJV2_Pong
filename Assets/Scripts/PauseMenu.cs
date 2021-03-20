using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("AllMenus Settings")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button buttonResume;
    [SerializeField] private Button buttonMainMenu;
    [SerializeField] private Button buttonQuit;

    private bool pauseMenuActive;

    void Start()
    {
        ExitMenu();
        buttonResume.onClick.AddListener(OnClickResumeHandler);
        buttonQuit.onClick.AddListener(OnClickQuitHandler);
        buttonMainMenu.onClick.AddListener(OnClickMenuHandler);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
    }

    private void Pause()
    {
        Time.timeScale = 0;
        pauseMenuActive = true;
        pauseMenu.SetActive(true);

    }

    private void ExitMenu()
    {
        Time.timeScale = 1;
        pauseMenuActive = false;
        pauseMenu.SetActive(false);
    }

    private void OnClickResumeHandler()
    {
        ExitMenu();
    }

    private void OnClickQuitHandler()
    {
        Application.Quit();
        print("Se cierra el juego");
    }

    private void OnClickMenuHandler()
    {
        SceneManager.LoadScene("Menu");
        print("Menu");
    }
}
