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

    [SerializeField] private List<ColourType> colourTypes;

    private int colourIndex;    // Used to randomly pick a colour
    private int wordIndex;  // Used to randomly pick a word

    private ColourType currentColour;   // Used to keep track of the current/previous colour
    private ColourType currentWord; // Used to keep track of the current/previous word 

    void Awake()
    {
        if (Instance)
            Destroy(Instance);

        Instance = this;
    }

    public void Initialise()
    {
        // First attempt to randomise the two indexes since they are at a default of 0
        colourIndex = Random.Range(0, colourTypes.Count);
        currentColour = colourTypes[colourIndex];

        wordIndex = Random.Range(0, colourTypes.Count);
        currentWord = colourTypes[wordIndex];
    }

    /// <summary>
    /// Invoked by the GameManager. 
    /// The function selects a new random colour word
    /// </summary>
    public void SelectNewColourWord()
    {        
        // Ensures a differnt colour word is selcted
        Randomise();

        UIManager.Instance.colourWord.color = currentColour.colour;
        UIManager.Instance.colourWord.text = currentWord.colourName;
    }

    private void Randomise()
    {
        // Record the previously used colour and word
        ColourType previousWord = currentWord;
        ColourType previousColour = currentColour;

        // Remove the used colour from the list
        colourTypes.Remove(previousColour);

        // Randomise a new colour with other colours that haven't been used 
        colourIndex = Random.Range(0, colourTypes.Count);
        currentColour = colourTypes[colourIndex];

        // Add the removed colour back
        colourTypes.Add(previousColour);

        // Check if the new selected colour was the same as the previous word
        if (currentColour == previousWord)
        {
            // Just remove the colour from the list
            colourTypes.Remove(currentColour);

            wordIndex = Random.Range(0, colourTypes.Count);
            currentWord = colourTypes[wordIndex];

            // Add the colour back to the list
            colourTypes.Add(currentColour);
        }
        else
        {
            // Just remove the colour and previous word from the list
            colourTypes.Remove(currentColour);
            colourTypes.Remove(previousWord);

            wordIndex = Random.Range(0, colourTypes.Count);
            currentWord = colourTypes[wordIndex];

            // Add the colour and previous word back to the list
            colourTypes.Add(currentColour);
            colourTypes.Add(previousWord);
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
        if(colour.colour == this.currentColour.colourId)    
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
