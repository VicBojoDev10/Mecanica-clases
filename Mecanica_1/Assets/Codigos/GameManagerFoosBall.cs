using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerFoosBall : MonoBehaviour
{
    public static GameManagerFoosBall Instance;

    public int scoreLeft = 0;
    public int scoreRight = 0;

    public TextMeshProUGUI leftScoreText;
    public TextMeshProUGUI rightScoreText;
    public ShellBehaviour ballPrefab;
    public Transform ballSpawnPoint;

    private ShellBehaviour currentBall;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        SpawnBall();
        UpdateUI();
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

        Invoke(nameof(SpawnBall), 1f);
    }

    void SpawnBall()
    {
        currentBall = Instantiate(ballPrefab, ballSpawnPoint.position, Quaternion.identity);
    }
}
