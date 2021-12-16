using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourWordManager : MonoBehaviour
{
    public static ColourWordManager Instance;

    [SerializeField] private List<string> words;
    [SerializeField] private List<Color> colours;

    public List<int> colourIds;

    public List<ColourType> colourTypes;

    private int randWordIndex;
    private int randColourIndex;

    //private string currentWord;
    //private Color currentColour;

    private GameManager.Colours currentColour;

    private int prevWordIndex;
    private int prevColourIndex;
    int colourIndex;
    int wordIndex;

    ColourType colour;
    ColourType word;


    void Awake()
    {
        if (Instance)
            Destroy(Instance);

        Instance = this;
    }

    private void Start()
    {
        // Assign each colour id a number for each colour in the Colours enum
        for(int i = 0; i < System.Enum.GetValues(typeof(GameManager.Colours)).Length; i++)
        {
            colourIds.Add(i);
        }
    }

    public void Initialise()
    {
        colourIndex = Random.Range(0, colourTypes.Count);
        colour = colourTypes[colourIndex];

        wordIndex = Random.Range(0, colourTypes.Count);
        word = colourTypes[wordIndex];

        // Update the colour word text
        UIManager.Instance.colourWord.text = colourTypes[wordIndex].colourName;
        UIManager.Instance.colourWord.color = colourTypes[colourIndex].colour;
    }

    public void SelectNewColourWord()
    {        
        //wordIndex = Random.Range(0, words.Count);
        //colourIndex = Random.Range(0, colourTypes.Count);

        // Ensures a differnt colour word is selcted
        Randomise();

        if (wordIndex == prevWordIndex)
        {
            //wordIndex = GenerateNewColourWord(wordIndex);
        }
        Debug.Log("wordINdex: " + wordIndex);

        if(colourIndex == prevColourIndex)

        // Keep track of current word and colour used
        prevWordIndex = wordIndex;
        prevColourIndex = colourIndex;
        currentColour = (GameManager.Colours)colourIndex;

        //// Update the colour word text
        //UIManager.Instance.colourWord.text = colourTypes[wordIndex].colourName;
        //UIManager.Instance.colourWord.color = colourTypes[colourIndex].colour;
    }

    private void Randomise()
    {
        // Add the removed text back
        //words.Add(text);

        ColourType lastTextToExemptOk = word;
        ColourType colourToExempt = colour;

        // Remove the previuslly used colour index
        colourTypes.Remove(colourToExempt);

        colourIndex = Random.Range(0, colourTypes.Count);
        colour = colourTypes[colourIndex];

        // Update the colour word text
        UIManager.Instance.colourWord.color = colour.colour;

        //ColourType colourWordToExempt = colour;

        // Add the removed colour back
        colourTypes.Add(colourToExempt);

        // + previous word
        //ColourType wordToExempt1 = colourTypes[wordIndex];

        if (colour == lastTextToExemptOk)
        {
            // Just remove one
            colourTypes.Remove(colour);
        }
        else
        {

            // Remove the previuslly used colour index
            colourTypes.Remove(colour);
            colourTypes.Remove(lastTextToExemptOk);
            
        }

        wordIndex = Random.Range(0, colourTypes.Count);
        word = colourTypes[wordIndex];

        UIManager.Instance.colourWord.text = word.colourName;

        if (colour == lastTextToExemptOk)
        {
            // Just add one
            colourTypes.Add(colour);
        }
        else
        {

            // Remove the previuslly used colour index
            colourTypes.Add(colour);
            colourTypes.Add(lastTextToExemptOk);

        }
    }

    private int GenerateNewColourWord(int index)
    {
        List<string> tempWords = new List<string>();
        for(int i = 0; i <words.Count; i++)
        {
            if(i != index)
                tempWords.Add(words[i]);
        }

        // TO-do
        // The colour must be different from the word
        // itself


        index = Random.Range(0, tempWords.Count);


        // Returmn string instead?
        return index;
    }

    public int RandomIntExcept(int min, int max, int except)
    {
        int result = Random.Range(min, max - 1);
        if (result >= except) result += 1;
        return result;
    }

    public bool CompareColours(ColourOption colour)
    {
        if(colour.colour == this.colour.colourId)    
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
