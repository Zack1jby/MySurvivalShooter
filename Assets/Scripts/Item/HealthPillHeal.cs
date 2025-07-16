using System;
using UnityEngine;

public class HealthPillHeal : MonoBehaviour
{
    private GameObject player;
    private PlayerHealth playerHealth;
    private int healAmount = 50;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Heal player then disappear
        if (other.gameObject == player)
        {
            playerHealth.RecoverHealth(healAmount);
            GameObject.Destroy(this.gameObject);
        }
    }
}
