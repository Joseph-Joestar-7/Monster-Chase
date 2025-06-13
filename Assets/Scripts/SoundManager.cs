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
        AudioClip newClip = null;

        if (SceneManager.GetActiveScene().name == "MainMenu" ||
            SceneManager.GetActiveScene().name == "GameOver")
        {
            newClip = mainMenuClip;
        }
        else if (SceneManager.GetActiveScene().name == "Gameplay")
        {
            newClip = gameplayClip;
        }
        // If it's the same clip, do nothing
        if (backgroundSource.clip == newClip && backgroundSource.isPlaying)
        {
            // Already playing this track
            return;
        }
        // Otherwise, restart with new
        backgroundSource.clip = newClip;
        backgroundSource.Play();
    }
}
