using UnityEngine;
using TMPro;

using System;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText = null;
    [SerializeField] TextMeshProUGUI highscoreText = null;

    public float scoreTimer { private set; get; }

    public bool shouldIncreaseScore = true;

    private void Start()
    {
        if (!scoreText || !highscoreText)
        {
            Debug.Log("References not set in score text HUD. Timers aren't being updated!!!");
            Destroy(this);
        }
        else
        {
            highscoreText.text = "NO HS";
            scoreText.text = scoreTimer.ToString();
        }
    }

    private void Update()
    {
        if (shouldIncreaseScore)
        {
            scoreTimer += Time.deltaTime;
            string span = TimeSpan.FromSeconds(scoreTimer).ToString(@"mm\:ss\.ff");
            
            scoreText.text = "CS " + span.ToString();
        }
    }
}
