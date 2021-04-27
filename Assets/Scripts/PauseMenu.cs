using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InControl;

public class PauseMenu : MonoBehaviour
{
    private bool _gameIsPaused = false; 

    [SerializeField] private GameObject pauseMenuUi;
    [SerializeField] private PlayerController playerController;

    void Update()
    {
        if (InputManager.Devices.Count <= playerController.index)
        {
            return;
        }

        var device = InputManager.Devices[playerController.index];
        if (device.Command.WasPressed)
        {
            if (_gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        _gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        _gameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
        Debug.Log("Loading Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game");
    }
}
