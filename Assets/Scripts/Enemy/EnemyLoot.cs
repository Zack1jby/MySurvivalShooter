using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
    public GameObject healthDrop;
    public GameObject scoreDrop;
    public int healthDropOdds;
    public int scoreDropOdds;

    public void DropLoot()
    {
        // Random chance to spawn ONE or none of the following loot drops
        int lootCheck = Random.Range(1, 100);

        if (lootCheck <= scoreDropOdds)
        {
            Instantiate(scoreDrop, transform.position + new Vector3(0, .15f), transform.rotation);
        }
        else if (lootCheck <= healthDropOdds)
        {
            Instantiate(healthDrop, transform.position + new Vector3(0, .15f), transform.rotation);
        }
    }
}
