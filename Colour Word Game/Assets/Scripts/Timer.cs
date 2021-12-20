using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// General counting timer that counts up.
/// </summary>
public class Timer : MonoBehaviour
{
    public event Action onCompleteEvent;

    private float timer = 0.0f;
    private bool isCountingDown;

    public void Initialise(Action actionEvent)
    {
        onCompleteEvent = actionEvent;
    }

    public void StartCountUp()
    {
        Debug.Log("starting timer");
        // Set the duration
        timer = 0.0f;

        isCountingDown = true;

        //await CountdownAsync();
        StartCoroutine(CountUpActive());
    }

    public void StartCountDown(int duration)
    {
        timer = duration;
        isCountingDown = true;

        StartCoroutine(CountDownActive());
    }

    public void Stop()
    {
        isCountingDown = false;

        StopAllCoroutines();
    }

    public float GetTime()
    {
        return timer;
    }

    private IEnumerator CountUpActive()
    {
        // Decrease the time if we still have time left
        while (isCountingDown)
        {
            timer += Time.deltaTime;

            // Display time in seconds
            int time = (int)timer;

            yield return null;
        }

        onCompleteEvent?.Invoke();

        // Play on-complete event
    }

    private IEnumerator CountDownActive()
    {
        while(timer > 0.0f)
        {
            timer -= Time.deltaTime;

            yield return null;
        }

        Debug.Log("Passed null");

        onCompleteEvent?.Invoke();
    }
}
