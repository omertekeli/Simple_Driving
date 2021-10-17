using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] float scoreMultipler;

    public const string HighScoreKey = "HighScore";
    private float score;

    // Update is called once per frame
    void Update()
    {
        //Increasing score in accordance time 
        score += Time.deltaTime * scoreMultipler;

        //To round float to int and also to show as string all int values
        scoreText.text = Mathf.FloorToInt(score).ToString();
    }

    //OnDestroy is used because we want to data which is occured when game was end
    private void OnDestroy()
    {
        //PlayerPrefs stores float, int, string value in a game session
        //HighScoreKey is a string variable
        int currentHighScore = PlayerPrefs.GetInt(HighScoreKey, 0);

        //Score is bigger than last game score, it will be high score
        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, Mathf.FloorToInt(score));
        }
    }
}
