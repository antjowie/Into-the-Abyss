using UnityEngine;
using TMPro;

using System;

public class ScoreHUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText = null;
    [SerializeField] TextMeshProUGUI highscoreText = null;
    [SerializeField] PlayerTimer timer = null;

    private void Start()
    {
        highscoreText.text = "NO HS";
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
        scoreText.text = "CS " + FormatToDate(timer.ElapsedTime());
    }

    private string FormatToDate(float aliveTimeInSeconds)
    {
        return TimeSpan.FromSeconds(aliveTimeInSeconds).ToString(@"mm\:ss\.ff");
    }
}