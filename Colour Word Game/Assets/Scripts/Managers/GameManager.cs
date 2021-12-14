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

    private int score = 0;

    public enum GameState
    {
        START,
        GAME,
        END
    };

    public enum Colours
    {
        RED,
        YELLOW,
        BLUE,
        GREEN
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
                {
                    break;
                }
            case GameState.GAME:
                timerBar.StartCountdown(roundDuration);
                colourWordManager.Randomise();
                // Start game loop
                break;
            case GameState.END:
                UIManager.Instance.DisplayEndGame(score);
                Debug.Log("GAME END");
                break;
        }
    }

    // Called by Unity onclick event.
    // Gets invoked when player selects
    // any one of the given colour options
    public void ColourSelected(ColourOption colourOption)
    {
        if (ColourWordManager.Instance.CompareColours(colourOption.colour))
            score++;

        currentRound++;

        // If this was the last round...
        if (currentRound == rounds)
            UpdateGameState(GameState.END);
        else
            UpdateGameState(GameState.GAME);
    }
}
