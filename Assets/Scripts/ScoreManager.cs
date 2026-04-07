using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private float p1Score = 0;
    private float p2Score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    private BallController _bc;

    void Awake()
    {
        _bc = FindAnyObjectByType<BallController>();
    }
    void OnEnable()
    {
        _bc.OnScore += ApplyScore;
    }

    void OnDisable()
    {
        _bc.OnScore -= ApplyScore;
    }

    void ApplyScore(GameObject goal)
    {
        if (goal.CompareTag("LeftGoal"))
            p2Score += 1;
        else if (goal.CompareTag("RightGoal"))
            p1Score += 1;
        Debug.Log($"{p1Score}\t\t{p2Score}");
    }

    void Update()
    {
        scoreText.text = $"{p1Score}\t\t{p2Score}";
    }
}
