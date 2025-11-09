using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerFoosBall : MonoBehaviour
{
    public static GameManagerFoosBall Instance;

    [Header("Puntuación")]
    public int scoreLeft = 0;
    public int scoreRight = 0;

    [Header("UI")]
    public TextMeshProUGUI leftScoreText;
    public TextMeshProUGUI rightScoreText;
    public TextMeshProUGUI timerText;
    public GameObject gameOverScreen;

    [Header("Tiempo de juego")]
    public float matchTime = 60f; // segundos
    private float timer;
    private bool gameEnded = false;

    [Header("Pelota")]
    public ShellBehaviour ballPrefab;
    public Transform ballSpawnPoint;
    private ShellBehaviour currentBall;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        timer = matchTime;
        SpawnBall();
        UpdateUI();
        if (gameOverScreen != null) gameOverScreen.SetActive(false);
    }

    void Update()
    {
        if (gameEnded) return;

        timer -= Time.deltaTime;
        timerText.text = $"Tiempo: {Mathf.CeilToInt(timer)}";

        if (timer <= 0f)
        {
            EndMatch();
        }
    }

    public void GoalLeft()
    {
        scoreRight++;
        UpdateUI();
        ResetBall();
    }

    public void GoalRight()
    {
        scoreLeft++;
        UpdateUI();
        ResetBall();
    }

    void UpdateUI()
    {
        leftScoreText.text = scoreLeft.ToString();
        rightScoreText.text = scoreRight.ToString();
    }

    void ResetBall()
    {
        if (currentBall != null)
            Destroy(currentBall.gameObject);

        Invoke(nameof(SpawnBall), 1.5f); 
    }

    void SpawnBall()
    {
        currentBall = Instantiate(ballPrefab, ballSpawnPoint.position, Quaternion.identity);
    }

    void EndMatch()
    {
        gameEnded = true;
        Time.timeScale = 0f; // Pausa física
        if (gameOverScreen != null)
            gameOverScreen.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
