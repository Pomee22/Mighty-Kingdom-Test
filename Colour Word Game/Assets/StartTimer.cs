using System;
using System.Collections;
using UnityEngine;
using TMPro;

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

        //GameManager.Instance.SetUpGameLevel();

        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        timer.Initialise(OnCompleteEvent);
    }

    private void Update()
    {
        int timeRemaining = (int)timer.GetTime() + 1;
        timeText.text = timeRemaining.ToString();
    }

    public void StartCountDown()
    {
        Debug.Log("StartCountDown");

        gameObject.SetActive(true);

        // Send in finish event
        timer.StartCountDown(time);
    }
}
