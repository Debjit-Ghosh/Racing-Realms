using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ScoreSystem : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    float score, scoreMultiplier= 1f;

    public const string HighScoreKey = "HighScore";
    void Start()
    {
        
    }
    void Update()
    {
        score += scoreMultiplier * Time.deltaTime;

        scoreText.text = Mathf.FloorToInt(score).ToString();
    }

    private void OnDestroy()
    {
        int currentHighScore = PlayerPrefs.GetInt(HighScoreKey, 0);

        if (score > currentHighScore) 
        { PlayerPrefs.SetInt(HighScoreKey, Mathf.FloorToInt(score));  }
    }
}
