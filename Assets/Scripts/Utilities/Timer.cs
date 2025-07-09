using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public int timer;

    private void Awake()
    {
        Debug.Log("Start Timer!");
        timer = 5;
        StartCoroutine(nameof(StartStopwatch));
    }

    private IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("Timer Completed!");
    }

    private IEnumerator StartStopwatch()
    {
        while (timer > 0)
        {
            yield return new WaitForSeconds(1f);
            timer--;
            Debug.Log("Time left: " + timer);
        }
        Debug.Log("Timer Completed!");
    }
}
