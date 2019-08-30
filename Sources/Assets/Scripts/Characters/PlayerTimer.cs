using UnityEngine;

public class PlayerTimer : MonoBehaviour
{
    // This is so that we can check the state to see if player is dead
    PlayerController player = null;

    float initialTime;
    float deltaTime;

    void Start()
    {
        player = GetComponent<PlayerController>();
        deltaTime = initialTime = Time.timeSinceLevelLoad;
    }

    private void Update()
    {
        if(player.state != PlayerController.PlayerState.Dead)
            deltaTime = Time.timeSinceLevelLoad;
    }

    public float ElapsedTime()
    {
        return deltaTime - initialTime;
    }
}
