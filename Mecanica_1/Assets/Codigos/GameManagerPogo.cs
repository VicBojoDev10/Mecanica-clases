using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerPogo : MonoBehaviour
{
    public float gameTime = 60f; // Duración en segundos
    public TextMeshProUGUI timerText;
    public GameObject gameOverScreen;

    private float timer;
    private int activePlayers = 3; // Total de jugadores
    public static GameManagerPogo instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        timer = gameTime;
        gameOverScreen.SetActive(false);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        timerText.text = $"Time: {Mathf.CeilToInt(timer)}";

        if (timer <= 0)
        {
            EndGame();
        }
    }

    public void CheckPlayersAlive()
    {
        activePlayers--;

        if (activePlayers <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);
        timerText.text = "Game Over!";
    }
    public void RestartGame()
    {
        Time.timeScale = 1f; 
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex); 
    }
}
