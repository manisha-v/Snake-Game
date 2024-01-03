using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject Options;
  
    public void Setup()
    {
        gameObject.SetActive(true);
    }

    public void Play()
    {
        Snake.isStart = true;
        SceneManager.LoadScene("snakeGame");
    }

    public void GameSetting()
    {
        Options.SetActive(true);
    }

    public void returnFromSettings()
    {
        Options.SetActive(false);
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }

}
