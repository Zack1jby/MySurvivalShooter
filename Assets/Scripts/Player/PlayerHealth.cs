using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, .1f);

    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;
    public bool isInvincible;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
        currentHealth = startingHealth;
    }

    void Update()
    {
        /*if (playerMovement.isDashing)
        {
            isInvincible = true;
        }
        else
        {
            isInvincible = false;
        }*/
        // If the player character is dashing, make them invincible. If not, make them vunerable.
        isInvincible = playerMovement.isDashing ? true : false;

        if(damaged)
        {
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    public void TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        playerAudio.Play();
        if(currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    public void RecoverHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > startingHealth) currentHealth = startingHealth;
        healthSlider.value = currentHealth;
    }

    private void Death()
    {
        isDead = true;

        playerShooting.DisableEffects();

        anim.SetTrigger("Die");
        playerAudio.clip = deathClip;
        playerAudio.Play(); // plays "death"
        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }

    public bool CheckDead()
    {
        return isDead;
    }

    public void RestartLevel()
    {
        Debug.Log("Restart Level");
    }
}
