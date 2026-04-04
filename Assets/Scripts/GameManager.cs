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

    public static State CurrentState { get; private set; }
    public event Action<State> OnStateChange;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        TransitionTo(State.Menu);
    }

    void TransitionTo(State newState)
    {
        if (CurrentState == newState) return;
        CurrentState = newState;
        OnStateChange?.Invoke(newState);

        switch (newState)
        {
            case State.Menu: MenuBehaviour(); break;
            case State.Serve: ServeBehaviour(); break;
            case State.Playing: PlayingBehaviour(); break;
            case State.Score: ScoreBehaviour(); break;
            case State.GameOver: GameOverBehaviour(); break;
        }

    }

    void MenuBehaviour()
    {

    }

    void ServeBehaviour()
    {

    }

    void PlayingBehaviour()
    {

    }

    void ScoreBehaviour()
    {

    }

    void GameOverBehaviour()
    {

    }
}
