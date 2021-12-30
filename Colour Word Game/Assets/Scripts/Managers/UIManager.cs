using UnityEngine;
using TMPro;

/// <summary>
/// The UIManager manages the ui in the game
/// by updating, enabling and disabling them
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("State Panels")]
    [Space]
    [SerializeField] private GameObject startContent;
    [SerializeField] private GameObject gameContent;
    [SerializeField] private GameObject endContent;

    [Header("Scoring Text")]
    [Space]
    [SerializeField] private TextMeshProUGUI timeTakenText;
    [SerializeField] private TextMeshProUGUI colourWord;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI answersText;

    private void Awake()
    {
        if (Instance)
            Destroy(Instance);

        Instance = this;
    }

    /// <summary>
    /// Invoked by the GameManager.
    /// Enables the start screen
    /// </summary>
    public void DisplayStartScreen()
    {
        endContent.SetActive(false);

        startContent.SetActive(true);
    }

    /// <summary>
    /// Invoked by the GameManager.
    /// Enables the game screen
    /// </summary>
    public void DisplayGameScreen()
    {
        startContent.SetActive(false);

        gameContent.SetActive(true);
    }

    /// <summary>
    /// Invoked by the GameManager.
    /// Enables the end screen
    /// </summary>
    public void DisplayEndScreen(int score, int timeTaken)
    {
        // Update score texts
        scoreText.text = score.ToString();
        timeTakenText.text = timeTaken.ToString();
        answersText.text = $"{GameManager.Instance.CorrectAnswerCounter} / {GameManager.Instance.numberOfRounds}";

        endContent.SetActive(true);
        gameContent.SetActive(false);
    }

    /// <summary>
    /// Invoked by the ColourWordManager.
    /// Updates the colour word text.
    /// </summary>
    /// <param name="visualColour"></param>
    /// <param name="colourText"></param>
    public void DisplayColourWord(ColourAssets visualColour, ColourAssets colourText)
    {
        colourWord.color = visualColour.colour;
        colourWord.text = colourText.colourName;
    }
}