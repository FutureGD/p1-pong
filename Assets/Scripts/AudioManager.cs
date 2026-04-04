using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum SoundClip { WallBounce, PaddleBounce, Score, Win, Click };
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioClip bounceWall;
    [SerializeField] private AudioClip bouncePaddle;
    [SerializeField] private AudioClip score;
    [SerializeField] private AudioClip win;
    [SerializeField] private AudioClip click;

    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Awake()
    {
        if (Instance != null || Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Play(SoundClip clipName)
    {
        AudioClip clip = null;
        switch (clipName)
        {
            case SoundClip.WallBounce:
                clip = bounceWall;
                break;

            case SoundClip.PaddleBounce:
                clip = bouncePaddle;
                break;

            case SoundClip.Score:
                clip = score;
                break;

            case SoundClip.Win:
                clip = win;
                break;

            case SoundClip.Click:
                clip = click;
                break;
        }

        if (clip == null) return;

        _audioSource.PlayOneShot(clip);
    }
}
