using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum State
    {
        Menu,
        Serve,
        Playing,
        Score,
        GameOver
    };
    public static GameManager Instance { get; private set; }

    public static State CurrentState { get; private set; }
    public event Action<State> OnStateChange;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        TransitionTo(State.Menu);
    }

    public void TransitionTo(State newState)
    {
        if (CurrentState == newState) return;
        CurrentState = newState;
        OnStateChange?.Invoke(newState);


        // NO NEED!!!
        switch (newState)
        {
            case State.Menu: MenuBehaviour(); break;
            case State.Serve: ServeBehaviour(); break;
            case State.Playing: PlayingBehaviour(); break;
            case State.Score: ScoreBehaviour(); break;
            case State.GameOver: GameOverBehaviour(); break;
        }

    }

    // NO NEED!!!
    void MenuBehaviour()
    {
        Time.timeScale = 0f;
    }

    void ServeBehaviour()
    {
        Time.timeScale = 1f;
    }

    void PlayingBehaviour()
    {
        Time.timeScale = 1f;
    }

    void ScoreBehaviour()
    {
        Time.timeScale = 1f;
    }

    void GameOverBehaviour()
    {
        Time.timeScale = 0f;
    }
}


// Why Not needed:
// All the state logics are done inside the class responsible for their states, no need to implement
// those logics here each class will handle them all based on just the current state that this will 
// let them know using event.