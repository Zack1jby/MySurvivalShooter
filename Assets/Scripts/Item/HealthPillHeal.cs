using System;
using UnityEngine;

public class HealthPillHeal : MonoBehaviour
{
    GameObject player;
    PlayerHealth playerHealth;

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
            playerHealth.RecoverHealth(50);
            GameObject.Destroy(this.gameObject);
        }
    }
}
