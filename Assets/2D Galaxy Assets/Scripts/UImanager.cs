using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;


public class UImanager : MonoBehaviour
{
    public Sprite[] lives;
    public Image livesImageDisplay;
    public int score;
    public Text scoreText;
    public GameObject titleScreen;


    public void updateImage(int currentLives)
    {
        livesImageDisplay.sprite = lives[currentLives];

    }

    public void updateScore()
    {
         score += 10;
        scoreText.text = "Score: " + score;
    }

    public void showTitleScreen()
    {
        titleScreen.SetActive(true);
    }

    public void hideTitleScreen()
    {
        titleScreen.SetActive(false);
        scoreText.text = "Score: ";
    }

}
