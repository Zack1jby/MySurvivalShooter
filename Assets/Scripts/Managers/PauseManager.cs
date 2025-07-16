using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject PauseMenu;
    public PlayerHealth playerHealth;
    public static bool isPaused;

    private Canvas pauseCanvas;

    private void Awake()
    {
        pauseCanvas = PauseMenu.GetComponent<Canvas>();
        pauseCanvas.enabled = false;
    }
    
    void Update()
    {
        // Pause when the ESC key is pressed, but don't pause if the game is already paused or the player character is dead
        if (Input.GetKeyDown(KeyCode.Escape) && !(isPaused || playerHealth.CheckDead()))
        {
            Pause();
        }
        // Unpause the game when the ESC key is pressed if it's already paused
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            UnPause();
        }
    }

    void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseCanvas.enabled = true;
    }

    public void UnPause()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseCanvas.enabled = false;
    }
}
