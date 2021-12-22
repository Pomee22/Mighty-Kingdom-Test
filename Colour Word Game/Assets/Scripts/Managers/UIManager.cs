using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject menuScreen;
    public GameObject gamemodeScreen;

    public GameObject startContent;
    public GameObject gameContent;
    public GameObject endContent;

    public TextMeshProUGUI timeTakenText;
    public TextMeshProUGUI timeScoreText;

    public TextMeshProUGUI colourWord;
    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI descriptionText;

    public Timer startTimer;
    public TextMeshProUGUI startTimerText;

    public TextMeshProUGUI gameTimer;

    private void Awake()
    {
        if (Instance)
            Destroy(Instance);

        Instance = this;
    }

    /// <summary>
    /// Button onclick event.
    /// Starts the game.
    /// </summary>
    public void StartGame()
    {
        //startTimer.StartCountDown()

        startContent.SetActive(false);

        gameContent.SetActive(true);
    }

    public async Task Transition()
    {
        await Task.Yield();
    }

    public void UpdateStartContent(bool status)
    {
        startContent.SetActive(status);
    }

    public void DisplayEndGame(int score, int timeTaken, int timeScore)
    {
        scoreText.text = score.ToString();
        timeTakenText.text = timeTaken.ToString();
        timeScoreText.text = timeScore.ToString();

        endContent.SetActive(true);
        gameContent.SetActive(false);
    }

    public void EnterGameModeScreen()
    {
        menuScreen.SetActive(false);
        gamemodeScreen.SetActive(true);
    }
}
