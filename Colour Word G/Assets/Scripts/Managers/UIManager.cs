using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject startContent;
    public GameObject gameContent;

    public Timer timer;
    public TextMeshProUGUI timerText;

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
    public async void StartGame()
    {
        // Wait for the transition animation to finish
        await Transition();

        startContent.SetActive(false);
        gameContent.SetActive(true);

        // Then proceed to the game state
        GameManager.Instance.UpdateGameStart(GameManager.GameState.GAME);
    }

    private async Task Transition()
    {
        await Task.Yield();
    }

    public void UpdateStartContent(bool status)
    {
        startContent.SetActive(status);
    }

    public void UpdateGameContent(bool status)
    {

    }

    public void UpdateEndContent(bool status)
    {

    }
}
