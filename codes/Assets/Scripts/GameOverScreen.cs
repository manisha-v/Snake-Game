using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public Text scoreText;
    
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        scoreText.text = "Score: " + score.ToString(); 
    }

    public void restartButton()
    {
        Debug.Log("Restart");
        SceneManager.LoadScene("snakeGame");
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
