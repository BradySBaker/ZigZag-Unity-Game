using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int points = 0;
    public bool gameStarted = false;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI instructionsText;
    public TextMeshProUGUI highScoreText;

    public void Start() {
        Debug.Log("HighScore: " + PlayerPrefs.GetInt("highScore", 0));
        DisplayMenu();
    }

    public void AddPoint() {
        points++;
        if (scoreText == null) {
            Debug.Log("Please add text mesh");
            return;
        }
        scoreText.text = points.ToString();
    }

    public void StopGame() {
        if (PlayerPrefs.GetInt("highScore") < points) {
            PlayerPrefs.SetInt("highScore", points);
        }
        PlayerPrefs.SetInt("prevScore", points);

        Debug.Log(PlayerPrefs.GetInt("highScore"));


        SceneManager.LoadScene(0);
    }

    public void StartGame() {
        scoreText.text = "0";
        instructionsText.gameObject.SetActive(false);
        highScoreText.gameObject.SetActive(false);
        gameStarted = true;
    }

    public void DisplayMenu() {
        scoreText.text = PlayerPrefs.GetInt("prevScore").ToString();
        gameStarted = false;
        instructionsText.gameObject.SetActive(true);
        highScoreText.gameObject.SetActive(true);
        highScoreText.text = "High Score " + PlayerPrefs.GetInt("highScore");
    }
}
