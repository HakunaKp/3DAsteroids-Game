using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gameplay : MonoBehaviour
{
    public GameObject asteroid;
    public GameObject healthBar;
    public int asteroidsNum;        // Get asteroidsNum from Prompts
    public bool asteroidsReady;
    public float health = 100f;

    public bool endGame;
    bool immunity;

    TimeManager TM;
    RestartScene SM;
    Text score;

    private void Start()
    {
        // Begin game
        endGame = false;

        // Create asteroids
        CreateAsteroids(asteroidsNum);

        // Get score Text
        score = GameObject.Find("Score").GetComponent<Text>();

        // Get TimeManager
        TM = GetComponent<TimeManager>();

        // Get SceneManager
        SM = GameObject.Find("SceneManager").GetComponent<RestartScene>();

        // Assign immunity
        immunity = false;

    }

    private void FixedUpdate()
    {
        // Do not set asteroids unless user input flag is received
        if (asteroidsReady == true) 
        {
            // Activate health display
            GameObject.Find("HealthBar").GetComponent<Canvas>().enabled = true;

            // Create asteroids
            CreateAsteroids(asteroidsNum);
        }

        if (Input.GetKeyDown("y") && endGame == true) { SM.Restart(); }
        if (Input.GetKeyDown("n") && endGame == true) { Quit(); }
    }

    private void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 0.8f);

        if (Input.GetKeyUp("q"))
        {
            immunity = true;
            TM.StopTime();
        }
        if (Input.GetKeyUp("e"))
        {
            immunity = false;
            TM.ContinueTime();
        }
    }


    private void CreateAsteroids(int asteroidsNum)
    {
        for (int i = 1; i <= asteroidsNum; i++) {
            GameObject asteroidsClone = Instantiate(asteroid, new Vector2(Random.Range(-10, 10), Random.Range(-10, 10)), transform.rotation);
            asteroidsClone.SetActive(true);
        }

        // Asteroids are created
        asteroidsReady = false;
    }

    public void UpdateHealth()
    {
        if (!immunity) {
            // Decrease health
            health = health - 10f;
            Set_Width(healthBar, health);
        }
        
        // Player lost game
        if (health <= 0) {
            score.text = "The rocket is dead!";
            EndGame(); // play again?
        }
    }

    public void UpdateAsteroids()
    {
        // Decrease asteroid cout
        asteroidsNum = asteroidsNum - 1;
        score.text = "Remaining Asteroids: " + asteroidsNum;

        // Player won game
        if (asteroidsNum == 0) { 
            score.text = "You win!";
            EndGame(); // play again?
        } 

    }

    // Game Over / Victory scenarios
    public void EndGame()
    {
        // Display end prompt
        GameObject.Find("EndPrompt").GetComponent<Text>().enabled = true;
        
        // Play again enabled flag
        endGame = true;

        // Enable immunity
        immunity = true;

        // Stop time
        TM.StopTime();
    }

    public void Quit() {
        Application.Quit(0);
    }

    public static void Set_Width(Component component, float width)
    {
        if (component != null)
        {
            Set_Width(component.gameObject, width);
        }
    }
    public static void Set_Width(GameObject gameObject, float width)
    {
        if (gameObject != null)
        {
            var rectTransform = gameObject.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.sizeDelta = new Vector2(width, rectTransform.sizeDelta.y);
            }
        }
    }
}
