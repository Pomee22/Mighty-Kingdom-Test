using System;
using System.Collections;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

/// <summary>
/// Used to convey to the player that the 
/// game is about to start by counting down 
/// the time
/// </summary>
public class StartTimer : MonoBehaviour
{
    public int time = 3;

    [SerializeField] private Timer timer;

    public event Action OnCompleteEvent;

    private TextMeshProUGUI timeText;

    private void Awake()
    {
        timeText = GetComponentInChildren<TextMeshProUGUI>();
        Debug.Assert(timeText != null, "tmp text is null!");
    }

    private void OnEnable()
    {
        OnCompleteEvent += StartTimer_onCompleteEvent;
    }

    private void OnDisable()
    {
        OnCompleteEvent -= StartTimer_onCompleteEvent;
    }

    private void StartTimer_onCompleteEvent()
    {
        Debug.Log("Completed timer event!");

        gameObject.SetActive(false);
    }

    void Start()
    {
        timer.Initialise(OnCompleteEvent);
    }

    public async void StartCountDown()
    {
        Debug.Log("StartCountDown");

        gameObject.SetActive(true);

        // Send in the on-complete event
        timer.StartCountDown(time);

        await UpdateUI();
    }

    /// <summary>
    /// Updates it's associated text while
    /// the timer is active.
    /// </summary>
    /// <returns></returns>
    private async Task UpdateUI()
    {
        while(timer.GetTime() > 0.0f)
        {
            // Display the time in seconds
            int timeRemaining = (int)timer.GetTime();
            timeText.text = timeRemaining.ToString();

            // Display "start" when the timer reaches 0
            if ((int)timer.GetTime() == 0)
                timeText.text = "START";

            await Task.Yield();
        }
    }
}
