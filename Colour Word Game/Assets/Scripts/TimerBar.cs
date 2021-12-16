using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour
{
    [SerializeField] private Image timeBar;

    [SerializeField] private Timer timerT;

    private float timer = 0.0f;
    private float duration;
    private bool isCountingDown;

    private float minTime, midTime;

    private void Start()
    {
        //timerT = new Timer();
        //Debug.Assert(timerT != null, "Timer is null!");

        minTime = GameManager.Instance.roundDuration / 3; // 1/3 of duration
        midTime = (GameManager.Instance.roundDuration * 2) / 3; // 2/3 of duration

        timerT.StartCountdown(GameManager.Instance.roundDuration);
    }

    private void Update()
    {
        // Update the fillamount, so the bar decreases overtime
        timeBar.fillAmount = timerT.GetTime() / GameManager.Instance.roundDuration;

        UpdateColour(timerT.GetTime());
    }

    public void Reset()
    {
        //timerT.StopCountdown();
        timerT.StartCountdown(GameManager.Instance.roundDuration);
    }


    //public async void StartCountdown(float duration)
    //{
    //    // Begin countdown timer
    //    isCountingDown = true;

    //    // Set the duration
    //    timer = duration;
    //    this.duration = duration;

    //    minTime = timer / 3; // 1/3 of duration
    //    midTime = (timer * 2) / 3; // 2/3 of duration

    //    //await CountdownAsync();
    //    StopAllCoroutines();
    //    StartCoroutine(CountdownActive());
    //}

    public void StopCountdown()
    {
        // Stop countdown timer
        isCountingDown = false;
    }

    public float GetTime()
    {
        return timer;
    }

    //private async Task CountdownAsync()
    //{
    //    // Decrease the time if we still have time left
    //    while (timer > 0.0f /*&& isCountingDown*/)
    //    {
    //        if (!isCountingDown)
    //            break;

    //        timer -= Time.deltaTime;

    //        // Display time in seconds
    //        int time = (int)timer;
    //        UIManager.Instance.timerText.text = time.ToString();

    //        // Update the fillamount, so the bar decreases overtime
    //        timeBar.fillAmount = timer / duration;

    //        UpdateColour(timer);

    //        await Task.Yield();
    //    }
    //}

    //private IEnumerator CountdownActive()
    //{
    //    // Decrease the time if we still have time left
    //    while (timer > 0.0f /*&& isCountingDown*/)
    //    {
    //        if (!isCountingDown)
    //            break;

    //        timer -= Time.deltaTime;

    //        // Display time in seconds
    //        int time = (int)timer;
    //        UIManager.Instance.timerText.text = time.ToString();

    //        // Update the fillamount, so the bar decreases overtime
    //        timeBar.fillAmount = timer / duration;

    //        UpdateColour(timer);

    //        yield return null;
    //    }

    //    // Proceed to the next round if player doesn't select anything
    //    GameManager.Instance.NextRound();
    //}

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
