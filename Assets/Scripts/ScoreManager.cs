using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private float p1Score = 0;
    private float p2Score = 0;
    [SerializeField] private int winScore = 7;
    [SerializeField] private TextMeshProUGUI scoreText;
    private BallController _bc;
    public event Action<int> OnWin; // used int to pass 1 or 2 for player1 or player2

    void Awake()
    {
        _bc = FindAnyObjectByType<BallController>();
    }
    void OnEnable()
    {
        _bc.OnScore += ApplyScore;
        OnWin += ChangeState;
    }

    private void ChangeState(int obj)
    {

        GameManager.Instance.TransitionTo(GameManager.State.GameOver);
    }

    void OnDisable()
    {
        _bc.OnScore -= ApplyScore;
    }

    void ApplyScore(GameObject goal)
    {
        AudioManager.Instance.Play(AudioManager.SoundClip.Score);
        if (goal.CompareTag("LeftGoal"))
            p2Score += 1;
        else if (goal.CompareTag("RightGoal"))
            p1Score += 1;
        Debug.Log($"{p1Score}\t\t{p2Score}");
        if (p1Score == winScore || p2Score == winScore)
        {
            AudioManager.Instance.Play(AudioManager.SoundClip.Win);
            int winner = (p1Score > p2Score) ? 1 : 2; // 1 -> player1, 2 -> player2
            OnWin?.Invoke(winner);
            Debug.Log($"Player {winner} Wins!");
            p1Score = 0;
            p2Score = 0;
        }
    }

    void Update()
    {
        scoreText.text = $"{p1Score}\t\t{p2Score}";
    }
}
