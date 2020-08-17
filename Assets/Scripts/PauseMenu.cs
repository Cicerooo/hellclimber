using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private Movement _movement;
    private void Awake()
    {
        InputMaster inputMaster = new InputMaster();
        inputMaster.UI.TogglePause.performed += _ => TogglePause();
        inputMaster.UI.TogglePause.Enable();
    }

    private void Start()
    {
        _movement = FindObjectOfType<Movement>();
    }

    public void TogglePause()
    {
        bool state = !pauseMenu.activeSelf;
        if (state)
        {
            _movement.DisableMovement();
            Time.timeScale = 0;
        }
        else
        {
            _movement.EnableMovement();
            Time.timeScale = 1;
        }
        pauseMenu.SetActive(state);
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
