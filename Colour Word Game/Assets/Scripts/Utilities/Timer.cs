using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// General timer class that 
/// can perform either a countdown or countup
/// </summary>
public class Timer : MonoBehaviour
{
    public event Action onCompleteEvent;    

    private float timer = 0.0f;
    private bool isCountingDown;

    /// <summary>
    /// Assigns a delegate to the variable.
    /// </summary>
    /// <param name="actionEvent"></param>
    public void Initialise(Action actionEvent)
    {
        onCompleteEvent = actionEvent;
    }

    public void StartCountUp()
    {
        // Set the duration
        timer = 0.0f;

        isCountingDown = true;

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

        // Stops all timer/s
        StopAllCoroutines();
    }

    public float GetTime()
    {
        return timer;
    }

    private IEnumerator CountUpActive()
    {
        // Counts up the timer until it is stopped
        while (isCountingDown)
        {
            timer += Time.deltaTime;

            yield return null;
        }

        // Play the delegate if it has been assigned
        onCompleteEvent?.Invoke();
    }

    private IEnumerator CountDownActive()
    {
        // Decrease the time if we still have time left
        while (timer > 0.0f)
        {
            timer -= Time.deltaTime;

            yield return null;
        }

        // Play the delegate if it has been assigned
        onCompleteEvent?.Invoke();
    }
}
