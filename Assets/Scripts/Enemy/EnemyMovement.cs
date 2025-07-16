using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private Transform player;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;
    private NavMeshAgent navAgent;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            navAgent.SetDestination(player.position);
        }
        else
        {
            navAgent.enabled = false;
        }
    }
}
