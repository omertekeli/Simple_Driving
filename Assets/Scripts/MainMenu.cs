using System; //To use DateTime class
using System.Collections;
using System.Collections.Generic;
using TMPro; //To use TMP_Text class
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; //To use Button class

public class MainMenu : MonoBehaviour
{
    [SerializeField] TMP_Text highScoreText;
    [SerializeField] TMP_Text energyText;
    [SerializeField] Button playButton;
    [SerializeField] int maxEnergy; 
    [SerializeField] int energyRechargeDuration; // To sign when energy is recharged

    private int energy;

    private const string EnergyKey = "Energy";
    private const string EnergyReadyKey = "EnegryReady";

    private void Start()
    {
        OnApplicationFocus(true); //Focus is on 
    }

    //Sent all gameobjects when player get or loss focus
    private void OnApplicationFocus(bool hasFocus)
    {
        if(!hasFocus) { return; }

        CancelInvoke(); //stop the ınvokes

        int highScore = PlayerPrefs.GetInt(ScoreSystem.HighScoreKey, 0);

        //It can be type as = "High Score: " + highScore.ToString();
        highScoreText.text = $"High Score: {highScore}";

        energy = PlayerPrefs.GetInt(EnergyKey, maxEnergy); //Firstly, energy = maxenergys

        if (energy == 0)
        {
            string energyReadyString = PlayerPrefs.GetString(EnergyReadyKey, string.Empty);

            if (energyReadyString == string.Empty) { return; }

            //DateTime is used to get time:month:year. To compare transform the string
            DateTime energyReady = DateTime.Parse(energyReadyString);

            if (DateTime.Now > energyReady)
            {
                energy = maxEnergy;
                PlayerPrefs.SetInt(EnergyKey, energy);
            }
            else
            {
                playButton.interactable = false; //Play button is inactive
                Invoke(nameof(EnergyRecharged), (energyReady - DateTime.Now).Seconds);
                //EnergyRecharged method will be work, after after seconds between realtime and energyReady
            }
        }

        energyText.text = $"Play({energy})"; //It will be seem liket that Play(5)
    }

    private void EnergyRecharged()
    {
        playButton.interactable = true;
        energy = maxEnergy;
        PlayerPrefs.SetInt(EnergyKey,energy);
        energyText.text = $"Play({energy})";
    }

    public void Play()
    {
        if (energy < 1){ return;} 

        energy--; //Decrease energy after clicking play button

        PlayerPrefs.SetInt(EnergyKey, energy); //Set the energy in accordance to last energy amount after clicking button

        if (energy == 0)
        {
            DateTime energyReady = DateTime.Now.AddMinutes(energyRechargeDuration);
            PlayerPrefs.SetString(EnergyReadyKey, energyReady.ToString());
        }
        SceneManager.LoadScene("GameMenu");
    }
}
