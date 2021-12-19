using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public ColourWordManager colourWordManager;
    public TimerBar timerBar;

    [Tooltip("The number of rounds the game consists of.")]
    public int rounds = 5;
    public float roundDuration = 5.0f;
    private int currentRound = 0;

    [SerializeField] private Timer gameTimer; 

    private int score = 0;

    private bool[] correctAnswers;

    public enum GameState
    {
        START,
        CLASSICMODE,
        QUICKMODE,
        END
    };

    private GameState curretState;

    private void Awake()
    {
        if (Instance)
            Destroy(Instance);
        
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        correctAnswers = new bool[rounds];

        UpdateGameState(GameState.START);
    }

    public void UpdateGameState(GameState state)
    {
        // Change the current game state with a new state
        curretState = state;

        // Run any events on game state change
        switch(curretState)
        {
            case GameState.START:               
                break;                
            case GameState.CLASSICMODE:
                StartGameLevel();
                break;
            case GameState.END:
                EndGame();
                break;
        }
    }

    /// <summary>
    /// Button onclick event.
    /// Starts the game.
    /// </summary>
    public void StartGame(GameModeOption gameModeOption)
    {
        UpdateGameState(gameModeOption.gameMode);
    }

    private async void SetUpGameLevel()
    {
        // Wait for the transition animation to finish
        await UIManager.Instance.Transition();

        // Disable the current start ui
        UIManager.Instance.StartGame();
    }

    private void StartGameLevel()
    {
        // Randomise colour word 
        colourWordManager.Initialise();

        // Set up timer and scene
        gameTimer.StartCountdown();
        SetUpGameLevel();
    }

    private void EndGame()
    {
        // Stop the game timer
        gameTimer.StopCountDown();

        // Round the timer as an int
        int timeTaken = (int)Math.Round(gameTimer.GetTime(), 0);
        int timeScore = GetTimeScore(timeTaken);

        // Update ui score
        UIManager.Instance.DisplayEndGame(score, timeTaken, timeScore);
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
        int timeScore = ((int)roundDuration * rounds) - timeTaken;
        timeScore /= rounds;
        
        for(int i = 0; i < rounds; i++)
        {
            // If the answer for the round was correct...
            if(correctAnswers[i])
            {
                // Add the time bonus point
                resultantTimeScore += timeScore;
            }
        }

        return resultantTimeScore;

    }

    // Called by Unity onclick event.
    // Gets invoked when player selects
    // any one of the given colour options
    public void ColourSelected(ColourOption colourOption)
    {
        // If the player selected the correct colour...
        if (ColourWordManager.Instance.CompareColours(colourOption))
        {        
            score++;
            correctAnswers[currentRound] = true;
        }
        else
            correctAnswers[currentRound] = false;

        NextRound();
    }

    public void NextRound()
    {
        currentRound++;

        // If this was the last round...
        if (currentRound == rounds)
            UpdateGameState(GameState.END);
        else
        {
            timerBar.Reset();
            colourWordManager.SelectNewColourWord();
        }
    }
}
