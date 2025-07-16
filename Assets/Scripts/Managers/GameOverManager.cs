using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public float restartDelay = 5f;
    
    private Animator anim;
    private float restartTimer;
    [SerializeField] private string GameOverTrigger;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger(GameOverTrigger);

            restartTimer += Time.deltaTime;

            if(restartTimer >= restartDelay)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
