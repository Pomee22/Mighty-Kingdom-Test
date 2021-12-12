using System.Threading.Tasks;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float currentTime = 0.0f;

    private bool isCountingDown;

    public async void StartCountdown()
    {
        // Reset the time
        currentTime = 0.0f;

        // Begin countdown timer
        isCountingDown = true;
        await CountDownActive();
    }

    public void StopCountdown()
    {
        // Stop countdown timer
        isCountingDown = false;
    }

    private async Task CountDownActive()
    {
        while(isCountingDown)
        {
            // Increment the time
            currentTime += Time.deltaTime;
            await Task.Yield();

            // Display time in seconds
            int time = (int)currentTime;
            UIManager.Instance.timerText.text = time.ToString();
        }
    }

    /// <summary>
    /// Returns time in seconds
    /// </summary>
    /// <returns></returns>
    public float GetTime()
    {
        return currentTime;
    }
}
