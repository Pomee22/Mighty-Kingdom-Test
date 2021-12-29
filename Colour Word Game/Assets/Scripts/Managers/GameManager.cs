using System;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int CorrectAnswerCounter { get; set; }

    [Header("Class References")]
    [Space]
    [SerializeField] private ColourWordManager colourWordManager;
    [SerializeField] private StartTimer startTimer;
    [SerializeField] private Timer gameTimer; 

    [Header("Rounds")]
    [Space]
    [Tooltip("The number of rounds the game consists of.")]
    public int numberOfRounds = 5;
    private int currentRound = 0;

    [Header("Scoring")]
    [Space]
    [Tooltip("The max time-based points awarded to the player per round.")]
    [SerializeField] private int roundTimePoints = 5;
    [Tooltip("The score awarded to the player per correct answer.")]
    [SerializeField] private int points = 5;
    private int score = 0;

    public enum GameState
    {
        START,
        GAME,
        END
    };

    private GameState curretState;

    private void Awake()
    {
        if (Instance)
            Destroy(Instance);
        
        Instance = this;
    }

    void Start()
    {
        // Initialise the game state to start
        UpdateGameState(GameState.START);
    }

    public void UpdateGameState(GameState state)
    {
        // Change the current game state with a new state
        curretState = state;

        // Update game on game state change
        switch(curretState)
        {
            case GameState.START:
                SetUpStartScreen();
                break;                
            case GameState.GAME:
                SetUpGameScreen();
                break;
            case GameState.END:
                SetUpEndScreen();
                break;
        }
    }

    #region button_OnClick_Events
    // Called by Unity onclick event.
    // Gets invoked when player selects
    // any one of the given colour options
    public void ColourSelected(ColourOption colourOption)
    {
        // If the player selected the correct colour...
        if (ColourWordManager.Instance.CompareColours(colourOption))
        {
            // Increment score
            score += points;
            CorrectAnswerCounter++;

            // Play correct sound
            SoundManager.Instance.Play(SoundManager.Sound.BUTTONCLICKPOS);
        }
        else
        {
            // Play incorrect sound
            SoundManager.Instance.Play(SoundManager.Sound.BUTTONCLICKNEG);
        }

        NextRound();
    }

    /// <summary>
    /// Button onclick event.
    /// Sets up the menu.
    /// </summary>
    public void StartMenu()
    {
        // Play sound
        SoundManager.Instance.Play(SoundManager.Sound.BUTTONCLICK);

        UpdateGameState(GameState.START);
    }

    /// <summary>
    /// Button onclick event.
    /// Starts the game.
    /// </summary>
    public void StartGame()
    {
        // Play sound
        SoundManager.Instance.Play(SoundManager.Sound.BUTTONCLICK);

        UpdateGameState(GameState.GAME);
    }

    #endregion

    private void SetUpStartScreen()
    {
        UIManager.Instance.StartMenu();
    }

    public async void SetUpGameScreen()
    {
        // Reset game value
        currentRound = 0;
        score = 0;
        CorrectAnswerCounter = 0;

        await Countdown();

        colourWordManager.Initialise();

        // Wait for the rransition into the game scene
        UIManager.Instance.StartGame();

        // Begin the timer after waiting 
        gameTimer.StartCountUp();
    }

    private async Task Countdown()
    {
        // Delay for one second
        await Task.Delay(1000);

        // Commence countdown
        startTimer.StartCountDown();
        await Task.Delay(startTimer.time * 1000);
    }

    private void StartGameLevel()
    {
        // Randomise colour word 
        colourWordManager.Initialise();

        // Set scene
        SetUpGameScreen();
    }

    private void SetUpEndScreen()
    {
        // Stop the game timer
        gameTimer.Stop();

        // Round the timer as an int
        int timeTaken = (int)Math.Round(gameTimer.GetTime(), 0);
        int timeScore = GetTimeScore(timeTaken);

        // Add time bonus points to final score
        score += timeScore;

        // Update ui score
        UIManager.Instance.DisplayEndScreen(score, timeTaken);
    }

    /// <summary>
    /// Calculates and returns the time-based score
    /// The time score is considered only if the player
    /// selects the correct answer/s
    /// </summary>
    /// <param name="timeTaken"></param>
    /// <returns></returns>
    private int GetTimeScore(int timeTaken)
    {
        int resultantTimeScore = 0;

        // Calculate the timeScore and split it according to the amount of rounds
        int timeScore = (roundTimePoints * numberOfRounds) - timeTaken;
        timeScore /= numberOfRounds;

        // If the answer for the round was correct...
        for (int i = 0; i < CorrectAnswerCounter; i++)
        {
            // Add the time bonus point
            resultantTimeScore += timeScore;        
        }

        return resultantTimeScore;
    }

    private void NextRound()
    {
        currentRound++;

        // If this was the last round...
        if (currentRound == numberOfRounds)
            UpdateGameState(GameState.END);
        else
        {
            colourWordManager.SelectNewColourWord();
        }
    }
}
