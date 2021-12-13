using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourWordManager : MonoBehaviour
{
    public static ColourWordManager Instance;

    [SerializeField] private string[] words;
    [SerializeField] private Color[] colours;

    private int randWordIndex;
    private int randColourIndex;

    private string currentWord;
    private Color currentColour;

    void Awake()
    {
        if (Instance)
            Destroy(Instance);

        Instance = this;
    } 

    public void CycleToNext()
    {

    }

    public void Randomise()
    {
        int wordIndex = Random.Range(0, words.Length);
        int colourIndex = Random.Range(0, colours.Length);

        currentWord = words[wordIndex];
        currentColour = colours[colourIndex];

        // Update the colour word text
        UIManager.Instance.colourWord.text = currentWord;
        UIManager.Instance.colourWord.color = currentColour;
    }

    public int RandomIntExcept(int min, int max, int except)
    {
        int result = Random.Range(min, max - 1);
        if (result >= except) result += 1;
        return result;
    }

    public void CompareColours(GameManager.Colours colour)
    {
        //if(colour == )
    }
}
