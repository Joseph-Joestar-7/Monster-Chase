using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioClip mainMenuClip;
    [SerializeField] private AudioClip gameplayClip;

    private AudioSource backgroundSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        backgroundSource = GetComponent<AudioSource>();

        if (backgroundSource == null)
        {
            backgroundSource = gameObject.AddComponent<AudioSource>();
        }
        backgroundSource.loop = true;

        PlayMusicForCurrentScene();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForCurrentScene();
    }
    private void PlayMusicForCurrentScene()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "GameOver")
        {
            backgroundSource.clip = mainMenuClip;
        }
        else if (SceneManager.GetActiveScene().name == "Gameplay")
        {
            backgroundSource.clip = gameplayClip;
        }
        backgroundSource.volume = 0.2f;
        backgroundSource.Play();
    }
}
