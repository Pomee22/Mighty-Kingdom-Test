using System.Collections;
using UnityEngine;

/// <summary>
/// General counting timer that counts up.
/// </summary>
public class Timer : MonoBehaviour
{
    private float timer = 0.0f;
    private bool isCountingDown;

    public void StartCountdown()
    {
        // Set the duration
        timer = 0.0f;

        isCountingDown = true;

        //await CountdownAsync();
        StartCoroutine(CountdownActive());
    }

    public void StopCountDown()
    {
        isCountingDown = false;

        StopAllCoroutines();
    }

    public float GetTime()
    {
        return timer;
    }

    private IEnumerator CountdownActive()
    {
        // Decrease the time if we still have time left
        while (isCountingDown)
        {
            timer += Time.deltaTime;

            // Display time in seconds
            int time = (int)timer;
            UIManager.Instance.timerText.text = time.ToString();

            yield return null;
        }
    }
}
