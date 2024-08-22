using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameManager gameManager;
    public int minutes;
    public int seconds;
    public int miliseconds;

    // Update is called once per frame
    void Update()
    {
        if (miliseconds >= 100)
        {
            seconds++;
            miliseconds = 0;
        }

        if (seconds >= 59)
        {
            minutes++;
            seconds = 0;
        }
    }

    IEnumerator Stopwatch()
    {
        while (gameManager.isGameRunning)
        {
            miliseconds += 1;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void StartTimer()
    {
        StartCoroutine(Stopwatch());
    }

    public void TimerReset()
    {
        seconds = 0;
        minutes = 0;
        miliseconds = 0;
    }
}
