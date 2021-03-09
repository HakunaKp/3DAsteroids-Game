using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prompts : MonoBehaviour
{
    public GameObject asteroid;
    public GameObject scoreObject;
    public GameObject input;
    public Gameplay GP;
    Text score;

    void Awake()
    {
        // Hide score
        score = scoreObject.GetComponent<Text>();
        score.enabled = false;

        // Hide health bar
        GameObject.Find("HealthBar").GetComponent<Canvas>().enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Do not set asteroids until user input is received
        GP = GameObject.Find("Gameplay").GetComponent<Gameplay>();
        GP.asteroidsReady = false;
        asteroid.SetActive(false);
    }

    // After user keys enter on opening prompt
    // Method to get number of asteroids to generate
    public void ReadStringInput(string numAsteroids_string) 
    {
        // Store number of asteroids input
        numAsteroids_string = GameObject.Find("Input").GetComponent<Text>().text;

        // Check input validity
        if (int.TryParse(numAsteroids_string, out GP.asteroidsNum)) 
        {
            // Remove input field
            GameObject.Find("InputField").SetActive(false);

            // Toggle score on
            score.enabled = true;
            score.text = "Remaining Asteroids: " + numAsteroids_string;

            // Asteroids ready to be set in Gameplay.cs
            GP.asteroidsReady = true;

            // Hide cursor
            Cursor.visible = false;
        } else { GameObject.Find("Input").GetComponent<Text>().text = ""; }
    }
}
