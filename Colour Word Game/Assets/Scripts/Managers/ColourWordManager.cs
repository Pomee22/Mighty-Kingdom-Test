using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the color words by 
/// randomly selecting a colour and word 
/// </summary>
public class ColourWordManager : MonoBehaviour
{
    public static ColourWordManager Instance;

    public enum Colours
    {
        RED,
        YELLOW,
        BLUE,
        GREEN
    };

    [SerializeField] private List<ColourAssets> colours;

    private int colourIndex;    // Used to randomly pick a colour
    private int wordIndex;  // Used to randomly pick a word

    private ColourAssets currentColour;   // Used to keep track of the current/previous colour
    private ColourAssets currentWord; // Used to keep track of the current/previous word 

    void Awake()
    {
        if (Instance)
            Destroy(Instance);

        Instance = this;
    }

    public void Initialise()
    {
        // First attempt to randomise the two indexes since they are at a default of 0
        colourIndex = Random.Range(0, colours.Count);
        currentColour = colours[colourIndex];

        wordIndex = Random.Range(0, colours.Count);
        currentWord = colours[wordIndex];

        SelectNewColourWord();
    }

    /// <summary>
    /// Invoked by the GameManager. 
    /// The function selects a new random colour word
    /// </summary>
    public void SelectNewColourWord()
    {        
        // Ensures a differnt colour word is selcted
        Randomise();

        // Update colour word text
        UIManager.Instance.DisplayColourWord(currentColour, currentWord);
    }

    private void Randomise()
    {
        // Record the previously used colour and word
        ColourAssets previousWord = currentWord;
        ColourAssets previousColour = currentColour;

        // Remove the used colour from the list
        colours.Remove(previousColour);

        // Randomise a new colour with other colours that haven't been used 
        colourIndex = Random.Range(0, colours.Count);
        currentColour = colours[colourIndex];

        // Add the removed colour back
        colours.Add(previousColour);

        // Check if the new selected colour was the same as the previous word
        if (currentColour == previousWord)
        {
            // Just remove the colour from the list
            colours.Remove(currentColour);

            wordIndex = Random.Range(0, colours.Count);
            currentWord = colours[wordIndex];

            // Add the colour back to the list
            colours.Add(currentColour);
        }
        else
        {
            // Just remove the colour and previous word from the list
            colours.Remove(currentColour);
            colours.Remove(previousWord);

            wordIndex = Random.Range(0, colours.Count);
            currentWord = colours[wordIndex];

            // Add the colour and previous word back to the list
            colours.Add(currentColour);
            colours.Add(previousWord);
        }
    }

    /// <summary>
    /// Invoked by the GameManager when the player selects a button.
    /// The funcion checks if the colour is the correct answer
    /// </summary>
    /// <param name="colour"></param>
    /// <returns></returns>
    public bool CompareColours(ColourOption colour)
    {
        if(colour.colour == this.currentColour.colourType)    
        {
            Debug.Log("Bingo");
            return true;
        }
        else
        {
            Debug.Log("Nope");
            return false;
        }
    }
}

[System.Serializable]
public class ColourAssets
{
    public ColourWordManager.Colours colourType;
    public Color colour;
    public string colourName;
}

