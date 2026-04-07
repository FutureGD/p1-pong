using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BallController : MonoBehaviour
{

    [SerializeField] float speed = 6f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float startingSpeed;
    private bool isResetting;
    public event Action OnPaddleHit;
    public event Action OnWallHit;
    public event Action<GameObject> OnScore;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        startingSpeed = speed;
        AddStartingSpeed();
    }

    private void AddStartingSpeed()
    {
        Vector2 dir = new Vector2(
            UnityEngine.Random.value < 0.5f ? -1.0f : 1.0f,
            UnityEngine.Random.value < 0.5f ? UnityEngine.Random.Range(-1.0f, -0.5f) : UnityEngine.Random.Range(0.5f, 1.0f)
        );

        rb.linearVelocity = dir.normalized * speed;
    }

    private void FixedUpdate()
    {
        if (!isResetting)
        {
            // Safety: prevent perfectly horizontal loop
            if (Mathf.Abs(rb.linearVelocity.y) < 0.5f)
            {
                float nudge = UnityEngine.Random.Range(0.5f, 1f) * Mathf.Sign(rb.linearVelocity.y == 0 ?
               1f : rb.linearVelocity.y);
                rb.linearVelocity += new Vector2(0f, nudge);
                rb.linearVelocity = rb.linearVelocity.normalized * speed;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Paddle"))
        {
            // Debug.Log("Paddle Hit");
            rb.linearVelocity *= (speed + 0.2f) / speed;
            speed += 0.2f;
            OnPaddleHit?.Invoke();
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            // Debug.Log("Wall Bounce");
            OnWallHit?.Invoke();
        }
    }

    // GoalTrigger
    void OnTriggerEnter2D(Collider2D other)
    {
        OnScore?.Invoke(other.gameObject);
        rb.linearVelocity = Vector2.zero;
        StartCoroutine("WaitAndResetPosition", 3);
    }

    IEnumerator WaitAndResetPosition(int n)
    {
        isResetting = true;
        transform.position = Vector3.zero;
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(n);
        speed = startingSpeed;
        // StartCoroutine("Wait", 3);
        AddStartingSpeed();
        isResetting = false;
    }

}
