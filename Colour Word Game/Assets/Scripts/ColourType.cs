using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Colour", menuName = "ScriptableObjects/ColourType", order = 1)]
public class ColourType : ScriptableObject
{
    public Color colour;

    public GameManager.Colours colourId;

    public string colourName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
