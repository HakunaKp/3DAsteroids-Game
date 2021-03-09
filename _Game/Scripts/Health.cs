using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public float health = 100f;
    int numAsteroids;
    public Health healthBar;
    public Gameplay GP;

    // Start is called before the first frame update
    public void Start()
    {
        numAsteroids = GameObject.Find("Gameplay").GetComponent<Gameplay>().asteroidsNum;
        healthBar = GetComponent<Health>();
        Set_Width(healthBar, health);
    }

    public void UpdateHealth()
    {
        health = health - 5f;
        Set_Width(healthBar, health);

        // Player lost game
        if (health == 0) {
        //    gameText.text = "You Lost";
        }
    }

    public void UpdateAsteroids()
    {
        numAsteroids = numAsteroids - 1;

        // Player won game
        if (numAsteroids == 0) {
        //    gameText.text = "You Won";
        }

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
