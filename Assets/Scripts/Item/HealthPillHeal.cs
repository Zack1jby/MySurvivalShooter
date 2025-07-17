using System;
using UnityEngine;

public class HealthPillHeal : MonoBehaviour
{
    public GameObject player;
    private PlayerHealth playerHealth;
    private int healAmount = 50;

    void Start()
    {
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
