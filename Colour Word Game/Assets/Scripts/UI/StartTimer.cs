using System;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

/// <summary>
/// Used to convey to the player that the game 
/// is about to start by counting down the time
/// </summary>
public class StartTimer : MonoBehaviour
{
    public event Action OnCompleteEvent;

    [Header("Class References")]
    [Space]
    [SerializeField] private Timer timer;

    [Header("Time")]
    [Space]
    [Tooltip("The time it takes for the game to begin")]
    public int duration = 3;

    [Header("Animation")]
    [Space]
    [SerializeField] private float startFont = 34;
    [SerializeField] private float endFont = 24;
    [SerializeField] private Color32 startColour;
    [SerializeField] private Color32 endColour;

    private TextMeshProUGUI timeText;

    private void Awake()
    {
        timeText = GetComponentInChildren<TextMeshProUGUI>();
        Debug.Assert(timeText != null, "tmp text is null!");
    }

    private void OnEnable()
    {
        OnCompleteEvent += StartTimer_OnCompleteEvent;

        // Assign the delegate to the timer
        timer.Initialise(OnCompleteEvent);

        // Init text to start values
        timeText.faceColor = startColour;
        timeText.fontSize = startFont;
    }

    private void OnDisable()
    {
        OnCompleteEvent -= StartTimer_OnCompleteEvent;
    }

    private void StartTimer_OnCompleteEvent()
    {
        gameObject.SetActive(false);
    }

    public async void StartCountDown()
    {
        // Enable the countdown text
        gameObject.SetActive(true);

        timer.StartCountDown(duration);

        // Play the fade out effect
        await FadeOutText();
    }

    private async Task FadeOutText()
    {
        // Record the previous time
        int prevTime = (int)timer.GetTime();

        // Play sound
        SoundManager.Instance.Play(SoundManager.Sound.COUNTDOWN);

        bool reachedEndCounter = false;

        while (timer.GetTime() > 0.0f)
        {
            // Get the latest time
            int currentTime = (int)timer.GetTime();

            // Display the time in seconds
            int timeRemaining = (int)timer.GetTime();
            timeText.text = timeRemaining.ToString();

            // Display "start" when the timer reaches 0
            if ((int)timer.GetTime() == 0)
            {
                timeText.text = "START";

                if (!reachedEndCounter)
                {
                    // Play sound 
                    SoundManager.Instance.Play(SoundManager.Sound.LASTCOUNTDOWN);
                    reachedEndCounter = true;
                }
            }

            // Checks if the current time has gone down by an integer
            if (currentTime < prevTime)
            {
                // Reset the font size and colour
                timeText.faceColor = startColour;
                timeText.fontSize = startFont;

                prevTime = currentTime;

                if((int)timer.GetTime() > 0)
                {
                    // Play sound
                    SoundManager.Instance.Play(SoundManager.Sound.COUNTDOWN);
                }
            }

            // Lerp the texts' font size and colour for a fade-out effect
            timeText.fontSize = Mathf.Lerp(timeText.fontSize, endFont, 0.01f);
            timeText.faceColor = Color32.Lerp(timeText.faceColor, endColour, 0.01f);

            await Task.Yield();
        }
    }
}