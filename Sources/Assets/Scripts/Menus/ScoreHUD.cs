using UnityEngine;
using TMPro;

using System;

public class ScoreHUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText = null;
    [SerializeField] TextMeshProUGUI highscoreText = null;
    [SerializeField] PlayerTimer score = null;

    float hs;
    const string hsKey = "highscore";

    private void Start()
    {
        hs = PlayerPrefs.GetFloat("highscore", -1);
        if (hs == -1)
            highscoreText.text = "NO HIGHSCORE";
        else
            highscoreText.text = "HS " + FormatToDate(hs);
        
        ToUpdate();
    }

    private void Update()
    {
        ToUpdate();
    }

    // This function is called ToUpdate because it is supposed to be called during the update but also once
    // during start. by doing this, I don't have to write logic twice
    private void ToUpdate()
    {
        scoreText.text = "CS " + FormatToDate(score.ElapsedTime());
    }

    private string FormatToDate(float aliveTimeInSeconds)
    {
        return TimeSpan.FromSeconds(aliveTimeInSeconds).ToString(@"mm\:ss\.ff");
    }

    private void OnDestroy()
    {
        if (score.ElapsedTime() > hs)
            PlayerPrefs.SetFloat(hsKey, score.ElapsedTime());
    }
}