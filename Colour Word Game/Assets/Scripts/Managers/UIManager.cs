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

    public TextMeshProUGUI colourWord;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI answersText;

    public TextMeshProUGUI descriptionText;

    public Timer startTimer;
    public TextMeshProUGUI startTimerText;

    private void Awake()
    {
        if (Instance)
            Destroy(Instance);

        Instance = this;
    }

    public void StartMenu()
    {
        endContent.SetActive(false);

        startContent.SetActive(true);
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

    public void UpdateStartContent(bool status)
    {
        startContent.SetActive(status);
    }

    public void DisplayEndGame(int score, int timeTaken)
    {
        scoreText.text = score.ToString();
        timeTakenText.text = timeTaken.ToString();

        answersText.text = $"{GameManager.Instance.CorrectAnswerCounter} / {GameManager.Instance.rounds}";

        endContent.SetActive(true);
        gameContent.SetActive(false);
    }
}
