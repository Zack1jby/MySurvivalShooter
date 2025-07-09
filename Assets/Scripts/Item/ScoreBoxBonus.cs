using UnityEngine;

public class ScoreBoxBonus : MonoBehaviour
{
    GameObject player;
    int scoreBonus = 50;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Add scoreBonus to score then disappear
        if (other.gameObject == player)
        {
            ScoreManager.score += scoreBonus;
            ScoreManager.Instance.ShowScore();
            GameObject.Destroy(this.gameObject);
        }
    }
}
