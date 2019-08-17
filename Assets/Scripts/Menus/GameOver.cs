using UnityEngine;
using UnityEngine.SceneManagement;

// This script listens for player input when the player has dies
// it also triggers the HUD death animation
public class GameOver : MonoBehaviour
{
    [SerializeField] PlayerController playerState = null;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerState.state == PlayerController.PlayerState.Dead)
        {
            anim.SetTrigger("GameOver");
            
            if(Input.GetAxisRaw("Dash") == 1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
