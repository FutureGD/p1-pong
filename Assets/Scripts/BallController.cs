using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] float speed = 6f;
    private Rigidbody2D rb;
    private Vector2 movement;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        AddStartingSpeed();
    }

    private void AddStartingSpeed()
    {
        Vector2 dir = new Vector2(
            Random.value < 0.5f ? -1.0f : 1.0f,
            Random.value < 0.5f ? Random.Range(-1.0f, -0.5f) : Random.Range(0.5f, 1.0f)
        );

        rb.linearVelocity = dir * speed;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Paddle"))
        {
            // Debug.Log("Paddle Hit");
            speed += 2;
            // OnPaddleHit?.Invoke();
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            // Debug.Log("Wall Bounce");
            // OnWallHit?.Invoke();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LeftGoal"))
            Debug.Log("AI Score +1");
        else if (other.CompareTag("RightGoal"))
            Debug.Log("Player Score +1");
    }
}
