using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    private Animator anim;
    public GameObject OptionsScreen;
    public Slider volumeSlider;
    public TextMeshProUGUI timeText;
    private void Start()
    {
        anim = GetComponent<Animator>();
        timeText.text = "Score: " +Math.Round(PlayerPrefs.GetFloat("time"));
    }

    public void PlayGame()
    {
        anim.SetTrigger("Play");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Circle9");
    }

    public void ShowOptions()
    {
        OptionsScreen.SetActive(!OptionsScreen.activeSelf);
    }

    public void HideOptions()
    {
        OptionsScreen.SetActive(false);
    }

    public void SetVolume()
    {
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
    }

    public void ExitGame()
    {
        anim.SetTrigger("Exit");
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
