using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
    public GameObject healthDrop;
    public GameObject scoreDrop;
    public int healthDropOdds;
    public int scoreDropOdds;
    public GameObject player;

    [SerializeField] private float groundOffset;
    private HealthPillHeal healthPillHeal;
    private ScoreBoxBonus scoreBoxBonus;

    private void Awake()
    {
        healthPillHeal = healthDrop.GetComponent<HealthPillHeal>();
        scoreBoxBonus = scoreDrop.GetComponent<ScoreBoxBonus>();
    }

    public void DropLoot()
    {
        // Random chance to spawn ONE or none of the following loot drops
        int lootCheck = Random.Range(1, 100);

        if (lootCheck <= scoreDropOdds)
        {
            scoreBoxBonus.player = player;
            Instantiate(scoreDrop, transform.position + new Vector3(0, groundOffset), transform.rotation);
        }
        else if (lootCheck <= healthDropOdds)
        {
            healthPillHeal.player = player;
            Instantiate(healthDrop, transform.position + new Vector3(0, groundOffset), transform.rotation);
        }
    }
}
