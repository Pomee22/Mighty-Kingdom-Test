using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float timer = 0.0f;
    private float duration;
    private bool isCountingDown;

    public async void StartCountdown(float duration)
    {
        // Begin countdown timer
        isCountingDown = true;

        // Set the duration
        timer = duration;
        this.duration = duration;

        //await CountdownAsync();
        StopAllCoroutines();
        StartCoroutine(CountdownActive());
    }

    public void StopCountdown()
    {
        // Stop countdown timer
        isCountingDown = false;
    }

    public float GetTime()
    {
        return timer;
    }

    private IEnumerator CountdownActive()
    {
        // Decrease the time if we still have time left
        while (timer > 0.0f /*&& isCountingDown*/)
        {
            if (!isCountingDown)
                break;

            timer -= Time.deltaTime;

            // Display time in seconds
            int time = (int)timer;
            UIManager.Instance.timerText.text = time.ToString();

            yield return null;
        }

        // Proceed to the next round if player doesn't select anything
        GameManager.Instance.NextRound();
    }
}
