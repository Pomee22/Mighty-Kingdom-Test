using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public ColourWordManager colourWordManager;

    [Tooltip("The number of rounds the game consists of.")]
    public int rounds = 5;
    public float roundDuration = 5.0f;

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
                    colourWordManager.Randomise();

                    break;
                }
            case GameState.GAME:
                UIManager.Instance.timer.StartCountdown(roundDuration);
                // Start game loop
                break;
            case GameState.END:
                break;
        }
    }

    // Called by Unity onclick event.
    // Gets invoked when player selects
    // any one of the given colour options
    public void ColourSelected(ColourOption colourOption)
    {
        Debug.Log(colourOption.colour + " selected");
    }
}
