using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public enum Enemy
{
    Zombunny,
    ZomBear,
    Hellephant
}

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
    private int spawnPointIndex;
    private bool nextSpawnSwapReady;
    private float spawnSwapTime = 10f;

    [SerializeField] private Enemy enemyType;
    private EnemyHealth enemyHealth;
    private EnemyAttack enemyAttack;
    private EnemyLoot enemyLoot;
    private NavMeshAgent navMeshAgent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyHealth = enemy.GetComponent<EnemyHealth>();
        enemyAttack = enemy.GetComponent<EnemyAttack>();
        enemyLoot = enemy.GetComponent<EnemyLoot>();
        navMeshAgent = enemy.GetComponent<NavMeshAgent>();
        // Assign enemy stats according to their types
        /*if (enemy.name == "Zombunny")
        {
            EnemyBuildZombunny();
        }
        else if (enemy.name == "ZomBear")
        {
            EnemyBuildZomBear();
        }
        else if (enemy.name == "Hellephant")
        {
            EnemyBuildHellephant();
        }*/
        switch(enemyType)
        {
            case Enemy.Zombunny:
                EnemyBuildZombunny();
                break;
            case Enemy.ZomBear:
                EnemyBuildZomBear();
                break;
            case Enemy.Hellephant:
                EnemyBuildHellephant();
                break;
        }
        // Select a random starting spawn
        spawnPointIndex = Random.Range(0, spawnPoints.Length);

        InvokeRepeating("Spawn", spawnTime, spawnTime);
        InvokeRepeating("ChooseNewSpawn", spawnSwapTime, spawnSwapTime);
    }

    void Spawn()
    {
        if(playerHealth.currentHealth <= 0)
        {
            return;
        }

        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }

    // Randomly select a DIFFERENT spawn point posistion from spawnPoints[]
    void ChooseNewSpawn()
    {
        int newSpawnPointIndex = Random.Range(0, spawnPoints.Length);
        if (newSpawnPointIndex != spawnPointIndex)
        {
            spawnPointIndex = newSpawnPointIndex;
        }
        else
        {
            ChooseNewSpawn();
        }
    }

    // Distribute enemy stats, loot odds, and spawn speed depending on its type
    void EnemyBuildZombunny()
    {
        enemyHealth.scoreValue = 10;
        enemyHealth.startingHealth = 40;
        enemyAttack.attackDamage = 10;
        navMeshAgent.speed = 3.5f;
        enemyLoot.healthDropOdds = 20;
        enemyLoot.scoreDropOdds = 0;
        spawnTime = 3f;
    }

    void EnemyBuildZomBear()
    {
        enemyHealth.scoreValue = 30;
        enemyHealth.startingHealth = 70;
        enemyAttack.attackDamage = 30;
        navMeshAgent.speed = 2;
        enemyLoot.healthDropOdds = 40;
        enemyLoot.scoreDropOdds = 10;
        spawnTime = 4f;
    }

    void EnemyBuildHellephant()
    {
        enemyHealth.scoreValue = 50;
        enemyHealth.startingHealth = 150;
        enemyAttack.attackDamage = 50;
        navMeshAgent.speed = 1;
        enemyLoot.healthDropOdds = 50;
        enemyLoot.scoreDropOdds = 20;
        spawnTime = 8f;
    }
}
