using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState
    {
        START,
        GAME,
        END
    }

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
        
    }

    public void UpdateGameStart(GameState state)
    {
        // Change the current game state with a new state
        curretState = state;

        // Run any events on game state change
        switch(curretState)
        {
            case GameState.START:
                break;
            case GameState.GAME:
                UIManager.Instance.timer.StartCountdown();
                // Start game loop
                break;
            case GameState.END:
                break;
        }
    }
}
