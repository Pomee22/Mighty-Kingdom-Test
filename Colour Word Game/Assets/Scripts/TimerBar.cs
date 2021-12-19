using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour
{
    [SerializeField] private Image timerBar;
    [SerializeField] private Timer roundTimer;

    private float minTime, midTime;
    private float timeRemaining;

    private void Start()
    {
        minTime = GameManager.Instance.roundDuration / 3; // 1/3 of duration
        midTime = (GameManager.Instance.roundDuration * 2) / 3; // 2/3 of duration

        roundTimer.StartCountDown(GameManager.Instance.roundDuration);
    }

    private void Update()
    {
        timeRemaining = /*GameManager.Instance.roundDuration - */roundTimer.GetTime();

        // Update the fillamount so the bar decreases overtime
        timerBar.fillAmount = timeRemaining / GameManager.Instance.roundDuration;

        UpdateColour(timeRemaining);

        // Proceed to the next round if player runs out of time
        if (timeRemaining <= 0)
        {
            GameManager.Instance.NextRound();
        }
    }

    public void Reset()
    {
        roundTimer.Stop();
        roundTimer.StartCountDown(GameManager.Instance.roundDuration);
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

        timerBar.color = newColour;
    }
}