using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour
{
    [SerializeField] private Image timeBar;

    private float timer = 0.0f;
    private float duration;
    private bool isCountingDown;

    private float minTime, midTime;

    public async void StartCountdown(float duration)
    {
        // Begin countdown timer
        isCountingDown = true;

        // Set the duration
        timer = duration;
        this.duration = duration;

        minTime = timer / 3; // 1/3 of duration
        midTime = (timer * 2) / 3; // 2/3 of duration

        await CountdownAsync();
    }

    public void StopCountdown()
    {
        // Stop countdown timer
        isCountingDown = false;
    }

    private async Task CountdownAsync()
    {
        // Decrease the time if we still have time left
        while (timer > 0.0f /*&& isCountingDown*/)
        {
            Debug.Log("Counting down " + timer);

            timer -= Time.deltaTime;

            // Display time in seconds
            int time = (int)timer;
            UIManager.Instance.timerText.text = time.ToString();

            isCountingDown = false;

            // Update the fillamount, so the bar decreases overtime
            timeBar.fillAmount = timer / duration;

            if (isCountingDown)
                break;

            UpdateColour(timer);

            await Task.Yield();
        }
    }

    private void UpdateColour(float time)
    {
        Color newColour = Color.yellow;

        // If time is more than 2 / 3 of the duration
        if (time > midTime)
        {
            newColour = Color.yellow;
        }
        // If time is more than 1 / 3 of the duration 
        // and less than 2 /3 of the duration
        else if (time < midTime && time > minTime)
        {
            newColour = new Color(1.0f, 0.64f, 0.0f);
        }
        // If time is less than 2 / 3 of the duration
        else
        {
            newColour = Color.red;
        }

        timeBar.color = newColour;
    }
}
