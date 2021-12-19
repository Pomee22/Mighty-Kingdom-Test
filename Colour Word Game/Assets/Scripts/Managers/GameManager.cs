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

    // Todo:
    // Let time affect the score
    // Either by using a range or dynamically 
    // determining the score based off time
    private float minTime, midTime, maxTime;

    public enum GameState
    {
        START,
        CLASSICMODE,
        SWITCHMODE,
        QUICKCLASSICMODE,
        QUICKSWITCHMODE,
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
                //timerBar.StartCountdown(roundDuration);
                colourWordManager.Initialise();
                colourWordManager.SelectNewColourWord();
                gameTimer.StartCountdown();
                SetUpGameLevel();
                // Start game loop
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

    private int GetTimeScore(int timeTaken)
    {
        return ((int)roundDuration * rounds) - timeTaken;
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
        }

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
