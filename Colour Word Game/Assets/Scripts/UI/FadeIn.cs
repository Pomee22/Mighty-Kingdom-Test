using UnityEngine;
using TMPro;
using System.Threading.Tasks;

/// <summary>
/// Plays an animation for text to fade in
/// when the gameObject becomes active
/// </summary>
public class FadeIn : MonoBehaviour
{
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI[] texts;

    [Header("Time")]
    [Space]
    [SerializeField] private float delay = 0.0f;
    [SerializeField] private float duration = 5.0f;

    [Header("Colour")]
    [Space]
    [SerializeField] private Color32 endColour;
    [SerializeField] private Color32 startColour;

    private Task[] tasks;   // Used to delay the fade of each text

    private void OnEnable()
    {
        tasks = new Task[texts.Length];

        // Sets all texts with the starting colour value
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].faceColor = startColour;
        }

        // Invoke the function with at a timed delay
        Invoke("FadeInActive", delay);
    }

    private async void FadeInActive()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            tasks[i] = LerpColour(texts[i]);
        }

        // Ensures all tasks are done asynchronously 
        await Task.WhenAll(tasks);
    }

    private async Task LerpColour(TextMeshProUGUI text)
    {
        float timeElasped = 0.0f;

        // Lerps the text's colour within the given duration
        while (timeElasped < duration)
        {
            text.faceColor = Color32.Lerp(text.faceColor, endColour, timeElasped / duration);

            timeElasped += Time.deltaTime;

            await Task.Yield();
        }
    }
}
