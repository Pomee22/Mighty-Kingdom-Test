using System;
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
    [Header("Time")]
    [Space]
    [Tooltip("The time it takes for the game to begin")]
    public int time = 3;

    [SerializeField] private Timer timer;

    public event Action OnCompleteEvent;

    private TextMeshProUGUI timeText;

    [Header("Animation")]
    [Space]
    public float endFont = 24;
    public float startFont = 34;

    public Color32 startColour;
    public Color32 endColour;

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

        timeText.faceColor = startColour;
        timeText.fontSize = startFont;
    }

    public async void StartCountDown()
    {
        Debug.Log("StartCountDown");

        gameObject.SetActive(true);

        // Send in the on-complete event
        timer.StartCountDown(time);

        await FadeOutText();
    }

    private async Task FadeOutText()
    {
        int prevTime = (int)timer.GetTime();

        while (timer.GetTime() > 0.0f)
        {
            int currentTime = (int)timer.GetTime();

            // Display the time in seconds
            int timeRemaining = (int)timer.GetTime();
            timeText.text = timeRemaining.ToString();

            // Display "start" when the timer reaches 0
            if ((int)timer.GetTime() == 0)
                timeText.text = "START";

            if (currentTime < prevTime)
            {
                // Reset the font size and colour
                timeText.faceColor = startColour;
                timeText.fontSize = startFont;

                prevTime = currentTime;
            }

            timeText.fontSize = Mathf.Lerp(timeText.fontSize, endFont, 0.01f);
            timeText.faceColor = Color32.Lerp(timeText.faceColor, endColour, 0.01f);

            await Task.Yield();
        }
    }
}