using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource musicSource;
    bool musicPaused;

    public PlayerHealth playerHealth;

    private void Awake()
    {
        musicSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Stop BGM on player music (it'll restart once the level reloads)
        if (playerHealth.CheckDead())
        {
            musicSource.Stop();
        }
        // PAUSE BGM when the game is paused
        else if (PauseManager.isPaused)
        {
            musicSource.Pause();
            musicPaused = true;
        }
        // UNPAUSE BGM when the game is unpaused (it should continue playing where it left off, not restart)
        else if (musicPaused)
        {
            musicSource.UnPause();
        }
    }
}
