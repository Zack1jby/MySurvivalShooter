using UnityEngine;

public class ScoreBoxBonus : MonoBehaviour
{
    public GameObject player;
    int scoreBonus = 50;

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
