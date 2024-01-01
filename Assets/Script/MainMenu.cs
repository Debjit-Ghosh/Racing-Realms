using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TMP_Text highScoreText;
    [SerializeField] TMP_Text fuelText;
    [SerializeField] private Button playButton;
    [SerializeField] int maxFuel;
    [SerializeField] int fuelRecahrgeDuration;

    int fuel;

    const string FuelKey = "Fuel";
    const string FuelReadyKey = "FuelReady";

    private void Start()
    {
        OnApplicationFocus(true);
    }
    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
            return;

        CancelInvoke();

        int highScore = PlayerPrefs.GetInt(ScoreSystem.HighScoreKey, 0);
        highScoreText.text = $"HighScore: {highScore}";
        
        //
        fuel = PlayerPrefs.GetInt(FuelKey, maxFuel);

        if (fuel == 0) 
        {
            string fuelReadyString = PlayerPrefs.GetString(FuelReadyKey, string.Empty);
            if (fuelReadyString == string.Empty)
                return;

            DateTime fuelReady = DateTime.Parse(fuelReadyString);

            if(DateTime.Now > fuelReady)
            {
                fuel = maxFuel;
                PlayerPrefs.SetInt(FuelKey, fuel);
            }
            else
            {
                playButton.interactable = false;
                Invoke(nameof(FuelRecharged), (fuelReady - DateTime.Now).Seconds);
            }
        }
        //

        fuelText.text = $"Fuel: ({fuel})";
    }
    private void FuelRecharged()
    {
        playButton.interactable = true;
        fuel = maxFuel;
        PlayerPrefs.SetInt(FuelKey, fuel);

        fuelText.text = $"Fuel: ({fuel})";
    }
    public void Play()
    {
        if (fuel < 1)
            return;

        fuel--;

        PlayerPrefs.SetInt(FuelKey, fuel);
        
        if(fuel == 0)
        {
            DateTime fuelReady = DateTime.Now.AddMinutes(fuelRecahrgeDuration);
            PlayerPrefs.SetString(FuelReadyKey, fuelReady.ToString());
        }
        SceneManager.LoadScene(1);
    }
    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
