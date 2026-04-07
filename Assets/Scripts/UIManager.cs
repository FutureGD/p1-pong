using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameManager _gm;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject alwaysActivePanel;
    [SerializeField] private GameObject gameOverPanel;


    private void Awake()
    {
        _gm = FindAnyObjectByType<GameManager>();
    }

    private void OnEnable()
    {
        _gm.OnStateChange += UIEnableThroughState;
    }


    private void Start()
    {
        menuPanel.SetActive(true);
        alwaysActivePanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    private void UIEnableThroughState(GameManager.State state)
    {
        if (state == GameManager.State.Menu)
        {
            menuPanel.SetActive(true);
            alwaysActivePanel.SetActive(false);
            gameOverPanel.SetActive(false);
        }
        else if (state == GameManager.State.Serve)
        {
            menuPanel.SetActive(false);
            alwaysActivePanel.SetActive(true);
            gameOverPanel.SetActive(false);
        }
        else if (state == GameManager.State.GameOver)
        {
            gameOverPanel.SetActive(true);
            menuPanel.SetActive(false);
            alwaysActivePanel.SetActive(false);
        }
    }

    public void Play()
    {
        AudioManager.Instance.Play(AudioManager.SoundClip.Click);
        Debug.Log("Play");
        GameManager.Instance.TransitionTo(GameManager.State.Serve);
    }

    public void Quit()
    {
        AudioManager.Instance.Play(AudioManager.SoundClip.Click);
        Debug.Log("Quit");
        Application.Quit();
    }

    public void MainMenu()
    {
        AudioManager.Instance.Play(AudioManager.SoundClip.Click);
        Debug.Log("MainMenu");
        GameManager.Instance.TransitionTo(GameManager.State.Menu);
    }

    public void Restart()
    {
        AudioManager.Instance.Play(AudioManager.SoundClip.Click);
        Debug.Log("Restart");
        GameManager.Instance.TransitionTo(GameManager.State.Serve);
    }
}
